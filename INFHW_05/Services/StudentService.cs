using INFHW_05.Models;
using INFHW_05.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INFHW_05.Services
{
    public class StudentService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// It is used for drop down list.
        /// Get's all the classes from the students table
        /// </summary>
        /// <returns>List of  distinct  classes from students table</returns>
        public static IEnumerable<SelectListItem> GetClasses()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var students = db.students.ToList();

            IList<SelectListItem> items = (from a in students
                                           select new SelectListItem
                                           {
                                               Value = a._class.ToString(),
                                               Text = a._class.ToString(),
                                           }).Distinct().ToList();


            items.Insert(0, (new SelectListItem { Text = "Select a Class", Value = ((int)0).ToString() }));

            return items;
        }

        /// <summary>
        /// Can filter by student and class name on this method
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="className"></param>
        /// <returns>List of students</returns>
        public List<StudentViewModel> GetStudents(string studentName, string className, int? bookId, bool isBookAvailable)
        {
            // initialize empty list
            List<StudentViewModel> model = new List<StudentViewModel>();

            // get the data from the database
            // it is an IQueryable which means we can filter data on it 
            
           var borrows = db.borrows.Include(x => x.student).Include(x => x.book).Where(x => x.bookId.Value == bookId.Value).AsNoTracking();


            // we validate if the book is available then it will return avaiable borrow data else it will return the data for the book brrowed
            // only filter for student name if string is not null or empty
            if (!string.IsNullOrEmpty(studentName) && isBookAvailable)
            {
                borrows = borrows.Where(x => x.student.name == studentName || x.student.surname == studentName);

            }
            else if (!string.IsNullOrEmpty(studentName) && !isBookAvailable)
            {

                borrows = borrows.Where(x => (x.student.name == studentName || x.student.surname == studentName) && !x.broughtDate.HasValue);
            }

            // only filter for class name if string is not null or empty
            if (!string.IsNullOrEmpty(className) && className != "0" && isBookAvailable)
            {
                borrows = borrows.Where(x => x.student._class == className);
            }
            else if (!string.IsNullOrEmpty(className) && className != "0" && !isBookAvailable)
            {
                borrows = borrows.Where(x => x.student._class == className && !x.broughtDate.HasValue);
            }


            // translate the data to a list and select the fields needed to display on the front end using a view model
            // it is ordered by student id
            var borrowedBooks = borrows.ToList();

            foreach (var x in borrowedBooks)
            {
                model.Add(new StudentViewModel
                {
                    studentId = x.student.studentId,
                    name = x.student.name,
                    surname = x.student.surname,
                    _class = x.student._class,
                    point = x.student.point,
                    IsReturnBook = x.broughtDate.HasValue || x.broughtDate != null ? false : true,
                    bookId = x.bookId,
                    borrowId = x.borrowId
                });
            }


            // return the list
            return model;

        }
    }
}