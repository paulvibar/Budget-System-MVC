using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class UACSItemViewModel
    {
        public UACS UACS { get; set; }
        public UACSObject UACSObject { get; set; }
        public UACSGroup UACSGroup { get; set; }
        public UACSClass UACSClass { get; set; }
        public UACSClassification UACSClassification { get; set; }

    }
}
