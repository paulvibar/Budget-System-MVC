using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class UACSClassification : BaseEntity
    {
        public string Code { get; set; }
        [DisplayName("Classification Description")]
        public string Description { get; set; }
        public string Caption { get { return Code + " - " + Description; } }
    }
}
