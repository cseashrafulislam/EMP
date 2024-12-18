using EMP.Database;
using EMP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EMP.Controllers
{
    public class EmployeesController : Controller
    {

        private EMSDbContext db = new EMSDbContext();

        // GET: Employees
        public ActionResult Index(string search, int? departmentId, string position, int page = 1, int pageSize = 10)
        {
            var employees = db.Employees.Include(e => e.Department).Where(e => !e.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(e => e.Name.Contains(search));
            }

            if (departmentId.HasValue)
            {
                employees = employees.Where(e => e.DepartmentId == departmentId.Value);
            }

            if (!string.IsNullOrEmpty(position))
            {
                employees = employees.Where(e => e.Position == position);
            }

            var pagedEmployees = employees
                .OrderBy(e => e.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(pagedEmployees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id && !e.IsDeleted);
            if (employee == null) return HttpNotFound();

            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = db.Employees.Find(id);
            if (employee == null || employee.IsDeleted) return HttpNotFound();

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = db.Employees.Find(id);
            if (employee == null || employee.IsDeleted) return HttpNotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
