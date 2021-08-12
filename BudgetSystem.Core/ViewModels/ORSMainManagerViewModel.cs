using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class ORSMainManagerViewModel
    {
        public ORSMainInformation ORSMain { get; set; }
        public IEnumerable<UACSClass> AllotmentClass { get; set; }
        public IEnumerable<FundSource> FundSource { get; set; }
        public IEnumerable<FundCluster> FundCluster { get; set; }
        public IEnumerable<BoxBSignatory> BoxB { get; set; }
    }
}
