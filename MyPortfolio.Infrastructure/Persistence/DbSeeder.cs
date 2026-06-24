using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Domain.Entities;
using MyPortfolio.Domain.Enums;

namespace MyPortfolio.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        await SeedUserAsync(context);
        await SeedAboutProfileAsync(context);
        await SeedSkillsAsync(context);
        await SeedTechnologiesAsync(context);
        await SeedTagsAsync(context);

        await context.SaveChangesAsync();
    }

    // ── 1. User ────────────────────────────────────────────────────────────
    private static async Task SeedUserAsync(ApplicationDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var hash = BCrypt.Net.BCrypt.HashPassword("Admin@123", workFactor: 12);
        var admin = User.Create("admin", "pmpmanhphong26@gmail.com", hash);
        await context.Users.AddAsync(admin);
        await context.SaveChangesAsync();

        Console.WriteLine("✓ Seeded: User");
    }

    // ── 2. About Profile ───────────────────────────────────────────────────
    private static async Task SeedAboutProfileAsync(ApplicationDbContext context)
    {
        if (await context.AboutProfiles.AnyAsync()) return;

        var profile = AboutProfile.Create("Phùng Mạnh Phong");
        profile.Update(
            fullName: "Phùng Mạnh Phong",
            tagline: "Full-stack Developer | .NET & React",
            bio: "Tôi là một Full-Stack Developer vừa tốt nghiệp, yêu thích xây dựng các ứng dụng web bằng ASP.NET Core và React." +
                 " Tôi luôn cố gắng học hỏi, cải thiện kỹ năng và mong muốn được tham gia vào các dự án thực tế để phát triển bản thân.",
            location: "Hà Nội, Việt Nam",
            contactEmail: "pmpmanhphong26@gmail.com"
        );

        await context.AboutProfiles.AddAsync(profile);
        await context.SaveChangesAsync();

        Console.WriteLine("✓ Seeded: AboutProfile");
    }

    // ── 3. Skills ──────────────────────────────────────────────────────────
    private static async Task SeedSkillsAsync(ApplicationDbContext context)
    {
        if (await context.Skills.AnyAsync()) return;

        var skills = new List<Skill>
        {
            // Frontend
            Skill.Create("React",       SkillCategory.Frontend, 2, null),
            Skill.Create("HTML/CSS",    SkillCategory.Frontend, 2, null),
            Skill.Create("Next.js",     SkillCategory.Frontend, 2, null),

            // Backend
            Skill.Create("ASP.NET Core", SkillCategory.Backend, 3, null),
            Skill.Create("C#",           SkillCategory.Backend, 3, null),
            Skill.Create("Node.js",      SkillCategory.Backend, 1, null),
            Skill.Create("REST API",     SkillCategory.Backend, 3, null),
            Skill.Create("Clean Architecture",     SkillCategory.Backend, 3, null),

            // Database
            Skill.Create("PostgreSQL", SkillCategory.Database, 3, null),
            Skill.Create("SQL Server", SkillCategory.Database, 3, null),
            Skill.Create("Redis",      SkillCategory.Database, 2, null),

            // Soft Skills
            Skill.Create("Team Work",      SkillCategory.Other, 4 , null),
            Skill.Create("Problem Solving", SkillCategory.Other, 4 , null),
            Skill.Create("Communication",  SkillCategory.Other, 4 , null),
        };

        // Gán display order
        for (int i = 0; i < skills.Count; i++)
            skills[i].SetOrder(i);

        await context.Skills.AddRangeAsync(skills);
        await context.SaveChangesAsync();

        Console.WriteLine("✓ Seeded: Skills");
    }

    // ── 6. Technologies ────────────────────────────────────────────────────
    private static async Task SeedTechnologiesAsync(ApplicationDbContext context)
    {
        if (await context.Technologies.AnyAsync()) return;

        var technologies = new List<Technology>
        {
           Technology.Create("C#"),
           Technology.Create("TypeScript"),
           Technology.Create("JavaScript"),
           Technology.Create("HTML/CSS"),
        
        // Frontend
           Technology.Create("React"),
           Technology.Create("Next.js"),
           Technology.Create("TailwindCSS"),
        
        // Backend
           Technology.Create("ASP.NET Core"),
           Technology.Create(".NET 8"),
           Technology.Create("Entity Framework Core"),
           Technology.Create("MediatR"),
           Technology.Create("REST API"),
           Technology.Create("SignalR"),
           Technology.Create("JWT"),
        
        // Database & Caching
           Technology.Create("PostgreSQL"),
           Technology.Create("SQL Server"),
           Technology.Create("Redis"),
        
        // DevOps & Cloud
           Technology.Create("Docker"),
           Technology.Create("GitHub Actions"),
        
        // Tools & Testing
           Technology.Create("Git"),
           Technology.Create("GitHub"),
           Technology.Create("xUnit"),
           Technology.Create("Postman")
        };

        await context.Technologies.AddRangeAsync(technologies);
        await context.SaveChangesAsync();

        Console.WriteLine("✓ Seeded: Technologies");
    }

    // ── 7. Tags ────────────────────────────────────────────────────────────
    private static async Task SeedTagsAsync(ApplicationDbContext context)
    {
        if (await context.Tags.AnyAsync()) return;

        var tags = new List<Tag>
        {
          // Topics
           Tag.Create("Web Development"),
           Tag.Create("Backend"),
           Tag.Create("Frontend"),
           Tag.Create("DevOps"),
           Tag.Create("System Design"),
           Tag.Create("Architecture"),
           Tag.Create("Performance"),
           Tag.Create("Database"),
         // Content Types
           Tag.Create("Tutorial"),
           Tag.Create("Tips & Tricks"),
           Tag.Create("Career"),
           Tag.Create("Personal"),
           Tag.Create("Open Source"),
        // Specific Tech Highlights
           Tag.Create(".NET"),
           Tag.Create("React"),
           Tag.Create("Clean Architecture")
        };

        await context.Tags.AddRangeAsync(tags);
        await context.SaveChangesAsync();

        Console.WriteLine("✓ Seeded: Tags");
    }
    
} 