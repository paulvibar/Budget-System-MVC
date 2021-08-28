using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class UACSObject : BaseEntity
    {
        public string Code { get; set; }
        [DisplayName("Object Description")]
        public string Description { get; set; }
        public int ClassificationId { get; set; }
        public int ClassId { get; set; }
        public int GroupId { get; set; }
        public string Caption { get { return Code + " - " + Description; } }
    }
}
