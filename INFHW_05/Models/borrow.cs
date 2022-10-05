using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFHW_05.Models
{
    /// <summary>
    /// Borrow model with all it's fields
    /// </summary>
    public partial class borrow
    {
        public int borrowId { get; set; }

        public int? studentId { get; set; }

        public int? bookId { get; set; }

        public DateTime? takenDate { get; set; }

        public DateTime? broughtDate { get; set; }

        public virtual book book { get; set; }

        public virtual student student { get; set; }
    }
}