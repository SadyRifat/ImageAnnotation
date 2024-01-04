using ImageAnnotation.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageAnnotation.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AssetInfo> Assets { get; set; } 
    }
}
