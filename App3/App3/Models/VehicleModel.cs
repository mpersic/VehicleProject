using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Models
{
    public class VehicleModel : IInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public string MakeId { get; set;  }
    }
}
