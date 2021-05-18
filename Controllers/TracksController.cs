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
    public class TracksController : Controller
    {
        private ChinookEntities db = new ChinookEntities();

        // GET: Tracks
        public ActionResult Index()
        {
            var track = db.Track.Include(t => t.Album).Include(t => t.Genre).Include(t => t.MediaType);
            return View(track.ToList());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Track.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Album, "AlbumId", "Title");
            ViewBag.GenreId = new SelectList(db.Genre, "GenreId", "Name");
            ViewBag.MediaTypeId = new SelectList(db.MediaType, "MediaTypeId", "Name");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrackId,Name,AlbumId,MediaTypeId,GenreId,Composer,Milliseconds,Bytes,UnitPrice")] Track track)
        {
            if (ModelState.IsValid)
            {
                db.Track.Add(track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Album, "AlbumId", "Title", track.AlbumId);
            ViewBag.GenreId = new SelectList(db.Genre, "GenreId", "Name", track.GenreId);
            ViewBag.MediaTypeId = new SelectList(db.MediaType, "MediaTypeId", "Name", track.MediaTypeId);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Track.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Album, "AlbumId", "Title", track.AlbumId);
            ViewBag.GenreId = new SelectList(db.Genre, "GenreId", "Name", track.GenreId);
            ViewBag.MediaTypeId = new SelectList(db.MediaType, "MediaTypeId", "Name", track.MediaTypeId);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrackId,Name,AlbumId,MediaTypeId,GenreId,Composer,Milliseconds,Bytes,UnitPrice")] Track track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Album, "AlbumId", "Title", track.AlbumId);
            ViewBag.GenreId = new SelectList(db.Genre, "GenreId", "Name", track.GenreId);
            ViewBag.MediaTypeId = new SelectList(db.MediaType, "MediaTypeId", "Name", track.MediaTypeId);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Track.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Track track = db.Track.Find(id);
            db.Track.Remove(track);
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
        //--------TOP 10 TRACKS--------
        public ActionResult Top10Tracks()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Top10Tracks(String date1, string date2)
        {
            int counterForNull = 0;
            String arxh = date1;
            String telos = date2;
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
                return RedirectToAction("Top10Tracks");
            }
            using (var context = new ChinookEntities())
            {
                var data1 = context.Database.SqlQuery<Top10Tracks_Result>("[dbo].[Top10Tracks] @StartDate , @StopDate",
                    new SqlParameter("@StartDate", arxh),
                    new SqlParameter("@StopDate", telos)).ToList();
                TempData["data1"] = data1;
                return RedirectToAction("Top10Tracks_show");
            }
        }

        public ActionResult Top10Tracks_show()
        {
            var result = TempData["data1"];
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("Top10Tracks");
            }
        }
        //--------QUARTER SALES--------
        public ActionResult quarterSales()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult quarterSales(String date)
        {

            String arxh = date;
            using (var context = new ChinookEntities())
            {
                var data6 = context.Database.SqlQuery<quarterSales_Result>("[dbo].[quarterSales] @StartDate",
                    new SqlParameter("@StartDate", arxh)).ToList();
                TempData["data6"] = data6;
                return RedirectToAction("quarterSales_show");
            }
        }
        public ActionResult quarterSales_show()
        {
            var result = TempData["data6"];
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("quarterSales");
            }
        }
    }
}
