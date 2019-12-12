using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab2_Construction.Models
{
    public class LibraryContext: DbContext
    {
        // public LibraryContext(): base("LibraryConnection") { }

        public DbSet<Book> Books { get; set; }

        public DbSet<LibraryCard> LibraryCards { get; set; }
    }
}