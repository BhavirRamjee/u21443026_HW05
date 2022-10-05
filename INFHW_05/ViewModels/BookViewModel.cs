using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFHW_05.ViewModels
{
    public class BookViewModel
    {
        // fields with default values
        public int bookId { get; set; } = 0;

        public string name { get; set; } = string.Empty;

        public int? pagecount { get; set; } = 0;

        public int? point { get; set; } = 0;

        public int? authorId { get; set; } = 0;

        public int? typeId { get; set; } = 0;

        public string author { get; set; } = string.Empty;

        public string type { get; set; } = string.Empty;

        public string status { get; set; } = string.Empty;
    }

    public class ViewBooksModel
    {
        // default values 
        public string BookName { get; set; } = string.Empty;
        public int? BookId { get; set; } = 0;

        public bool IsBookAvailable { get; set; } = false;

        // initialize empty lists
        public List<BookBorrowedModel> booksAvailableBorroweds { get; set; } = new List<BookBorrowedModel>();
     
        public List<BookViewModel> bookViewModels { get; set; } = new List<BookViewModel>();

    }

    public class BookBorrowedModel
    {
        public int BorrowId { get; set; } = 0;
        public DateTime? TakenDate { get; set; }
        public DateTime? BroughtDate { get; set; }
        public int? StudentId { get; set; } = 0;
        public string StudentName { get; set; } = string.Empty;

        public string StudentSurname { get; set; } = string.Empty;
    }
}