using Microsoft.EntityFrameworkCore;
using PawsomeAdoptBackEnd.Entitites;

namespace PawsomeAdoptBackEnd.Context
{

    public class PawsomeContext : DbContext
    {
        public PawsomeContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=tcp:pawsomeadoptbackenddbserver.database.windows.net,1433;Initial Catalog=PawsomeAdoptBackEnd_db;User Id=gurlinkaur01@pawsomeadoptbackenddbserver;Password=Kaurgurlin15+");
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
