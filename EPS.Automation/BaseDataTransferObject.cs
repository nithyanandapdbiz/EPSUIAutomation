using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Automation
{
    public class BaseDataTransferObject
    {
        /// <summary>
        /// This is the Guid id og the object. This is used by DB to update the list
        /// </summary>
        private readonly Guid _guidId = Guid.NewGuid();

        public Guid GuidId
        {
            get { return _guidId; }
        }

        public DateTime CreationDate { get; set; }
    }
}