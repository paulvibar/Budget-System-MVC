using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class UACSGroup : BaseEntity
    {
        public string Code { get; set; }
        [DisplayName("Group Description")]
        public string Description { get; set; }
        public int ClassificationId { get; set; }
        public int ClassId { get; set; }
        public string Caption { get { return Code + " - " + Description; } }
    }
}
