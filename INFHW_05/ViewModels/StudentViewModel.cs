using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFHW_05.ViewModels
{
    public class StudentViewModel
    {
        public int studentId { get; set; }


        public string name { get; set; }


        public string surname { get; set; }


        public DateTime? birthdate { get; set; }


        public string gender { get; set; }


        public string _class { get; set; }

        public int? point { get; set; }

        public bool IsReturnBook { get; set; }

        public int? bookId { get; set; }

        public int? borrowId { get; set; }
    }
}