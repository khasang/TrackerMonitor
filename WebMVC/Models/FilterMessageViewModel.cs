using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class FilterMessageViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime FirstDate { get; set; }

        [Required]
        public DateTime SecondDate { get; set; }

        public ICollection<GPSTrackerMessage> Messages { get; set; }

    }
}