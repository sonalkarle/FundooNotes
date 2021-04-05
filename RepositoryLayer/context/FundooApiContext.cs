using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RepositoryLayer
{
    public partial class FundooApiContext : DbContext
    {
        public FundooApiContext()
        {
        }

        public FundooApiContext(DbContextOptions<FundooApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserAccount> Accounts { get; set; }
        public virtual DbSet<Collaborator> Collaborators { get; set; }
        public virtual DbSet<Lable> Lables { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<NoteLable> NoteLables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-OKP25QH;Database=FundooApi;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Account__1788CC4C48307FF7");

                entity.ToTable("Account");

                entity.Property(e => e.Creationtime).HasColumnType("date");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modificationtime).HasColumnType("date");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Collaborator>(entity =>
            {
                entity.ToTable("Collaborator");

                entity.HasIndex(e => e.CollaboratorEmail, "UQ__Collabor__6D342036ADD67442")
                    .IsUnique();

                entity.Property(e => e.CollaboratorEmail)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("FKofNotetocoll");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FKofAccounttoColl");
            });

            modelBuilder.Entity<Lable>(entity =>
            {
                entity.HasKey(e => e.LableName)
                    .HasName("PK__Lable__2F4C7B1D74F533B4");

                entity.ToTable("Lable");

                entity.Property(e => e.LableName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.LableNavigation)
                    .WithMany(p => p.Lables)
                    .HasForeignKey(d => d.LableId)
                    .HasConstraintName("Forign_key");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Lables)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FKforlable");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                entity.Property(e => e.Body)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsArchieve).HasColumnName("isArchieve");

                entity.Property(e => e.IsPin).HasColumnName("isPin");

                entity.Property(e => e.IsTrash).HasColumnName("isTrash");

                entity.Property(e => e.ModificaionTime).HasColumnType("date");

                entity.Property(e => e.Reminder).HasColumnType("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK");
            });

            modelBuilder.Entity<NoteLable>(entity =>
            {
                entity.HasKey(e => e.LableId)
                    .HasName("PK__NoteLabl__2063BAA41B68D641");

                entity.ToTable("NoteLable");

                entity.Property(e => e.LableId).ValueGeneratedNever();

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.NoteLables)
                    .HasForeignKey(d => d.NoteId)
                    .HasConstraintName("Key_foreignforNotelable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
