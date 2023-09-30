using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
        
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Seed data for Difficulties
        var difficulties = new List<Difficulty>
        {
            new Difficulty()
            {
                Id = Guid.Parse("c08f7113-d920-4e59-b9ad-eb961c6d8fad"),
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.Parse("af5f372b-bad1-4b04-a30d-81f1e6952551"),
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = Guid.Parse("d86c36e2-bd3e-46b5-afba-f2bd295f61e9"),
                Name = "Hard"
            },
        };

        modelBuilder.Entity<Difficulty>().HasData(difficulties);
    }
}