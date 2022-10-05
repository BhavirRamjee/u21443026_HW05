using INFHW_05.Services;
using INFHW_05.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INFHW_05.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// We are calling the book service to get the data
        /// </summary>
        private readonly BookService ReturnBookService = new BookService();

        /// <summary>
        /// We are returning list of books by using GetBooks
        /// Then we do a check if the bookId has a value
        /// If the bookId has a value we return a different set of data to display the book details
        /// </summary>
        /// <param name="book"></param>
        /// <param name="type"></param>
        /// <param name="author"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public ActionResult Index(string book, int? type, int? author, int? bookId)
        {
            ViewBooksModel model = new ViewBooksModel();

            if (!bookId.HasValue)
                model = ReturnBookService.GetBooks(book, type, author);
            else if (bookId.HasValue)
                model = ReturnBookService.ViewBookDetails(bookId.Value);

            // we initialize empty fields to be used for success messages 
            var message = "";
            var success = false;
            // this tempdata is coming from students controller after We borrow or return book
            // This data will be null when we run the system and therefore we have to do this check
            if (TempData["Message"] != null && TempData["Success"] != null)
            {
                // we then assign the data if the data was found 
                message = TempData["Message"].ToString();

                string succ = TempData["Success"].ToString();

                // because it's a type of bool, we convert the data to bool
                success = Convert.ToBoolean(succ);
            }

            // we then return message with type of book in a viewbag
            ViewBag.Message = message;
            ViewBag.Success = success;

            // we also return the data to be displayed on the index page depending on the book or book details
            return View(model);
        }


    }
}