using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UContact.MyContactApi.Database.Entities
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
    }
}
