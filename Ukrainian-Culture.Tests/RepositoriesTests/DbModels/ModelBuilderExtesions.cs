namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public static class ModelBuilderExtesions
{
    public static void CreateUserModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<User>().Ignore(p => p.AccessFailedCount);
        modelBuilder.Entity<User>().Ignore(p => p.ConcurrencyStamp);
        modelBuilder.Entity<User>().Ignore(p => p.Email);
        modelBuilder.Entity<User>().Ignore(p => p.EmailConfirmed);
        modelBuilder.Entity<User>().Ignore(p => p.LockoutEnabled);
        modelBuilder.Entity<User>().Ignore(p => p.LockoutEnd);
        modelBuilder.Entity<User>().Ignore(p => p.SecurityStamp);
        modelBuilder.Entity<User>().Ignore(p => p.PhoneNumberConfirmed);
        modelBuilder.Entity<User>().Ignore(p => p.PhoneNumber);
        modelBuilder.Entity<User>().Ignore(p => p.PasswordHash);
        modelBuilder.Entity<User>().Ignore(p => p.NormalizedUserName);
        modelBuilder.Entity<User>().Ignore(p => p.NormalizedEmail);
        modelBuilder.Entity<User>().Ignore(p => p.UserName);
        modelBuilder.Entity<User>().Ignore(p => p.TwoFactorEnabled);
        modelBuilder.Entity<User>().Ignore(p => p.RefreshToken);
        modelBuilder.Entity<User>().Ignore(p => p.RefreshTokenExpiryTime);
        modelBuilder.Entity<User>()
            .HasMany(a => a.History)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
    }

    public static void CreateUserHistoryModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserHistory>().HasKey(uh => uh.Id);
        modelBuilder.Entity<UserHistory>().Property(uh => uh.DateOfWatch);
        modelBuilder.Entity<UserHistory>().Property(uh => uh.Title);
        modelBuilder.Entity<UserHistory>()
            .HasOne(uh => uh.User)
            .WithMany(user => user.History)
            .HasForeignKey(uh => uh.UserId);
    }
    public static void CreateArticlesLocaleModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticlesLocale>().HasKey(article => new { article.Id, article.CultureId });
        modelBuilder.Entity<ArticlesLocale>()
            .HasOne(culture => culture.Culture)
            .WithMany(cul => cul.ArticlesTranslates)
            .HasForeignKey(art => art.CultureId);

        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.ShortDescription);
        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.SubText);
        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.Title);
        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.Content);
    }
    public static void CreateCultureModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Culture>().HasKey(culture => culture.Id);
        modelBuilder.Entity<Culture>().Property(culture => culture.Name);
        modelBuilder.Entity<Culture>().Property(culture => culture.DisplayedName);

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.ArticlesTranslates)
            .WithOne(a => a.Culture)
            .HasForeignKey(cul => cul.CultureId);
    }
    public static void CreateArtilesModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasKey(article => article.Id);

        modelBuilder.Entity<Article>()
            .HasOne(article => article.Category)
            .WithMany(cat => cat.Articles);

        modelBuilder.Entity<Article>().Property(article => article.Date);
        modelBuilder.Entity<Article>().Property(article => article.Region);
        modelBuilder.Entity<Article>().Property(article => article.CategoryId);
    }
    public static void CreateCategoryModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(category => category.Id);
        modelBuilder.Entity<Category>().Property(category => category.Name);
        modelBuilder.Entity<Category>()
            .HasMany(cat => cat.Articles)
            .WithOne(art => art.Category);
    }
}