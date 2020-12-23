using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewHow.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public UserProfile Reviewer { get; set; }
    }
}