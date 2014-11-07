﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AtomConfiguratorModel.Models
{
  public class LoginModel
  {
    [Required(ErrorMessage = "Please enter User Name")]
    [Display(Name = "User name")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Please enter Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
  }
}