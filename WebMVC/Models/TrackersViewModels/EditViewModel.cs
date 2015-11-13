using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models.TrackersViewModels
{
    public class EditViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}