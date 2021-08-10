using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Models
{
    public interface IInfo
    {
        string Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
    }
}
