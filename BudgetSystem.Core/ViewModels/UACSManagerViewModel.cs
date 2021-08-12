using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class UACSManagerViewModel
    {
        public UACS UACS { get; set; }
        public IEnumerable<UACSObject> Objects { get; set; }
        public IEnumerable<UACSGroup> Groups { get; set; }
        public IEnumerable<UACSClass> Classes { get; set; }
        public IEnumerable<UACSClassification> Classifications { get; set; }
       
    }
}
