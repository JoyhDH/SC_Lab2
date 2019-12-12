using Lab2_Construction.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab2_Construction.Controllers
{
    public class HomeController : BaseController
    {
        LibraryContext db = new LibraryContext();

        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;
            return View(books);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        public ActionResult ReadersList()
        {
            IEnumerable<LibraryCard> cards = db.LibraryCards;
            return View(cards);
        }

        public ActionResult RegisterNewCard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNewCard(LibraryCard newCard)
        {
            if (ModelState.IsValid)
            {
                newCard.ID = db.LibraryCards.ToList().Count;
                db.LibraryCards.Add(newCard);
                db.SaveChanges();
                ViewData["Success"] = "Reader " + newCard.LastName + " " + newCard.FirstName + " was registered successfully.";
                return RedirectToAction("Index"); //"RegisterNewCardSuccess/" + newCard.ID
            }
            return View(newCard);
        }

        //[HttpGet]
        //public ActionResult ReserveBook1(int ID)
        //{
        //    LibraryCard card = new LibraryCard();
        //    db.Books.Find(ID).Availabe = false;
        //    var result = db.LibraryCards.Find(card.ID);//.ResevedBooks.Add(BookID);
        //    db.SaveChanges();
        //    return View(result);
        //        //$"Book {db.Books.Find(ViewBag.ID).Title} reserved by " +
        //        //$"{db.LibraryCards.Find(Card).FirstName} {db.LibraryCards.Find(Card).LastName} successfully.";
        //}

        public ActionResult ReserveBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryCard card = db.LibraryCards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        [HttpPost]
        public ActionResult ReserveBook(LibraryCard libraryCard)
        {
            bool check = true;
            for (int i = 0; i < db.LibraryCards.ToList().Count; i++)
            {
                if (db.LibraryCards.Find(i) != null && db.LibraryCards.Find(i).reservedBook == libraryCard.reservedBook)
                    check = false;
            }
            if (check != false)
            {
               // libraryCard.ResevedBooks.Add(libraryCard.reservedBook);
                // db.LibraryCards.Find(libraryCard.ID).ResevedBooks.Add(libraryCard.reservedBook);
                db.Entry(db.LibraryCards.Find(libraryCard.ID)).State = EntityState.Modified;
                db.Books.Find(libraryCard.reservedBook).Available = false;
                db.Entry(db.Books.Find(libraryCard.reservedBook)).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(libraryCard);
            /* $"Book {libraryCard.reservedBook.Title} reserved by " +
                     $"{libraryCard.FirstName} {libraryCard.LastName} successfully."; */
        }

        public ActionResult SearchReader(int ID)
        {
            ViewBag.ID = db.LibraryCards.Find(ID);
            return View();
        }

        public ActionResult ReceptionBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryCard card = db.LibraryCards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        [HttpPost]
        public ActionResult ReceptionBook(LibraryCard Card)
        {
            if (ModelState.IsValid)
            {
                db.LibraryCards.Find(Card.ID).ResevedBooks.Remove(Card.reservedBook);
                db.Entry(db.LibraryCards.Find(Card.ID)).State = EntityState.Modified;
                db.Books.Find(Card.reservedBook).Available = true;
                db.Entry(db.Books.Find(Card.reservedBook)).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Card);
        }
    }
}