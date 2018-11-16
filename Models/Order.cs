using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
namespace Ecommerce.Models
{
    [Table("order", Schema = "ecommercedb")]

    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public int quantity { get; set; }
        public DateTime created_at { get; set; }
        public Order()
        {
            created_at = DateTime.Now;
        }
        public int GetTimeSpan()
        {
            DateTime start = this.created_at;
            // Do some work
            TimeSpan timeDiff = DateTime.Now - start;
            return timeDiff.Hours;

        }
    }
}