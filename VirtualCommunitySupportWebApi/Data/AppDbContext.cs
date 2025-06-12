using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VirtualCommunitySupportWebApi.Models;

namespace VirtualCommunitySupportWebApi.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<Missionapplication> Missionapplications { get; set; }

    public virtual DbSet<Missionskill> Missionskills { get; set; }

    public virtual DbSet<Missiontheme> Missionthemes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userdetail> Userdetails { get; set; }

    public virtual DbSet<Userskill> Userskills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=virtual_community_support;Username=postgres;Password=Password@1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cityname)
                .HasMaxLength(100)
                .HasColumnName("cityname");
            entity.Property(e => e.Countryid).HasColumnName("countryid");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_countryid_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Countryname)
                .HasMaxLength(100)
                .HasColumnName("countryname");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("missions_pkey");

            entity.ToTable("missions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Missionavilability)
                .HasMaxLength(100)
                .HasColumnName("missionavilability");
            entity.Property(e => e.Missiondescription).HasColumnName("missiondescription");
            entity.Property(e => e.Missiondocuments).HasColumnName("missiondocuments");
            entity.Property(e => e.Missionimages).HasColumnName("missionimages");
            entity.Property(e => e.Missionorganisationdetail).HasColumnName("missionorganisationdetail");
            entity.Property(e => e.Missionorganisationname)
                .HasMaxLength(100)
                .HasColumnName("missionorganisationname");
            entity.Property(e => e.Missionskillid).HasColumnName("missionskillid");
            entity.Property(e => e.Missionthemeid).HasColumnName("missionthemeid");
            entity.Property(e => e.Missiontitle)
                .HasMaxLength(100)
                .HasColumnName("missiontitle");
            entity.Property(e => e.Missiontype)
                .HasMaxLength(50)
                .HasColumnName("missiontype");
            entity.Property(e => e.Missionvideourl)
                .HasMaxLength(255)
                .HasColumnName("missionvideourl");
            entity.Property(e => e.Registrationdeadline).HasColumnName("registrationdeadline");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Totalsheets).HasColumnName("totalsheets");

            entity.HasOne(d => d.City).WithMany(p => p.Missions)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("missions_cityid_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Missions)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("missions_countryid_fkey");

            entity.HasOne(d => d.Missionskill).WithMany(p => p.Missions)
                .HasForeignKey(d => d.Missionskillid)
                .HasConstraintName("missions_missionskillid_fkey");

            entity.HasOne(d => d.Missiontheme).WithMany(p => p.Missions)
                .HasForeignKey(d => d.Missionthemeid)
                .HasConstraintName("missions_missionthemeid_fkey");
        });

        modelBuilder.Entity<Missionapplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("missionapplication_pkey");

            entity.ToTable("missionapplication");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Applieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("applieddate");
            entity.Property(e => e.Missionid).HasColumnName("missionid");
            entity.Property(e => e.Sheet).HasColumnName("sheet");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Mission).WithMany(p => p.Missionapplications)
                .HasForeignKey(d => d.Missionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("missionapplication_missionid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Missionapplications)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("missionapplication_userid_fkey");
        });

        modelBuilder.Entity<Missionskill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("missionskill_pkey");

            entity.ToTable("missionskill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Skillname)
                .HasMaxLength(100)
                .HasColumnName("skillname");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Missiontheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("missiontheme_pkey");

            entity.ToTable("missiontheme");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Themename)
                .HasMaxLength(100)
                .HasColumnName("themename");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Emailaddress, "User_emailaddress_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"User_id_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Emailaddress)
                .HasMaxLength(255)
                .HasColumnName("emailaddress");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(50)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Usertype)
                .HasMaxLength(50)
                .HasColumnName("usertype");
        });

        modelBuilder.Entity<Userdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userdetail_pkey");

            entity.ToTable("userdetail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avilability)
                .HasMaxLength(100)
                .HasColumnName("avilability");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.Employeeid)
                .HasMaxLength(50)
                .HasColumnName("employeeid");
            entity.Property(e => e.Linkdinurl)
                .HasMaxLength(255)
                .HasColumnName("linkdinurl");
            entity.Property(e => e.Manager)
                .HasMaxLength(100)
                .HasColumnName("manager");
            entity.Property(e => e.Myprofile).HasColumnName("myprofile");
            entity.Property(e => e.Myskills).HasColumnName("myskills");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Userimage)
                .HasMaxLength(255)
                .HasColumnName("userimage");
            entity.Property(e => e.Whyivolunteer).HasColumnName("whyivolunteer");

            entity.HasOne(d => d.City).WithMany(p => p.Userdetails)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("userdetail_cityid_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Userdetails)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("userdetail_countryid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Userdetails)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userdetail_userid_fkey");
        });

        modelBuilder.Entity<Userskill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userskills_pkey");

            entity.ToTable("userskills");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Skill)
                .HasMaxLength(100)
                .HasColumnName("skill");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Userskills)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userskills_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
