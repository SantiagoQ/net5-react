using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Core.Auxiliaries.Entities
{
    public interface ICreationAudit
    {
        public DateTime CreationTime { get; set; }
        //public int CreatorUserId { get; set; }
    }
}
