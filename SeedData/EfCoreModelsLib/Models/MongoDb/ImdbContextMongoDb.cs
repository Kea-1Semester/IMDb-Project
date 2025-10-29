using Microsoft.EntityFrameworkCore;

namespace EfCoreModelsLib.Models.MongoDb
{
    public class ImdbContextMongoDb : DbContext
    {
        public ImdbContextMongoDb(DbContextOptions<ImdbContextMongoDb> options)
            : base(options)
        {
        }

        public DbSet<TitleMongoDb> Titles { get; set; }
        public DbSet<PersonsMongoDb> Persons { get; set; }


    }


}
