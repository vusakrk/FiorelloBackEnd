﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.ViewModels
{
    public class RegisterVM
    {
        [Required,StringLength(200)]
        public string FullName { get; set; }
        [Required,StringLength(100)]
        public string Username { get; set; }
        [Required,EmailAddress,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string CheckPassword { get; set; }
    }
}
