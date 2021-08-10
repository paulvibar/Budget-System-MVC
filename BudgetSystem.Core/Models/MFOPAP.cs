using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class MFOPAP
    {
        [DisplayName("P/A/P Code")]
        public string Id { get; set; }

        [DisplayName("P/A/P Name")]
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
