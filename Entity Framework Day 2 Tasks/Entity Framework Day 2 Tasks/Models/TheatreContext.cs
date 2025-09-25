using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Day_2_Tasks.Models;

public partial class TheatreContext : DbContext
{
    public TheatreContext()
    {
    }

    public TheatreContext(DbContextOptions<TheatreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Showtime> Showtimes { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3MRH4AD;Database=theatre;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__CUSTOMER__1CE12D379CD7901D");

            entity.ToTable("CUSTOMER");

            entity.HasIndex(e => e.Email, "UQ__CUSTOMER__161CF724E43D9EA9").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FULL_NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasKey(e => e.HallId).HasName("PK__HALL__BF1D6F6E4AF67E2E");

            entity.ToTable("HALL");

            entity.HasIndex(e => e.Name, "UQ__HALL__D9C1FA0023CBA4F3").IsUnique();

            entity.Property(e => e.HallId).HasColumnName("HALL_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__MOVIE__C6C7FEFC8AAB9EAF");

            entity.ToTable("MOVIE");

            entity.Property(e => e.MovieId).HasColumnName("MOVIE_ID");
            entity.Property(e => e.DurationMin).HasColumnName("DURATION_MIN");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("Y")
                .IsFixedLength()
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Showtime>(entity =>
        {
            entity.HasKey(e => e.ShowtimeId).HasName("PK__SHOWTIME__C3BD256ECFD0DE4F");

            entity.ToTable("SHOWTIME");

            entity.Property(e => e.ShowtimeId).HasColumnName("SHOWTIME_ID");
            entity.Property(e => e.BasePrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("BASE_PRICE");
            entity.Property(e => e.HallId).HasColumnName("HALL_ID");
            entity.Property(e => e.MovieId).HasColumnName("MOVIE_ID");
            entity.Property(e => e.StartAt)
                .HasColumnType("datetime")
                .HasColumnName("START_AT");

            entity.HasOne(d => d.Hall).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ST_HALL");

            entity.HasOne(d => d.Movie).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ST_MOVIE");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__TICKET__7F5E71D62FD015A5");

            entity.ToTable("TICKET");

            entity.HasIndex(e => new { e.ShowtimeId, e.SeatLabel }, "UQ_TICKET_SEAT").IsUnique();

            entity.Property(e => e.TicketId).HasColumnName("TICKET_ID");
            entity.Property(e => e.BookedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("BOOKED_AT");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.FinalPrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("FINAL_PRICE");
            entity.Property(e => e.PaidAt)
                .HasColumnType("datetime")
                .HasColumnName("PAID_AT");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_METHOD");
            entity.Property(e => e.SeatLabel)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SEAT_LABEL");
            entity.Property(e => e.ShowtimeId).HasColumnName("SHOWTIME_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValue("RESERVED")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TK_CUSTOMER");

            entity.HasOne(d => d.Showtime).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ShowtimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TK_SHOWTIME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
