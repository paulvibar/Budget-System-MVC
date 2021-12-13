using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class RCItemViewModel
    {
        public ResponsibilityCenter RC { get; set; }
        public MFOPAP MFOPAP { get; set; }
        public int Id { get; set; }
        public string Caption { get; set; }
    }
}
