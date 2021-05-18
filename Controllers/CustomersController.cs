using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamProject.Models;
using System.Data.SqlClient;

namespace ExamProject.Controllers
{
    public class CustomersController : Controller
    {
        private ChinookEntities db = new ChinookEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customer = db.Customer.Include(c => c.Employee);
            return View(customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.SupportRepId = new SelectList(db.Employee, "EmployeeId", "LastName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,FirstName,LastName,Company,Address,City,State,Country,PostalCode,Phone,Fax,Email,SupportRepId,turnovernew")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupportRepId = new SelectList(db.Employee, "EmployeeId", "LastName", customer.SupportRepId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupportRepId = new SelectList(db.Employee, "EmployeeId", "LastName", customer.SupportRepId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,Company,Address,City,State,Country,PostalCode,Phone,Fax,Email,SupportRepId,turnovernew")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupportRepId = new SelectList(db.Employee, "EmployeeId", "LastName", customer.SupportRepId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //---------CUSTOMER TURNOVER-------------------
        public ActionResult CustomerTurnover()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerTurnover(String date1, String date2)
        {
            int counterForNull = 0;
            String arxhTry = date1;
            String telosTry = date2;
            if (String.IsNullOrEmpty(arxhTry))
            {
                arxhTry = "1-1-2009";
                counterForNull++;
            }
            if (String.IsNullOrEmpty(telosTry))
            {
                telosTry = "1-1-2014";
                counterForNull++;
            }
            if (counterForNull > 1)
            {
                return RedirectToAction("CustomerTurnover");
            }
            using (var Context = new ChinookEntities())
            {
                var data3 = Context.Database.SqlQuery<CustomerTurnoverTry_Result>("[dbo].[CustomerTurnoverTry] @StartDateTry , @StopDateTry",
                     new SqlParameter("@StartDateTry", arxhTry),
                     new SqlParameter("@StopDateTry", telosTry)).ToList();
                TempData["data3"] = data3;
                return RedirectToAction("CustomerTurnover_show");
            }
        }
        public ActionResult CustomerTurnover_show()
        {
            var resultTry = TempData["data3"];
            if (resultTry != null)
            {
                return View(resultTry);
            }
            else
            {
                return RedirectToAction("CustomerTurnover");
            }
        }
        //---------CUSTOMER AND EMPLOYEE
        public ActionResult Customer_Employee()
        {
            var entities = new ChinookEntities();
            ViewBag.Employees = new SelectList(entities.Employee, "EmployeeId", "fullname");
            ViewBag.Customers = new SelectList(entities.Customer, "CustomerId", "fullname");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Customer_Employee(int? EmployeeID, int? CustomerID, String date1, String date2)
        {
           
           

            Customer customer = db.Customer.Find(CustomerID);
            String customerL = customer.LastName.ToString();
            String customerF = customer.FirstName.ToString();
            Employee employee = db.Employee.Find(EmployeeID);
            String employeeL = employee.LastName.ToString();
            String employeeF = employee.FirstName.ToString();
            String arxh = date1;
            String telos = date2;

           
            using (var Context = new ChinookEntities())
            {
                var data = Context.Database.SqlQuery<Customer_Employee_Result>("[dbo].[Customer_Employee] @cL , @cF , @eL , @eF ,@StartDate , @StopDate",
                    new SqlParameter("@cL", customerL),
                    new SqlParameter("@cF", customerF),
                    new SqlParameter("@eL", employeeL),
                    new SqlParameter("@eF", employeeF),

                    new SqlParameter("@StartDate", arxh),
                    new SqlParameter("@StopDate", telos)).ToList();
                TempData["data"] = data;
                return RedirectToAction("Customer_Employee_show");
            }
        }
        public ActionResult Customer_Employee_show()
        {
            var result = TempData["data"];
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("Customer_Employee");
            }
        }
    }
}
