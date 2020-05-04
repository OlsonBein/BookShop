using System;

namespace Store.DataAccessLayer.Repositories.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsRemoved { get; set; }
    }
}