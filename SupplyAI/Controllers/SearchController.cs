﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplyAI.Models;

namespace SupplyAI.Controllers
{
    public class SearchController : Controller
    {
        //Where database refrerences would be stored.
        //Since we aren't connecting to a database, we create our own termoprary one to make things look cool
        //Where we will initialize our database stuff
        private DataSetDictionary Database => Startup.Database; //alias the name for convenience

        public DataSetDictionary SearchResults;
        public int hi = 0;

        // GET: Search/Index or Search
        public ActionResult Index()
        {
            //must calculate total number of results
            SearchResults = Database;


            ViewBag.Title =  SearchResults.Count() +" Search Results";


            return View(this);
        }

        // GET: Search/Tag/{name}
        [Route("Tag/{name}")]
        public ActionResult Tag(string name)
        {
            var results= Database.Where(x => x.Tags.ContainsKey(name)); //find each database entry with a tag with key name
            //the .ToDictionary method loops through each Value in results, and uses the input function to generate the key
            //in other words, for each DataSet value 'a', the key is a.id
            SearchResults = (DataSetDictionary) results.ToDictionary(a => a.ID); 
            return View("Index",this); //we need to specify 'Index', because otherwise it will look for the 'Tag' View
        }

        // GET: Search/Author/{name}
        [Route("Author/{name}")]
        public ActionResult Author(string name)
        {
            //2nd verse, same as the first. See above method for explanation
            var results = Database.Where(
                x => {
                    return x.Authors.Contains(name);
                }); //find each database entry with a tag with key name           
            SearchResults = (DataSetDictionary)results.ToDictionary(a => a.ID);
            return View("Index",this);
        }

        // POST: DataSet/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
