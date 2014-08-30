using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Web.Domains
{
    public class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}