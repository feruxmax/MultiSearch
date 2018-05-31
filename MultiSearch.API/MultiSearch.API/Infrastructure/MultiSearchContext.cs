using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using MultiSearch.API.Models;

namespace MultiSearch.API.Infrastructure
{
    public class MultiSearchContext : DbContext, IUnitOfWork
    {
        public DbSet<SearchRequest> Request { get; set; }
        public DbSet<SearchResult> Results { get; set; }

        public MultiSearchContext()
            : base("MultiSearchContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchRequest>()
                .Property(c => c.Date)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            base.OnModelCreating(modelBuilder);
        }
    }
}
