using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Models
{
    public class Books
    {
        [Key]
        public int BId { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string BName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(2, 1000)]
        public int Copies { get; set; }
        [Range(2, 100)]
        public float Price { get; set; }    
        public int Period { get; set; }

        [ForeignKey("CatID")]
        public int? CategoryID { get; set; } 

        public virtual Category CatID { get; set; }

   

    }
}
