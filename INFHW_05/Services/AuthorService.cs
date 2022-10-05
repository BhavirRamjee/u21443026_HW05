using INFHW_05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INFHW_05.Services
{
    public class AuthorService
    {
        /// <summary>
        /// This method is used for the dropdowns
        /// It can be used anywhere in the system again as it is a static method
        /// It returns a list of authors from the database
        /// We then turn this list into selectlistitem
        /// Which will be used to display the authors in the dropdown
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetAuthors()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var authors = db.authors.ToList();

            IList<SelectListItem> items = (from a in authors
                                           select new SelectListItem
                                           {
                                               Value = ((int)a.authorId).ToString(),
                                               Text = $"{a.name} {a.surname}",
                                           }).ToList();


            items.Insert(0, (new SelectListItem { Text = "Select a Author", Value = ((int)0).ToString() }));

            return items;
        }
    }
}