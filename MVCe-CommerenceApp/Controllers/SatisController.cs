using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCe_CommerenceApp.Models.Siniflar;

namespace MVCe_CommerenceApp.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            ViewBag.satis = new SelectList(c.Uruns, "UrunId", "UrunAd");
            ViewBag.cari = new SelectList(c.Carilers, "CariID", "CariAd", "CariSoyad");
            ViewBag.prs = new SelectList(c.Personels, "PersonelID", "PersonelAd", "PersonelSoyad");
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            ViewBag.satis = new SelectList(c.Uruns, "UrunId", "UrunAd");
            ViewBag.cari = new SelectList(c.Carilers, "CariID", "CariAd", "CariSoyad");
            ViewBag.prs = new SelectList(c.Personels, "PersonelID", "PersonelAd", "PersonelSoyad");
            var deger = c.SatisHarekets.Find(id);
            return View("SatisGetir", deger);
        }
        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var deger = c.SatisHarekets.Find(p.SatisID);
            deger.cariid = p.cariid;
            deger.Adet = p.Adet;
            deger.Fiyat = p.Fiyat;
            deger.personelid = p.personelid;
            deger.Tarih = p.Tarih;
            deger.ToplamTutar = p.ToplamTutar;
            deger.urunid = p.urunid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }
       
    }
}