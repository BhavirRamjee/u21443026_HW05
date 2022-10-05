using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFHW_05.Models
{
    /// <summary>
    /// Type model with all it's fields
    /// </summary>
    public partial class type
    {
        
        public type()
        {
            books = new HashSet<book>();
        }

        public int typeId { get; set; }

        [StringLength(30)]
        public string name { get; set; }

        public virtual ICollection<book> books { get; set; }
    }
}