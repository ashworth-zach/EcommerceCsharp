using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
namespace Ecommerce.Models
{
    [Table("product", Schema = "ecommercedb")]

    public class Product
    {
        [Key]
        public int ProductId{get;set;}
        public int quantity{get;set;}
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string description{get;set;}
        public string name{get;set;}
        public string image{get;set;}
        public float price{get;set;}
        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
        public List<Order> orders {get;set;}
        public Product(){
            created_at=DateTime.Now;
            updated_at=DateTime.Now;
            orders = new List<Order>();

        }
    }
}