using Microsoft.EntityFrameworkCore;

namespace Personal_Library.Models
{
    public class PersonalBookCollectionContext : DbContext
    {
        public PersonalBookCollectionContext(DbContextOptions<PersonalBookCollectionContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookTranslator> BookTranslators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Collections 複合主鍵
            modelBuilder.Entity<Collection>()
                .HasKey(c => new { c.UserID, c.ISBN });

            // Books 與 Publishers 關聯
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherID);

            // Collections 與 Users, Books 關聯
            modelBuilder.Entity<Collection>()
                .HasOne(c => c.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<Collection>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Collections)
                .HasForeignKey(c => c.ISBN);

            // Books 與 Authors 多對多
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.ISBN, ba.AuthorID });

            // Books 與 Categories 多對多
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.ISBN, bc.CategoryID });

            // Books 與 Translators 多對多
            modelBuilder.Entity<BookTranslator>()
                .HasKey(bt => new { bt.ISBN, bt.TranslatorID });

            // Categories 自關聯
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(c => c.ParentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
