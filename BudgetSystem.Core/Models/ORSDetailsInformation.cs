using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class ORSDetailsInformation : BaseEntity
    {
        [DisplayName("ORS Number")]
        [ForeignKey("ORSMainInformation")]
        public int ORSId { get; set; }
        [DisplayName("Responsibility Center")]
        public int RCId { get; set; }
        
        [DisplayName("UACS Object Code")]
        public int UACSId { get; set; }
        public decimal Amount { get; set; }

        public ORSMainInformation ORSMainInformation { get; set; }
    }
}
