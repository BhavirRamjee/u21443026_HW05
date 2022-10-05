using INFHW_05.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INFHW_05.Controllers
{
    public class StudentsController : Controller
    {
        // We call two services here, the first is to get the students 
        private StudentService studentService = new StudentService();
        // the second contains logic for boorrowing and returning of the book
        private BrorrowService  brorrowService = new BrorrowService();
        // GET: Students

        /// <summary>
        /// This method returns a list of students 
        /// It can also be filtered using the parameters
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="className"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public ActionResult Index(string studentName, string className, int? bookId, string isBookAvailable)
        {
            var isAvailable = Convert.ToBoolean(isBookAvailable);
            var result = studentService.GetStudents(studentName, className,bookId, isAvailable);
            ViewBag.BookId = bookId;
            ViewBag.isBookAvailable = isBookAvailable;
            return View(result);
        }

      
        /// <summary>
        /// When we want to borrow a book, this method will be called
        /// Given that book id and student is provided
        /// There is validations in the service to check for null 
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ActionResult BorrowBook(int? bookId, int? studentId)
        {
            // this method performs and contains all the logic to borrow a book by a student
            var result = brorrowService.BorrowBook(bookId, studentId);

            // this is the data we pass to show a message on the home controller
            // we using tempdata in order to pass data between two controllers
            TempData["Message"] = result.Message;
            TempData["Success"] = result.Success;

            // we then redirect the user to the index page of the home controller
            return RedirectToAction("Index", "Home");
        }

       
        /// <summary>
        /// This method performs and contains all the logic for returning a book
        /// It accepts the borrowId as a paramter
        /// All validations take place in the service and will return a message depending 
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        public ActionResult ReturnBook(int? borrowId)
        {
            var result = brorrowService.ReturnBook(borrowId);
            // as mentioned in the above method
            // We create tempdata to pass data between controlllers as assign the data returned from the service to this temp data
            TempData["Message"] = result.Message;
            TempData["Success"] = result.Success;

            // we then redirect the user to the index page of the home controller which shows all the books
            // which are available or out
            return RedirectToAction("Index", "Home");
        }
    }
}