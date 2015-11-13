using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models.TrackersViewModels
{
    public class RegisterViewModel
    {
        [Display( Name = "Название")]
        public string Name { get; set; }

        [Display( Name = "Номер SIM-карты трекера")]
        public string PhoneNumber { get; set; }
    }
}