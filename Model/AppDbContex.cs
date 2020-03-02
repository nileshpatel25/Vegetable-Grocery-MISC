using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vegetable_Grocery_MISC.Model
{
  public class AppDbContex: IdentityDbContext<Applicationuser>
  {
    public AppDbContex(DbContextOptions<AppDbContex> options) : base(options)
    {

    }
    public DbSet<City> Cities { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<Unit> Units { get; set; }

    public DbSet<Product> Products { get; set; }
    



    public DbSet<ordermaster> Ordermasters { get; set; }
    public DbSet<orderdetails> Orderdetails { get; set; }
    public DbSet<TaxSlabMaster> TaxSlabMasters { get; set; }
  }
}
