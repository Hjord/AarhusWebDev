using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class Message
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Msg { get; set; }

    }
}