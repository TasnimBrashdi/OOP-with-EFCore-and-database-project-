using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Models
{
    public class User
    {
        public enum GENDER
        {
            Male,
            Female
        }
        [Key]
        public int UID { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string UName { get; set; }
        public GENDER Gender { get; set; }
        public string Passcode { get; set; }

    }
}
