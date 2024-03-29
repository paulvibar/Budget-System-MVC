﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public class ResponsibilityCenter : BaseEntity
    {
        [DisplayName("Responsibility Center Code")]
        public string Code { get; set; }
        [DisplayName("Office Name")]
        public string Name { get; set; }
        public string Acronym { get; set; }
        [DisplayName("P/A/P Code")]
        [ForeignKey("MFOPAP")]
        public int PAP { get; set; }
        public string Status { get; set; }
        public string Caption { get { return Code + " - " + Name; } }
        public MFOPAP MFOPAP { get; set; }
    }
}
