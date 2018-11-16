using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
namespace Ecommerce.Models
{
    [Table("user", Schema = "ecommercedb")]
    public class User
    {
        // auto-implemented properties need to match columns in your table
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string firstname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string lastname { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        [NotMapped]
        public List<Order> orders { get; set; }
        public User()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
            orders = new List<Order>();
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