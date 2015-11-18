using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models.TrackersViewModels
{
    public class EditViewModel
    {
        [Display(Name = "EMEI")]
        public string Id { get; set; }

        [Display(Name = "Телефонный номер")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}