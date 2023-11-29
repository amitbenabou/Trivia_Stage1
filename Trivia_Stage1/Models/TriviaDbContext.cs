using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaDbContext : DbContext
{
    public TriviaDbContext()
    {
    }

    public TriviaDbContext(DbContextOptions<TriviaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerType> PlayerTypes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionStatus> QuestionStatuses { get; set; }

    public virtual DbSet<QuestionSubject> QuestionSubjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = LAB2-3\\SQLEXPRESS; Database=TriviaDB;Trusted_Connection = True; TrustServerCertificate = True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74C836BA9731");

            entity.HasOne(d => d.Type).WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Player__typeID__2B3F6F97");
        });

        modelBuilder.Entity<PlayerType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__PlayerTy__516F03B56403025F");

            entity.Property(e => e.TypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FACE5F1B5AA");

            entity.HasOne(d => d.Player).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__Playe__2F10007B");

            entity.HasOne(d => d.Status).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__Statu__2E1BDC42");

            entity.HasOne(d => d.Subject).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__Subje__300424B4");
        });

        modelBuilder.Entity<QuestionStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Question__C8EE2063CB2D9C0D");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<QuestionSubject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Question__AC1BA3A8D5381C28");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
