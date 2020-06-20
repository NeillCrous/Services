using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace GeneralData.Repository
{
    public interface IGenericRepository
    {
        void ChangeDatabase(string glasDatabaseName);

        IEnumerable<T> Get<T>(
        Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "", bool eagerLoad = false, IQueryable<T> query = null) where T : class;

        IQueryable<T> Find<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        TResult GetMaxBy<T, TResult>(Expression<Func<T, TResult>> maxField = null, Expression<Func<T, bool>> predicate = null) where T : class;
        T GetByKey<T>(params object[] key) where T : class;
        void Insert<T>(T obj) where T : class;
        void BulkInsert<T>(IEnumerable<T> query) where T : class;
        /// <summary>Create a new entity from DTO</summary>
        /// 
        /// 
        /// 
        /// <typeparam name="T">Entity Type</typeparam>
        /// <typeparam name="T1">DTO Type</typeparam>
        /// <param name="objectWithChanges">DTO object</param>
        T InsertFromObject<T, T1>(T1 objectWithChanges) where T : class where T1 : class;
        void Update<T>(T obj) where T : class;
        void SetEntityProperties<T, T1>(T entityToSet, T1 objectWithChanges) where T : class where T1 : class;
        void UpdateEntity<T>(T obj, int id) where T : class;
        /// <summary>Update entity from another entity or DTO</summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <typeparam name="T1">object with changes'type</typeparam>
        /// <param name="objectWithChanges">source entity or DTO with changed properties</param>
        /// <param name="key">Primary key of entity</param>
        void UpdateFromObject<T, T1>(T1 objectWithChanges, params object[] key) where T : class where T1 : class;
        void Delete<T>(object id) where T : class;
        void Delete<T>(T entityToDelete) where T : class;
        void DeleteByKey<T>(params object[] key) where T : class;
        void DeleteRange<T>(Expression<Func<T, bool>> predicate) where T : class;
        void DeleteRangeByList<T>(IEnumerable<T> list) where T : class;
        void Save();
        void DisposeTransaction();
        string getContextDatabase();
        IQueryable<T> ExecuteQuery<T>(string query) where T : class;
        void ReIndexTables();
        int ExecuteSqlCommand(string script, params object[] parameters);
        DataTable getTable(string sql);

        #region Transactions
        void BeginTransaction();
        bool EndTransaction();
        void RollBack();
        #endregion
    }
}