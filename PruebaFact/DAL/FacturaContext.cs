using PruebaFact.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PruebaFact.DAL
{
    public class FacturaContext: DbContext
    {
        public FacturaContext() : base("FacturaContext")
        {
        }

        public DbSet<Guia> Guias { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}