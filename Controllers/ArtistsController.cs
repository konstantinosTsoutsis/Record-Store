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
    public class ArtistsController : Controller
    {
        private ChinookEntities db = new ChinookEntities();

        // GET: Artists
        public ActionResult Index()
        {
            return View(db.Artist.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artist.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistId,Name")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artist.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artist.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistId,Name")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artist.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artist.Find(id);
            db.Artist.Remove(artist);
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
        // ------------METHOD ARTIST BEST ALBUM---------------
        public ActionResult ArtistBestAlbum()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArtistBestAlbum(int? numberOfArtists, String date1, string date2)
        {
            int counterForNull = 0;
            int? count = numberOfArtists;
            String arxh = date1;
            String telos = date2;
            if (count == null)
            {
                count = 275;
                counterForNull++;
            }
            if (String.IsNullOrEmpty(arxh))
            {
                arxh = "1-1-2009";
                counterForNull++;
            }
            if (String.IsNullOrEmpty(telos))
            {
                telos = "1-1-2014";
                counterForNull++;
            }
            if (counterForNull > 1)
            {
                return RedirectToAction("ArtistBestAlbum");
            }
            using (var context = new ChinookEntities())
            {
                var data = context.Database.SqlQuery<BestArtistsAlbums_Result>("[dbo].[BestArtistsAlbums] @x ,@StartDate , @StopDate",
                    new SqlParameter("@x", count),
                    new SqlParameter("@StartDate", arxh),
                    new SqlParameter("@StopDate", telos)).ToList();
                TempData["data"] = data;
                return RedirectToAction("ArtistBestAlbum_show");
            }
        }
        public ActionResult ArtistBestAlbum_show()
        {
            var result = TempData["data"];
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("ArtistBestAlbum");
            }
        }
    }
}
