using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class ORSDetailsManagerViewModel
    {
        public ORSDetailsInformation ORSdetails { get; set; }
        public IEnumerable<RCItemViewModel> RCs { get; set; }
        public IEnumerable<UACS> UACS { get; set; }
        public IEnumerable<ResponsibilityCenter> RC { get; set; }
        public IEnumerable<MFOPAP> PAP { get; set; }
        public int ORSNumber { get; set; }
    }
}
