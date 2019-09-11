using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SearchFilter;

namespace SearchFilter.Controllers
{
    public class CountriesController : Controller
    {
        private dbNewEntities db = new dbNewEntities();

        // GET: Countries
        public ActionResult Index()
        {
            ViewBag.count = db.countries.Count();
            return View(db.countries.ToList());
        }

        public ActionResult Filter(string data,string type)
        {
            if (type.Equals("country"))
            {
                var result = db.countries.Where(c => c.country1.StartsWith(data));
                return Json(result.ToList(),JsonRequestBehavior.AllowGet);
            }else if (type.Equals("capital"))
            {
                var result = db.countries.Where(c => c.capital.StartsWith(data));
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            else if (type.Equals("city"))
            {
                var result = db.countries.Where(c => c.city.StartsWith(data));
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
            return new JsonResult();
        }


        public ActionResult ExtFilter(string country,string city,string capital)
        {
            var result = db.countries.Where(c => c.country1.StartsWith(country) && c.city.StartsWith(city) && c.capital.StartsWith(capital)).ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }


        public ActionResult FilterName(string data)
        {
            if (data.Equals("Pakistan"))
            {
                var result = db.countries.Where(c => c.country1.Equals(data)).ToList();
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            else if (data.Equals("Karachi"))
            {
                var result = db.countries.Where(c => c.city.Equals(data)).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if(data.Equals("Islamabad"))
            {
                var result = db.countries.Where(c => c.city.Equals(data)).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json("",JsonRequestBehavior.AllowGet);
        }


        public ActionResult Sort(string type, string order)
        {
            if (type == "country")
            {
                if (order == "desc")
                {
                    var result = db.countries.OrderByDescending(o => o.country1).ToList();
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                else if (order == "asc")
                {
                    var result = db.countries.OrderBy(o => o.country1).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else if (type == "city")
            {
                if (order == "desc")
                {
                    var result = db.countries.OrderByDescending(o => o.city).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (order == "asc")
                {
                    var result = db.countries.OrderBy(o => o.city).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else if (type == "capital")
            {
                if (order == "desc")
                {
                    var result = db.countries.OrderByDescending(o => o.capital).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (order == "asc")
                {
                    var result = db.countries.OrderBy(o => o.capital).ToList();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }

            return Json("",JsonRequestBehavior.AllowGet);
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,country1,city,capital")] country country)
        {
            if (ModelState.IsValid)
            {
                db.countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,country1,city,capital")] country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            country country = db.countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            country country = db.countries.Find(id);
            db.countries.Remove(country);
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
    }
}
