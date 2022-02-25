using Microsoft.EntityFrameworkCore;
using MoneerProject.Models.Entity;
using MoneerProject.Models.EntityMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneerProject
{
    public class PblogsContext : DbContext
    {
        public PblogsContext(DbContextOptions<PblogsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap()); 
            modelBuilder.ApplyConfiguration(new TabelSection2Map()); 
            modelBuilder.ApplyConfiguration(new TableSection3Map()); 
           
        }
        public DbSet<Users> Users { get; set; } 
        public DbSet<TabelSection2> TabelSection2 { get; set; } 
        public DbSet<TableSection3> TableSection3 { get; set; } 
        

    }
}
