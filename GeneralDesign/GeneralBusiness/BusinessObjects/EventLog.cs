using GeneralData.Model.Entities;
using GeneralData.Repository;
using System;
using System.Collections.Generic;

namespace GeneralBusiness.BusinessObjects
{
    public class EventLog : BaseBusinessObject
    {
        public EventLog() { }

        public EventLog(IGenericRepository repo) => Repo = repo;

        public IEnumerable<eventLog> Get(DateTime startDate) => Repo.Find<eventLog>(x => x.Date >= startDate);

        /// <summary>Log the event.</summary>
        public void Insert(string description, string objectId)
        {
            eventLog newEntity = new eventLog { Date = DateTime.Now, Description = description, ObjectId = objectId };
            Repo.Insert(newEntity);
            Repo.Save();
        }
    }
}
