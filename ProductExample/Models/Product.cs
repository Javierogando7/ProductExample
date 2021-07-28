using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductExample.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        public List<ColorPrice> ColorPrices { get; set; }

        public Product()
        {
            Name = "";
            Description = "";
            ColorPrices = new List<ColorPrice>();
        }
    }
}