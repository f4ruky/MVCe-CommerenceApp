﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCe_CommerenceApp.Models.Siniflar;

namespace MVCe_CommerenceApp.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            //    List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
            //                                   select new SelectListItem
            //                                   {

            //                                       Text = x.KategoriAd,
            //                                       Value = x.KategoriID.ToString()

            //                                   }).ToList();
            //    ViewBag.dgr = deger1;
            ViewBag.kategoriAd = new SelectList(c.Kategoris, "Kategoriid", "KategoriAd");



            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun param)
        {
            c.Uruns.Add(param);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urn = c.Uruns.Find(p.UrunId);
            urn.AlisFiyat = p.AlisFiyat;
            urn.Durum = p.Durum;
            urn.kategoriid = p.kategoriid;
            urn.Marka = p.Marka;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Stok = p.Stok;
            urn.UrunAd = p.UrunAd;
            urn.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            ViewBag.prs = new SelectList(c.Personels, "PersonelID", "PersonelAd", "PersonelSoyad");
            var deger1 = c.Uruns.Find(id);
            ViewBag.dgr1 = deger1.UrunId;
            ViewBag.dgr2 = deger1.SatisFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
            
        }
       

    }
}