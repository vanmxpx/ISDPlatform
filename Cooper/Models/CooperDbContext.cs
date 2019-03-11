using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace Cooper.Models
{
    public class CooperDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }

        //public CooperDbContext(DbContextOptions options) : base("name = DefaultConnection")
        //{
        //}

        public CooperDbContext(DbContextOptions<CooperDbContext> options) : base(options) { }

        //public CooperDbContext() :
        //base(new OracleConnection("DATA SOURCE=localhost; PASSWORD=QAZse4321;USER ID=SYSTEM")) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder.UseSqlServer(@"User Id = system; Password = QAZse4321; " +
                "Data Source=localhost:1521/xe;");*/
            //optionsBuilder.UseSqlServer(@"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = xe))); User Id = systen; Password = QAZse4321;");
        }
    }
}
