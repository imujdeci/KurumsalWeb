using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class KimlikController : Controller
    {
        KurumsalWebDB db = new KurumsalWebDB();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());
        }



        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x=>x.KimlikId== id).SingleOrDefault();   
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik,HttpPostedFileBase LogoUrl)
        {
            if(ModelState.IsValid) 
            {
                var k = db.Kimlik.Where(x => x.KimlikId == id).SingleOrDefault();
                if(LogoUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.LogoUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.LogoUrl));
                    }
                    WebImage img = new WebImage(LogoUrl.InputStream);
                    FileInfo imginfo = new FileInfo(LogoUrl.FileName);

                    string logoName = LogoUrl.FileName+ imginfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Kimlik/" + logoName);

                    k.LogoUrl= "/Uploads/Kimlik/" + logoName;
                }
                k.Title = kimlik.Title;
                k.Keywords= kimlik.Keywords;
                k.Description= kimlik.Description;
                k.Unvan= kimlik.Unvan;
                db.SaveChanges();
                return RedirectToAction("Index");   

            }
            return View(kimlik);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kimlik kimlik = db.Kimlik.Find(id);
            if (kimlik == null)
            {
                return HttpNotFound();
            }
            return View(kimlik);
        }

        // POST: Kimlik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kimlik kimlik = db.Kimlik.Find(id);
            db.Kimlik.Remove(kimlik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

    internal class KurumsalWebDBEntities
    {
        public object Kategori { get; internal set; }
        public IEnumerable<object> Admin { get; internal set; }
    }
}
