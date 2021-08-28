using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class RCManagerViewModel
    {
        public ResponsibilityCenter RC { get; set; }
        public IEnumerable<MFOPAP> PAPs { get; set; }
        public IEnumerable<Identifier> Identifiers { get; set; }

    }
}
