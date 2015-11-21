using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models.PersonalAreaViewModels
{
    public class EditViewModel
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}