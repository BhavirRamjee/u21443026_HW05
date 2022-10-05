using INFHW_05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INFHW_05.Services
{
    public class TypeService
    {
        /// <summary>
        /// This method is used for the dropdowns
        /// It can be used anywhere in the system again as it is a static method
        /// It returns a list of types from the database
        /// We then turn this list into selectlistitem
        /// Which will be used to display the types in the dropdown
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetTypes()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var types = db.types.ToList();

            IList<SelectListItem> items = (from t in types
                                           select new SelectListItem
                                           {
                                               Value = ((int)t.typeId).ToString(),
                                               Text = t.name.ToString(),

                                           }).ToList();


            items.Insert(0, (new SelectListItem { Text = "Select a Type", Value = ((int)0).ToString() }));

            return items;
        }
    }
}