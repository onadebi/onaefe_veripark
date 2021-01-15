using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BusinessHolidayController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BusinessHoliday
        public ActionResult Index()
        {
            var businessHolidays = db.BusinessHolidays.Where(m => m.Active == true ).ToList();

            return View(businessHolidays ?? new List<BusinessHoliday>());
        }

        [Authorize(Users = "supervisor@veripark.test")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HolidayOccassion,FromDate,ToDate")] BusinessHoliday bizHoliday)
        {
            if (ModelState.IsValid)
            {
                db.BusinessHolidays.Add(bizHoliday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bizHoliday);
        }

        [Authorize(Users = "supervisor@veripark.test")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessHoliday holiday = db.BusinessHolidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HolidayOccassion,FromDate,ToDate,HolidayId")] BusinessHoliday holiday)
        {
            if (ModelState.IsValid)
            {
                BusinessHoliday holidayUpdate = db.BusinessHolidays.Find(holiday.HolidayId);
                holidayUpdate.HolidayOccassion = holiday.HolidayOccassion;
                holidayUpdate.FromDate = holiday.FromDate;
                holidayUpdate.ToDate = holiday.ToDate;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(holiday);
        }

        [Authorize(Roles = LibraryManagement.Models.Constants.SupervisorRoleName)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessHoliday business = db.BusinessHolidays.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }

            return View(business); // RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessHoliday bizHoliday = db.BusinessHolidays.Find(id);
            db.BusinessHolidays.Remove(bizHoliday);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}