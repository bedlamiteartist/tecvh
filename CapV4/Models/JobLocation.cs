using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapModel;

namespace CapV4.Models
{
    public class JobLocation
    {
        public JobPosting jobposting { get; set; }
        public Location location { get; set; }

    }
}