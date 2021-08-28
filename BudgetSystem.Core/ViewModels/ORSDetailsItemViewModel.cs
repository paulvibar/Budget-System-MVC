using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class ORSDetailsItemViewModel
    {
        public ORSMainInformation ORSMain { get; set; }
        public ORSDetailsInformation ORSDetails { get; set; }
        public ResponsibilityCenter RC { get; set; }
        public MFOPAP MFOPAP { get; set; }
        public UACS UACS { get; set; }
        
    }
}
