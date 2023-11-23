using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChatLibrary.Models;

public partial class ChatApidbContext : DbContext
{
    public ChatApidbContext()
    {
    }

    public ChatApidbContext(DbContextOptions<ChatApidbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Chatroom> Chatrooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-FC8EBGB;Initial Catalog=ChatAPIDB;User ID=shield;Password=08101984;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chats__A9FBE7C6E069CD3D");

            entity.Property(e => e.ChatId).ValueGeneratedNever();
            entity.Property(e => e.ChatContent).HasColumnType("text");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Path).HasColumnType("text");

            entity.HasOne(d => d.Chatroom).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ChatroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chats__ChatroomI__44FF419A");

            entity.HasOne(d => d.ChattedUser).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ChattedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chats__ChattedUs__440B1D61");
        });

        modelBuilder.Entity<Chatroom>(entity =>
        {
            entity.HasKey(e => e.ChatroomId).HasName("PK__Chatroom__B83BDFC8E2ABE90B");

            entity.Property(e => e.ChatroomId).ValueGeneratedNever();

            entity.HasOne(d => d.AccessibleUser).WithMany(p => p.Chatrooms)
                .HasForeignKey(d => d.AccessibleUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chatrooms__Acces__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CECE20DEC");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
