using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace INFHW_05.Models
{
    /// <summary>
    /// This is the database context class
    /// We have created the database using the scripts provided
    /// </summary>
    public partial class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// In the web config, we are calling this DefaultConnection
        /// If we chnage the name here we must change the name in the web config file
        /// </summary>
        public ApplicationDbContext()
           : base("name=DefaultConnection")
        {
        }

        /// <summary>
        /// By doing this, we are connecting the database to our system
        /// This can then be queried
        /// </summary>
        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<borrow> borrows { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<type> types { get; set; }

        /// <summary>
        /// This is fluent api validation.
        /// If we connect the database using ADO.NET data model, this get's auto generated
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<author>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<author>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<book>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e._class)
                .IsUnicode(false);

            modelBuilder.Entity<type>()
                .Property(e => e.name)
                .IsUnicode(false);
        }
    }
}