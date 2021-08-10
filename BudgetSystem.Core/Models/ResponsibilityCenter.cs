using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class ResponsibilityCenter
    {
        [DisplayName("Responsibility Center")]
        public string Id { get; set; }
        [DisplayName("Office Name")]
        public string Name { get; set; }
        public string Acronym { get; set; }
        [DisplayName("P/A/P Code")]
        public string PAP { get; set; }
        public string Status { get; set; }

    }
}
