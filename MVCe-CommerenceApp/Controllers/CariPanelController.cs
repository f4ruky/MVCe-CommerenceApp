using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCe_CommerenceApp.Models.Siniflar;

namespace MVCe_CommerenceApp.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.m = mail;
            //ViewBag.ad = degerler.CariAd;
            //ViewBag.soyad = degerler.CariSoyad;
            //ViewBag.sehir = degerler.CariSehir;
            //ViewBag.sifre = degerler.CariSifre;


            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y=>y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.cariid == id).ToList();

            return View(degerler);
        }
    }
}