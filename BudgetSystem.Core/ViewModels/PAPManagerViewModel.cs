using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.ViewModels
{
    public class PAPManagerViewModel
    {
        public MFOPAP PAP { get; set; }

        public IEnumerable<Identifier> Identifiers { get; set; }
    }
}
