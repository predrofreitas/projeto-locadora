using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Locadora.Dados
{
    public class LocadoraContextFactory : IDesignTimeDbContextFactory<LocadoraContext>
    {
        public LocadoraContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocadoraContext>();
            //optionsBuilder.UseSqlServer("Password=abc,12345678;Persist Security Info=True;User ID=sa;Initial Catalog=locadoradb;Data Source=localhost");
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=locadoradb;User Id=sa;Password=abc,12345678");
            return new LocadoraContext(optionsBuilder.Options);
        }
    }

}
