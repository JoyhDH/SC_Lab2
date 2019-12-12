using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Lab2_Construction.Models
{
    public class LibraryDbInitializer : DropCreateDatabaseAlways<LibraryContext>
    {
        protected override void Seed(LibraryContext db)
        {
            if (db == null)
                Debug.Fail("Database is not initialized.");
            db.Books.Add(new Book { ID = 1, Title = "Война и мир", Author = "Лев Толстой", Available = false });
            db.Books.Add(new Book { ID = 2, Title = "Intermezzo", Author = "Kotscubinkiy", Available = true });
            db.Books.Add(new Book { ID = 3, Title = "World Encyclopedia", Author = "Co-op authors", Available = true });
            db.Books.Add(new Book { ID = 4, Title = "WEB tutorial", Author = "Co-op authors", Available = true });
            db.LibraryCards.Add(new LibraryCard { ID = 1, FirstName = "Galina", LastName = "Turish", ResevedBooks = new List<int> { 0, 1 } });
            base.Seed(db);
        }
    }
}