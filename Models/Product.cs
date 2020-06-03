using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductAmount { get; set; }
        public string ProductDescription { get; set; }
        //public string ProductDescription { get; set; }
    }
}
