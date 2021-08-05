using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Locadora.Dados
{
    public class LocadoraContextFactory : IDesignTimeDbContextFactory<LocadoraContext>
    {
        public LocadoraContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocadoraContext>();
            optionsBuilder.UseSqlServer("Password=Projeto@Locadora#2021;Persist Security Info=True;User ID=sa;Initial Catalog=locadoradb;Data Source=192.168.0.111");
            return new LocadoraContext(optionsBuilder.Options);
        }
    }

}
