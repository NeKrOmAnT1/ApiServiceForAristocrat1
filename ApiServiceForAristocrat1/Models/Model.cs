using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiServiceForAristocrat1.Models
{
    public class Model
    {
        public class User
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int? Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        } 
        public class Admin
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int? Id { get; set; }
            public string? FIO { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class Product
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int? Id { get; set; }
            public string Productname { get; set; }
            public decimal Productprice { get; set; }
            public string Productdescription { get; set; }
            public decimal Productsize { get; set; }
            public byte[] ProductImage { get; set; }
        }
    }
}
