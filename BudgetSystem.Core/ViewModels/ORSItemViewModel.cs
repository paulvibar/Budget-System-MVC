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
        public ORSDetailsInformation ORSDetails { get; set; }
        public UACSClass UACSClass { get; set; }
        public FundSource FundSource { get; set; }
        public FundCluster FundCluster { get; set; }
        public BoxBSignatory BoxB { get; set; }
        public ResponsibilityCenter RC { get; set; }
        public MFOPAP MFOPAP { get; set; }
        public UACS UACS { get; set; }
        public string Total { get; set; }
        public string Date { get; set; }
    }
}
