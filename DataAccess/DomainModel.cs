using Domain.Interfaces;

namespace DataAccess
{
    using System.Data.Entity;
    using DomainEntities;

    public class DomainModel : DbContext, IUnitOfWork
    {
        // Your context has been configured to use a 'DomainModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccess.DomainModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DomainModel' 
        // connection string in the application configuration file.
        public DomainModel()
            : base("name=DomainModel")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DomainModel>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
    }
}