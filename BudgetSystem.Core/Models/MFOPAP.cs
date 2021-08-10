using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class MFOPAP : BaseEntity
    {
        [DisplayName("P/A/P Code")]
        public string Code { get; set; }

        [DisplayName("P/A/P Name")]
        public string Name { get; set; }

        [DisplayName("P/A/P Type")]
        public string Type { get; set; }
        public string Status { get; set; }

    }
}
