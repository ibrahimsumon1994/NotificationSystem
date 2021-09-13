﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WundermanThompson.Model
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
