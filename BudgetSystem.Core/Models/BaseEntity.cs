﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.Core.Models
{
    public abstract class BaseEntity
    {
        
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


    }
}
