using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class ORSItemViewModel
    {
        public ORSMainInformation ORSMain { get; set; }
        public ORSDetailsInformation ORSDetail { get; set; }
        public UACSClass UACSClass { get; set; }
        public FundSource FundSource { get; set; }
        public FundCluster FundCluster { get; set; }
        public BoxBSignatory BoxB { get; set; }

        public decimal Total { get; set; }
    }
}
