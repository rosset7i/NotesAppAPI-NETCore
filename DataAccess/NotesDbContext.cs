using Microsoft.EntityFrameworkCore;
using NotesAppAPI___ASP.Models.Domain;

namespace NotesAppAPI___ASP.DataAccess
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Note> Notes { get; set; }
    }
}
