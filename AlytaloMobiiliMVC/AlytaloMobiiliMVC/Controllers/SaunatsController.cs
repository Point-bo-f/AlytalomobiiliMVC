using AlytaloMobiiliMVC.Models;
using AlytaloMobiiliMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace AlytaloMobiili.Controllers
{
    public class SaunaController : Controller


    {
        private AlytaloEntities db = new AlytaloEntities();
        private object talosauna;

        // GET: SaunaTila
        public ActionResult Index()
        {
            List<SaunaViewModel> model = new List<SaunaViewModel>();
            AlytaloEntities entities = new AlytaloEntities();
            try
            {
                List<Saunat> sau = entities.Saunat.OrderByDescending(Saunat => Saunat.SaunaId).ToList();
                foreach (Saunat saun in sau)
                {
                    SaunaViewModel view = new SaunaViewModel();
                    view.SaunaId = saun.SaunaId;
                    //view.SaunanTila = saun.SaunanTila;
                    view.NykyLampotila = saun.NykyLampotila;
                    // view.SaunaStart = saun.SaunaStart;
                    //view.SaunaStop = saun.SaunaStop;
                    view.SaunaNimi = saun.SaunaNimi;
                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);

        }
        public ActionResult SaunaOff(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saunat sauna = db.Saunat.Find(id);
            if (sauna == null)
            {
                return HttpNotFound();
            }
            SaunaViewModel view = new SaunaViewModel();
            view.SaunaId = sauna.SaunaId;
            view.SaunaNimi = sauna.SaunaNimi;


            //view.SaunaStop = sauna.SaunaStop;
            view.SaunanTila = false;



            return View(view);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaunaOff(SaunaViewModel model)
        {
            Saunat view = db.Saunat.Find(model.SaunaId);
            //view.SaunaStop = DateTime.Now;
            view.SaunaNimi = model.SaunaNimi;
            view.SaunanTila = false;

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //Sauna on //
        public ActionResult SaunaOn(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saunat sauna = db.Saunat.Find(id);
            if (sauna == null)
            {
                return HttpNotFound();
            }
            SaunaViewModel view = new SaunaViewModel();
            view.SaunaId = sauna.SaunaId;
            view.SaunaNimi = sauna.SaunaNimi;
            //view.SaunaStart = sauna.SaunaStart;
            view.SaunanTila = true;

            return View(view);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaunaOn(SaunaViewModel model)
        {
            Saunat view = db.Saunat.Find(model.SaunaId);
            //view.SaunaStart = DateTime.Now;
            view.SaunaNimi = model.SaunaNimi;
            view.SaunanTila = true;

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: SaunaTila/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saunat sauna = db.Saunat.Find(id);
            if (sauna == null)
            {
                return HttpNotFound();
            }
            return View(sauna);
        }

        // GET: SaunaTila/Create
        public ActionResult Create()
        {
            AlytaloEntities db = new AlytaloEntities();
            SaunaViewModel model = new SaunaViewModel();

            return View(model);
        }

        // POST: SaunaTila/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaunaViewModel model)
        {
            Saunat view = new Saunat();
            view.SaunaId = model.SaunaId;
            view.SaunaNimi = model.SaunaNimi;
            view.NykyLampotila = model.NykyLampotila;
            db.Saunat.Add(view);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("Index");
        }

        // GET: SaunaTila/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saunat sauna = db.Saunat.Find(id);
            if (sauna == null)
            {
                return HttpNotFound();
            }
            return View(sauna);
        }

        // POST: SaunaTila/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaunaId,SaunanTila,SaunanNykyLampotila,SaunaStart,SaunaStop,SaunanNimi")] Saunat sauna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sauna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sauna);
        }

        // GET: SaunaTila/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saunat sauna = db.Saunat.Find(id);
            if (sauna == null)
            {
                return HttpNotFound();
            }
            return View(sauna);
        }

        // POST: SaunaTila/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Saunat sauna = db.Saunat.Find(id);
            db.Saunat.Remove(sauna);
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