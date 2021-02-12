using System;

namespace UContact.MyReportApi.Database.Entities
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
    }
}
