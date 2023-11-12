using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MintoStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public int SizeId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public int Quantity { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        [ForeignKey("SizeId")]
        public virtual Size? Size { get; set; } 



    }
}
