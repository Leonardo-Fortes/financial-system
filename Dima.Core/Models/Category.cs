﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public string UserID { get; set; } = string.Empty;
    }
}
