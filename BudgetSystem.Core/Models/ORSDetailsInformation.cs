using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class ORSDetailsInformation : BaseEntity
    {
        public int ORSId { get; set; }
        public int RCId { get; set; }
        public int PAPId { get; set; }
        public int UACSId { get; set; }
        public decimal Amount { get; set; }
    }
}
