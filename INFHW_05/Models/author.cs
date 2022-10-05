using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFHW_05.Models
{
    /// <summary>
    /// Author model with all it's fields
    /// </summary>
    public partial class author
    {
        public author()
        {
            books = new HashSet<book>();
        }

        public int authorId { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(70)]
        public string surname { get; set; }

        public virtual ICollection<book> books { get; set; }

    }
}