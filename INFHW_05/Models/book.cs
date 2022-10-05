using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFHW_05.Models
{
    /// <summary>
    /// Book model with all it's fields
    /// </summary>
    public partial class book
    {
      
        public book()
        {
            borrows = new HashSet<borrow>();
        }

        public int bookId { get; set; }

        [StringLength(90)]
        public string name { get; set; }

        public int? pagecount { get; set; }

        public int? point { get; set; }

        public int? authorId { get; set; }

        public int? typeId { get; set; }

        public virtual author author { get; set; }

        public virtual type type { get; set; }

        public virtual ICollection<borrow> borrows { get; set; }
    }
}