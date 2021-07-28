using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductExample.Models
{
    public class ColorPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Color")]
        public string ColorName { get; set; }
        [Display(Name = "Precio")]
        public decimal Price { get; set; }
    }
}