namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public static class ModelBuilderExtesions
{
    public static void CreateUserModel(this ModelBuilder model)
    {
        model.Entity<User>().HasKey(user => user.Id);
        model.Entity<User>().Property(user => user.Login).IsRequired();
        model.Entity<User>().Property(user => user.Name).IsRequired();
        model.Entity<User>().Property(user => user.Phone).IsRequired();
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
        modelBuilder.Entity<Culture>().Property(culture => culture.Name).IsRequired();
        modelBuilder.Entity<Culture>().Property(culture => culture.DisplayedName).IsRequired();

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.ArticlesTranslates)
            .WithOne(a => a.Culture)
            .HasForeignKey(cul => cul.CultureId);

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.Categories)
            .WithOne(a => a.Culture)
            .HasForeignKey(cul => cul.CultureId);
    } 
    public static void CreateCategoriesLocaleModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryLocale>().HasKey(ct => new { ct.CategoryId, ct.CultureId });
        modelBuilder.Entity<CategoryLocale>().HasKey(culture => culture.CategoryId);
        modelBuilder.Entity<CategoryLocale>().Property(culture => culture.Name);
        modelBuilder.Entity<CategoryLocale>()
            .HasOne(culture => culture.Culture)
            .WithMany(cul => cul.Categories)
            .HasForeignKey(cul => cul.CultureId);
    }
    public static void CreateArtilesModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasKey(article => article.Id);

        modelBuilder.Entity<Article>()
            .HasOne(article => article.Category)
            .WithMany(cat => cat.Articles);

        modelBuilder.Entity<Article>().Property(article => article.Date).IsRequired();
        modelBuilder.Entity<Article>().Property(article => article.Region).IsRequired();
        modelBuilder.Entity<Article>().Property(article => article.Type).IsRequired();
        modelBuilder.Entity<Article>().Property(article => article.CategoryId).IsRequired();
    }
    public static void CreateCategoryModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(category => category.Id);
        modelBuilder.Entity<Category>()
            .HasMany(cat => cat.Articles)
            .WithOne(art => art.Category);
    }
}