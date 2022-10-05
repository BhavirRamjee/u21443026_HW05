using INFHW_05.Models;
using INFHW_05.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace INFHW_05.Services
{
    public class BookService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get's the list of the books to be displayed on the index page.
        /// It takes the search parameters
        /// </summary>
        /// <param name="book"></param>
        /// <param name="type"></param>
        /// <param name="author"></param>
        /// <returns>Returns a whole list or a filtered list based on the filters</returns>
        public ViewBooksModel GetBooks(string book, int? type, int? author)
        {
            //initialize empty list
            ViewBooksModel model = new ViewBooksModel();

            // get the data from the database
            var result = db.books.Include(x => x.author).Include(x => x.borrows).AsNoTracking();

            // check if the string is null or empty then only filter the data
            if (!string.IsNullOrEmpty(book)) result = result.Where(x => x.name.Contains(book));
            // check if the type has value and is greator than 0  then only filter the data
            if (type.HasValue && type.Value > 0) result = result.Where(x => x.typeId == type);
            // check if the author has value and is greator than 0  then only filter the data
            if (author.HasValue && author.Value > 0) result = result.Where(x => x.authorId == author);

            // translate the data into a list and order it by book id 
            var resultData = result.Select(x => new BookViewModel
            {
                bookId = x.bookId,
                name = x.name,
                pagecount = x.pagecount,
                point = x.point,
                authorId = x.authorId,
                typeId = x.typeId,
                author = x.author.surname,
                type = x.type.name,
                status = x.borrows.Any() && x.borrows.Any(y => y.bookId.Value == x.bookId && !y.broughtDate.HasValue) ? "Out" : "Available" 
            }).OrderBy(x => x.bookId).ToList();

            // check if the data above returned is greator than 0 then add to the list initialized above
            if (resultData.Count > 0) model.bookViewModels.AddRange(resultData);

            // return the list
            return model;
        }

        /// <summary>
        /// View details of the book by bookId
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns>Returns book related date</returns>
        public ViewBooksModel ViewBookDetails(int bookId)
        {
            // intialize emptu model
            ViewBooksModel model = new ViewBooksModel();

            // validation
            if (bookId == null || bookId <= 0) return model;

            // get the data from the database
            var book = db.books.Where(x => x.bookId == bookId).FirstOrDefault();

            // validation again
            if(book == null) return model;

            // set the fields
            model.BookId = book.bookId;
            model.BookName = book.name;
            //
            model.IsBookAvailable = db.borrows.Any(x => x.bookId.Value == bookId && !x.broughtDate.HasValue) ? false : true;
            // check for the available books and translate that to a list
            var availableBooks = db.borrows.Include(x => x.student)
                                .AsNoTracking()
                                .Where(x => x.bookId == bookId)
                                .Select(x=> new BookBorrowedModel 
                                {
                                    BorrowId = x.borrowId,
                                    TakenDate = x.takenDate,
                                    BroughtDate = x.broughtDate,
                                    StudentId = x.studentId,
                                    StudentName = x.student.name ,
                                    StudentSurname = x.student.surname
                                })
                                .OrderBy(x=>x.BorrowId)
                                .ToList();

            // if we find available books, add that to the list
            if (availableBooks.Count > 0) model.booksAvailableBorroweds.AddRange(availableBooks);

            // return the model
            return model;

        }
    }
}