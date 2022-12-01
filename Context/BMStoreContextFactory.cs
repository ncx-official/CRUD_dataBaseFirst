using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataBaseManagerApplication.Context;

public class BMStoreContextFactory : IDesignTimeDbContextFactory<BMStoreContext>
{
    public BMStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BMStoreContext>();

        // getting config from appsettings.json
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile($"appsettings.json");
        IConfigurationRoot config = builder.Build();

        // getting connectionString from appsettings.json
        string connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(connectionString: config.GetConnectionString("DefaultConnection"),
            serverVersion: ServerVersion.Parse(config["ServerVersion"]),
            opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

        return new BMStoreContext(optionsBuilder.Options);
    }
}