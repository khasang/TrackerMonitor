using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models.TrackersViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "IMEI")]
        public string Id { get; set; }

        [Display(Name = "Телефонный номер SIM-карты трекера")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Имя для трекера")]
        public string Name { get; set; }
    }
}