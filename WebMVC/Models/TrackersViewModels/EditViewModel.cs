using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models.TrackersViewModels
{
    public class EditViewModel
    {

        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "EMEI")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Телефонный номер")]
        public string PhoneNumber { get; set; }
    }
}