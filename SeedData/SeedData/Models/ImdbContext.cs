using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace SeedData.Models;

public partial class ImdbContext : DbContext
{
    public ImdbContext()
    {
    }

    public ImdbContext(DbContextOptions<ImdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NameBasic> NameBasics { get; set; }

    public virtual DbSet<TitleAkas> TitleAkas { get; set; }

    public virtual DbSet<TitleAttribute> TitleAttributes { get; set; }

    public virtual DbSet<TitleBasic> TitleBasics { get; set; }

    public virtual DbSet<TitleComment> TitleComments { get; set; }

    public virtual DbSet<TitleEpisode> TitleEpisodes { get; set; }

    public virtual DbSet<TitleGenre> TitleGenres { get; set; }

    public virtual DbSet<TitleRating> TitleRatings { get; set; }

    public virtual DbSet<TitleType> TitleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(Env.GetString("ConnectionString"), ServerVersion.AutoDetect(Env.GetString("ConnectionString")));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<NameBasic>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("name_basics");

            entity.Property(e => e.BirthYear).HasColumnName("birthYear");
            entity.Property(e => e.DeathYear).HasColumnName("deathYear");
            entity.Property(e => e.Nconst)
                .HasColumnType("text")
                .HasColumnName("nconst");
            entity.Property(e => e.PrimaryName)
                .HasColumnType("text")
                .HasColumnName("primaryName");
        });

        modelBuilder.Entity<TitleAkas>(entity =>
        {
            entity.HasKey(e => e.IdAkas).HasName("PRIMARY");

            entity.ToTable("title_akas");

            entity.HasIndex(e => e.Tconst, "fk_title_akas_title_basics_idx");

            entity.Property(e => e.IdAkas).HasColumnName("id_akas");
            entity.Property(e => e.IsOriginalTitle).HasColumnName("is_original_title");
            entity.Property(e => e.Language)
                .HasMaxLength(100)
                .HasColumnName("language");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Tconst)
                .HasMaxLength(100)
                .HasColumnName("tconst");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.TitleAkas)
                .HasForeignKey(d => d.Tconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_title_akas_title_basics");
        });

        modelBuilder.Entity<TitleAttribute>(entity =>
        {
            entity.HasKey(e => e.IdAttribute).HasName("PRIMARY");

            entity.ToTable("title_attributes");

            entity.Property(e => e.IdAttribute).HasColumnName("id_attribute");
            entity.Property(e => e.Attribute)
                .HasMaxLength(100)
                .HasColumnName("attribute");

            entity.HasMany(d => d.IdAkas).WithMany(p => p.IdAttributes)
                .UsingEntity<Dictionary<string, object>>(
                    "TitleAkasAttribute",
                    r => r.HasOne<TitleAkas>().WithMany()
                        .HasForeignKey("IdAkas")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_title_attributes_has_title_akas_title_akas1"),
                    l => l.HasOne<TitleAttribute>().WithMany()
                        .HasForeignKey("IdAttribute")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_title_attributes_has_title_akas_title_attributes1"),
                    j =>
                    {
                        j.HasKey("IdAttribute", "IdAkas")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("title_akas_attributes");
                        j.HasIndex(new[] { "IdAkas" }, "fk_title_attributes_has_title_akas_title_akas1_idx");
                        j.HasIndex(new[] { "IdAttribute" }, "fk_title_attributes_has_title_akas_title_attributes1_idx");
                        j.IndexerProperty<int>("IdAttribute").HasColumnName("id_attribute");
                        j.IndexerProperty<int>("IdAkas").HasColumnName("id_akas");
                    });
        });

        modelBuilder.Entity<TitleBasic>(entity =>
        {
            entity.HasKey(e => e.Tconst).HasName("PRIMARY");

            entity.ToTable("title_basics");

            entity.Property(e => e.Tconst)
                .HasMaxLength(100)
                .HasColumnName("tconst");
            entity.Property(e => e.EndYear).HasColumnName("end_year");
            entity.Property(e => e.IsAdult).HasColumnName("is_adult");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(255)
                .HasColumnName("original_title");
            entity.Property(e => e.PrimaryTitle)
                .HasMaxLength(255)
                .HasColumnName("primary_title");
            entity.Property(e => e.RuntimeMinutes).HasColumnName("runtime_minutes");
            entity.Property(e => e.StartYear).HasColumnName("start_year");
            entity.Property(e => e.TitleType)
                .HasMaxLength(100)
                .HasColumnName("title_type");
        });

        modelBuilder.Entity<TitleComment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PRIMARY");

            entity.ToTable("title_comments");

            entity.HasIndex(e => e.Tconst, "fk_title_comments_title_basics1_idx");

            entity.Property(e => e.IdComment).HasColumnName("id_comment");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .HasColumnName("comment");
            entity.Property(e => e.Tconst)
                .HasMaxLength(100)
                .HasColumnName("tconst");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.TitleComments)
                .HasForeignKey(d => d.Tconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_title_comments_title_basics1");
        });

        modelBuilder.Entity<TitleEpisode>(entity =>
        {
            entity.HasKey(e => new { e.ParentTconst, e.Tconst })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("title_episodes");

            entity.HasIndex(e => e.Tconst, "fk_title_episodes_title_basics1");

            entity.HasIndex(e => e.ParentTconst, "fk_title_episodes_title_basics2_idx");

            entity.Property(e => e.ParentTconst)
                .HasMaxLength(100)
                .HasColumnName("parent_tconst");
            entity.Property(e => e.Tconst)
                .HasMaxLength(100)
                .HasColumnName("tconst");
            entity.Property(e => e.EpisodeNumber).HasColumnName("episode_number");
            entity.Property(e => e.SeasonNumber).HasColumnName("season_number");

            entity.HasOne(d => d.ParentTconstNavigation).WithMany(p => p.TitleEpisodeParentTconstNavigations)
                .HasForeignKey(d => d.ParentTconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_title_episodes_title_basics2");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.TitleEpisodeTconstNavigations)
                .HasForeignKey(d => d.Tconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_title_episodes_title_basics1");
        });

        modelBuilder.Entity<TitleGenre>(entity =>
        {
            entity.HasKey(e => e.IdGenre).HasName("PRIMARY");

            entity.ToTable("title_genres");

            entity.Property(e => e.IdGenre).HasColumnName("id_genre");
            entity.Property(e => e.Genre)
                .HasMaxLength(100)
                .HasColumnName("genre");

            entity.HasMany(d => d.Tconsts).WithMany(p => p.IdGenres)
                .UsingEntity<Dictionary<string, object>>(
                    "TitleBasicsGenre",
                    r => r.HasOne<TitleBasic>().WithMany()
                        .HasForeignKey("Tconst")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_title_genres_has_title_basics_title_basics1"),
                    l => l.HasOne<TitleGenre>().WithMany()
                        .HasForeignKey("IdGenre")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_title_genres_has_title_basics_title_genres1"),
                    j =>
                    {
                        j.HasKey("IdGenre", "Tconst")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("title_basics_genres");
                        j.HasIndex(new[] { "Tconst" }, "fk_title_genres_has_title_basics_title_basics1_idx");
                        j.HasIndex(new[] { "IdGenre" }, "fk_title_genres_has_title_basics_title_genres1_idx");
                        j.IndexerProperty<int>("IdGenre").HasColumnName("id_genre");
                        j.IndexerProperty<string>("Tconst")
                            .HasMaxLength(100)
                            .HasColumnName("tconst");
                    });
        });

        modelBuilder.Entity<TitleRating>(entity =>
        {
            entity.HasKey(e => e.IdRating).HasName("PRIMARY");

            entity.ToTable("title_ratings");

            entity.HasIndex(e => e.Tconst, "fk_title_ratings_title_basics1");

            entity.Property(e => e.IdRating).HasColumnName("id_rating");
            entity.Property(e => e.AverageRating).HasColumnName("average_rating");
            entity.Property(e => e.NumVotes).HasColumnName("num_votes");
            entity.Property(e => e.Tconst)
                .HasMaxLength(100)
                .HasColumnName("tconst");

            entity.HasOne(d => d.TconstNavigation).WithMany(p => p.TitleRatings)
                .HasForeignKey(d => d.Tconst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_title_ratings_title_basics1");
        });

        modelBuilder.Entity<TitleType>(entity =>
        {
            entity.HasKey(e => e.IdTypes).HasName("PRIMARY");

            entity.ToTable("title_types");

            entity.Property(e => e.IdTypes).HasColumnName("id_types");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");

            entity.HasMany(d => d.IdAkas).WithMany(p => p.IdTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "TitleAkasType",
                    r => r.HasOne<TitleAkas>().WithMany()
                        .HasForeignKey("IdAkas")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_title_types_has_title_akas_title_akas1"),
                    l => l.HasOne<TitleType>().WithMany()
                        .HasForeignKey("IdTypes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_title_types_has_title_akas_title_types1"),
                    j =>
                    {
                        j.HasKey("IdTypes", "IdAkas")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("title_akas_types");
                        j.HasIndex(new[] { "IdAkas" }, "fk_title_types_has_title_akas_title_akas1_idx");
                        j.HasIndex(new[] { "IdTypes" }, "fk_title_types_has_title_akas_title_types1_idx");
                        j.IndexerProperty<int>("IdTypes").HasColumnName("id_types");
                        j.IndexerProperty<int>("IdAkas").HasColumnName("id_akas");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
