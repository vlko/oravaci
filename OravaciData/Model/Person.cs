using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OravaciData.Model
{
    public class Person
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "- chýbajúce Meno a Priezvisko")]
        public string FullName { get; set; }

        public string Occupation { get; set; }

        public string Img { get; set; }

        public TimeEvent[] TimeData { get; set; }
    }
}
