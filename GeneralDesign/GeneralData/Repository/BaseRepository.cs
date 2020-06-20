using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace GeneralData.Repository
{
    /// <summary>Base repository for all generic implimentations of database methods.</summary>
    public abstract class BaseRepository : IGenericRepository
    {
        abstract internal DbContext Context { get; set; }
        public void ChangeDatabase(string databaseName)
        {
            if (Context.Database.GetDbConnection().State == System.Data.ConnectionState.Closed)
                Context.Database.GetDbConnection().Open();

            Context.Database.GetDbConnection().ChangeDatabase(databaseName);
        }

        #region Create
        public virtual void Insert<T>(T entity) where T : class => Context.Set<T>().Add(entity);

        public virtual void BulkInsert<T>(IEnumerable<T> query) where T : class
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = false;

            IEnumerable<T> entities = query.AsEnumerable();
            Context.Set<T>().AddRange(entities);
            Context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
        /// <summary>Create a new entity from DTO</summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <typeparam name="T1">DTO Type</typeparam>
        /// <param name="objectWithChanges">DTO object</param>
        public virtual T InsertFromObject<T, T1>(T1 objectWithChanges) where T : class where T1 : class
        {
            T entity = CopyClass.CopyToNew<T, T1>(objectWithChanges);
            Insert(entity);
            return entity;
        }
        #endregion

        #region Read
        /// <summary>
        /// Get entities from database
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="filter">Filter collection criteria</param>
        /// <param name="orderBy">Order result by</param>
        /// <param name="includeProperties">String of comma seperated entities to be included on eagerLoading</param>
        /// <param name="eagerLoad">Switch lazy loading off to only get this entity and includeProperties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get<T>(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "", bool eagerLoad = false, IQueryable<T> query = null) where T : class
        {
            List<T> result;

            if (query == null)
                query = Context.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            try
            {
                if (orderBy != null)
                    result = orderBy(query).ToList();
                else
                    result = query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Get entity by its id. Lazy loading will be on
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T GetByKey<T>(params object[] key) where T : class => Context.Set<T>().Find(key);

        /// <summary>
        /// Find an entity by a predicate filter. Lazy loading will be on 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            IQueryable<T> query = Context.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }
        public TResult GetMaxBy<T, TResult>(Expression<Func<T, TResult>> maxField = null, Expression<Func<T, bool>> predicate = null) where T : class
        {
            if (predicate != null)
                return Context.Set<T>().Where(predicate).Max(maxField);
            else
                return Context.Set<T>().Max(maxField);
        }
        #endregion

        #region Update
        public virtual void Update<T>(T entityToUpdate) where T : class
        {
            Context.Set<T>().Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void UpdateEntity<T>(T entityToUpdate, int id) where T : class
        {
            var entity = Context.Set<T>().Find(id);
            Context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        }
        /// <summary>Update entity from another entity or DTO</summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <typeparam name="T1">object with changes'type</typeparam>
        /// <param name="objectWithChanges">source entity or DTO with changed properties</param>
        /// <param name="key">Primary key of entity</param>
        public virtual void UpdateFromObject<T, T1>(T1 objectWithChanges, params object[] key) where T : class where T1 : class
        {
            var entity = GetByKey<T>(key);
            CopyClass.Copy(entity, objectWithChanges);
            Update(entity);
        }
        #endregion

        #region Delete
        public virtual void Delete<T>(object id) where T : class
        {
            T entityToDelete = Context.Set<T>().Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete<T>(T entityToDelete) where T : class
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
                Context.Set<T>().Attach(entityToDelete);

            Context.Set<T>().Remove(entityToDelete);
        }
        public virtual void DeleteByKey<T>(params object[] key) where T : class
        {
            var entityToDelete = GetByKey<T>(key);
            Context.Set<T>().Remove(entityToDelete);
        }
        public virtual void DeleteRange<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var records = Context.Set<T>().Where(predicate).ToList();

            if (records.Count > 0)
                Context.Set<T>().RemoveRange(records);
        }
        public virtual void DeleteRangeByList<T>(IEnumerable<T> list) where T : class => Context.Set<T>().RemoveRange(list);
        #endregion

        #region Save/General
        public virtual void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }
        }
        /// <summary>update one entity's properties from another entity or DTO</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToSet"></param>
        /// <param name="objectWithChanges"></param>
        public virtual void SetEntityProperties<T, T1>(T entityToSet, T1 objectWithChanges) where T : class where T1 : class => Context.Entry(entityToSet).CurrentValues.SetValues(objectWithChanges);

        public string getContextDatabase() => Context.Database.GetDbConnection().Database;

        public IQueryable<T> ExecuteQuery<T>(string query) where T : class => Context.Set<T>().FromSqlRaw(query);

        public void ReIndexTables() => ExecuteSqlCommand("Exec sp_msforeachtable 'SET QUOTED_IDENTIFIER ON; ALTER INDEX ALL ON ? REBUILD'");

        public int ExecuteSqlCommand(string script, params object[] parameters)
        {
            Context.Database.SetCommandTimeout(90);
            return Context.Database.ExecuteSqlRaw(script, parameters);
        }
        #endregion

        #region Unit of work
        IDbContextTransaction transaction { get; set; }
        public bool HasTransaction { get; set; } = false;

        public void BeginTransaction()
        {
            transaction = Context.Database.BeginTransaction();
            HasTransaction = true;
        }

        public virtual bool EndTransaction()
        {
            try
            {
                Save();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                RollBack();
                throw ex;
            }
            HasTransaction = false;
            return true;
        }

        public void RollBack()
        {
            transaction.Rollback();
            transaction?.Dispose();
            Context?.Dispose();
            HasTransaction = false;
        }
        public void DisposeTransaction() => Context.Database.CurrentTransaction.Dispose();
        #endregion

        public DataTable getTable(string sql)
        {
            
            SqlDataAdapter sqla = new SqlDataAdapter(sql, Context.Database.GetDbConnection().ConnectionString);
            DataTable dt = new DataTable();
            sqla.Fill(dt);
            return dt;
        }

    }
}