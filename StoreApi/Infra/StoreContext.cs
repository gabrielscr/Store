using Microsoft.EntityFrameworkCore;
using StoreApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Infra
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>();

            modelBuilder.Entity<Produto>(opts =>
            {
                opts.Property(p => p.Ativo).HasDefaultValue(true);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
