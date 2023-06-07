using CargoManagerSystem.Modules;
using Microsoft.EntityFrameworkCore;

namespace CargoManagerSystem.Models
{
    public class MyCargoDBContext:DbContext
    {
        public MyCargoDBContext()
        {
        }

        public MyCargoDBContext(DbContextOptions<MyCargoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CargoCategory> CargoCategories { get; set; }
        public virtual DbSet<CargoOrderDetails> CargoOderDetails { get; set; }
        public virtual DbSet<CargoOrder> CargoOrders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                    optionsBuilder.UseSqlServer("server=.\\sqlExpress;Integrated Security=true;Database=MyProjectFirstTry; TrustServerCertificate=True;");

            }
        }

    }
}
