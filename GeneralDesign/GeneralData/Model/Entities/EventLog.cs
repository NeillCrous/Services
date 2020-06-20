using System;
using System.Collections.Generic;

namespace GeneralData.Model.Entities
{
    public partial class eventLog
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string ObjectId { get; set; }
    }
}
