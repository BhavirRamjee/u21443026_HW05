using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFHW_05.Models
{
    /// <summary>
    /// Student model with all it's fields
    /// </summary>
    public partial class student
    {
       
        public student()
        {
            borrows = new HashSet<borrow>();
        }

        public int studentId { get; set; }

        [StringLength(20)]
        public string name { get; set; }

        [StringLength(20)]
        public string surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? birthdate { get; set; }

        [StringLength(10)]
        public string gender { get; set; }

        [Column("class")]
        [StringLength(7)]
        public string _class { get; set; }

        public int? point { get; set; }


        public virtual ICollection<borrow> borrows { get; set; }
    }
}