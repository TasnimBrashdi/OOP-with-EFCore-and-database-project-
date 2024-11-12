using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Models
{
    [PrimaryKey(nameof(UserId), nameof(BookId))]
    public class Borrowing
    {
        [ForeignKey("UId")]
        public int UserId { get; set; }

        public virtual User UId { get; set; }
        [ForeignKey("BId")]
        public int BookId { get; set; }
        public virtual Books BId {  get; set; }
        public DateTime BorrowDate {  get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ActualDate { get; set; }    

    }
}
