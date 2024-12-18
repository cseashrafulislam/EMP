using EMP.Database;
using EMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMP.Controllers
{
    public class PerformanceReviewsController : Controller
    {
        private EMSDbContext db = new EMSDbContext();

        // GET: PerformanceReviews
        public ActionResult Index()
        {
            var reviews = db.PerformanceReviews
                            .Include("Employee")
                           //.Include(r => r.Employee)
                            .ToList();
            return View(reviews);
        }

        // GET: PerformanceReviews/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: PerformanceReviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PerformanceReview review)
        {
            if (ModelState.IsValid)
            {
                db.PerformanceReviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", review.EmployeeId);
            return View(review);
        }
    }
}