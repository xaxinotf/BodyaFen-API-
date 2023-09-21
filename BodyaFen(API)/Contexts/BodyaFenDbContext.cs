using BodyaFen_API_.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BodyaFen_API_.Contexts
{
    public class BodyaFenDbContext : DbContext
    {
        public BodyaFenDbContext(DbContextOptions<BodyaFenDbContext> options)
        : base(options)
        {


        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Song> Songs { get; set; }

    }
}
