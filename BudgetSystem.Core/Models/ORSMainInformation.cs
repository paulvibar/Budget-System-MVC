using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class ORSMainInformation : BaseEntity
    {
        public DateTime Date { get; set; }
        public string AllotmentCode { get; set; }
        public string FundSource { get; set; }
        public string FundCluster { get; set; }
        public string Payee { get; set; }
        public string Office { get; set; }
        public string Address { get; set; }
        public string Particulars { get; set; }
        public string BoxASignatory { get; set; }
        public string BoxAPosition { get; set; }
        public string BoxBSignatory { get; set; }
        public string BoxBPosition { get; set; }
        public string Processor { get; set; }
    }
}
