using INFHW_05.Models;
using INFHW_05.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace INFHW_05.Services
{
    public class BrorrowService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This method performs all the logic for borrowing a book
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public BorrowViewModel BorrowBook(int? bookId,int? studentId)
        {
            
            // first we validate the data
            if (bookId.Value <= 0) return new BorrowViewModel { Message = "Book not found, please try again", Success = false };

            if(studentId.Value <= 0) return new BorrowViewModel { Message = "Student not found, please try again", Success = false };

            // we then return book information based on the book id
            var book = db.books.FirstOrDefault(x => x.bookId == bookId.Value);

            // we perform validation again
            if(book == null) return new BorrowViewModel { Message = "Book not found, please try again", Success = false };

            // we then go and retrive student data based on the student id
            var student = db.students.FirstOrDefault(x => x.studentId == studentId);

            // we perform validation again
            if(student == null) return new BorrowViewModel { Message = "Student not found, please try again", Success = false };

            // we then create an object of the borrow model and pass in the required data 
            borrow b = new borrow 
            {
                bookId = book.bookId,
                studentId = student.studentId,
                takenDate = System.DateTime.Now,
                broughtDate = null
            };

            // we then add this data to the borrow table in the memeory
            db.borrows.Add(b);

            // performing save changes will insert this record  in the database in the borrow table
            db.SaveChanges();

            // we then return an object with values
            return new BorrowViewModel { Message = $"Book {book.name} has been borrowed to student {student.name} {student.surname}", Success = true };

        }

        /// <summary>
        /// THis method performs all the logic for returning a book
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        public BorrowViewModel ReturnBook(int? borrowId)
        {
            // first we vlidate the data
            if (borrowId.Value <= 0) return new BorrowViewModel { Message = "Unable to return book without borrow id, please try again", Success = false };

            // we then get the information from borrowed table using the borrow id 
            // we using include, this will return linked data to the student and the book as there are linked
            // we do not have to perform linq or sql query and join the data 
            // entity framework has the functionality to do this.
            var borrowedBook = db.borrows.Include(x=>x.student).Include(x=>x.book).FirstOrDefault(x => x.borrowId == borrowId.Value);

            // we then perform validation again
            if(borrowedBook == null) return new BorrowViewModel { Message = "Unable to return book, borrow not found, please try again", Success = false };

            // because the only field we need to update here is the brought data
            // we assign the datetime when it was it returned 
            borrowedBook.broughtDate = System.DateTime.Now;

            // we then update the data
            db.Entry(borrowedBook).State = EntityState.Modified;

            // we then do a save changes 
            db.SaveChanges();

            // we then return a success
            return new BorrowViewModel { Message = $"Book {borrowedBook.book.name} has been returned by student {borrowedBook.student.name} {borrowedBook.student.surname}", Success = true };

        }
    }
}