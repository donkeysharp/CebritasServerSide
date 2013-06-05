using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using Cebritas.BusinessLogic.Entities;
using Cebritas.BusinessModel;

namespace Cebritas.DataAccess {
    public class CebraContext : DbContext, IDisposable {
        private static CebraContext context = new CebraContext();

        public DbSet<Usuario> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Report> Reports { get; set; }

        // Places Module
        public DbSet<Category> Categories { get; set; }
        public DbSet<Place> Places { get; set; }

        private CebraContext() {
        }

        static CebraContext() {
            context.Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static CebraContext GetInstance() {
            return context;
        }
    }
}