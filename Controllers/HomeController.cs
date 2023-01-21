using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        private KurumsalWebDB db = new KurumsalWebDB();
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmet.OrderByDescending(x=>x.HizmetId);
            return View();
        }
        public ActionResult SliderPartial()
        {

            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderID));
        }
        public ActionResult HizmetPartial()
        {
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Iletisim(string isim=null, string email = null, string konu = null, string mesaj = null)
        {
            if (isim!=null && email != null && konu != null && mesaj != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName= "chapqin09@gmail.com";
                WebMail.Password= "password";
                WebMail.SmtpPort = 587;
                WebMail.Send("chapqin09@gmail.com", konu,email+"<br>"+ mesaj);
                ViewBag.Uyari = "Mesajınız gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Bir hata oluştu. Tekrar deneyiniz.";
            }
            return View();
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmet.OrderByDescending(x => x.HizmetId);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            return PartialView();
        }
        
    }
}