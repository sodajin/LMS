using LibraryManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Data
{
    public class DataContexts : DbContext
    {
        //public DbSet<User> User { get; set; }
        //public DbSet<Book> Book { get; set; }
        //public DbSet<RequestedBookDTO> RequestedBooks { get; set; }
        //public DbSet<BorrowedBookDTO> BorrowedBooks { get; set; }

        public DbSet<Library> Library { get; set; }
        public DbSet<UserList> UserList { get; set; }

        public string Path = @"C:/LMS.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={Path}");
        
    }
}
