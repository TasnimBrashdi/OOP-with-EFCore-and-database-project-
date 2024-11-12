using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Models
{
    public class Category
    {
        [Key]
        public int CId { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string CName { get; set; }
        public int NoBooks { get; set; }

        public virtual ICollection<Books> Books { get; set; }

    }
}
