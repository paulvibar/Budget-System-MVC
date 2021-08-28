using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class UACSClass : BaseEntity
    {
        public string Code { get; set; }
        [DisplayName("Class Description")]
        public string Description { get; set; }
        public string ClassificationId { get; set; }
        public string AllotmentCode { get; set; }
        public string Caption { get {return AllotmentCode + " - " + Description;}}
    }
}
