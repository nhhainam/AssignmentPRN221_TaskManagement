using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssignmentPRN221_TaskManagement.DataAccess;

public partial class GroupManagementContext : DbContext
{
    public GroupManagementContext()
    {
    }

    public GroupManagementContext(DbContextOptions<GroupManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberIssue> MemberIssues { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		if (!optionsBuilder.IsConfigured)
		{
			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
			IConfigurationRoot configuration = builder.Build();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDb"));
		}
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__88C1034D0E578095");

            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.GroupName)
                .HasMaxLength(255)
                .HasColumnName("groupName");
            entity.Property(e => e.Purpose)
                .HasMaxLength(255)
                .HasColumnName("purpose");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GroupId }).HasName("PK__Invitati__03160CCB28D21AE6");

            entity.ToTable("Invitation");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Group).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invitatio__group__1DE57479");

            entity.HasOne(d => d.User).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invitatio__userI__1ED998B2");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__Issues__749E806CDBBD623F");

            entity.Property(e => e.IssueId).HasColumnName("issueId");
            entity.Property(e => e.Assignee).HasColumnName("assignee");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Creator).HasColumnName("creator");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasColumnName("dueDate");
            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.AssigneeNavigation).WithMany(p => p.IssueAssigneeNavigations)
                .HasForeignKey(d => d.Assignee)
                .HasConstraintName("FK__Issues__assignee__1FCDBCEB");

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.IssueCreatorNavigations)
                .HasForeignKey(d => d.Creator)
                .HasConstraintName("FK__Issues__creator__20C1E124");

            entity.HasOne(d => d.Project).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Issues__projectI__21B6055D");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GroupId }).HasName("PK__Members__03160CEBDE7C0B2B");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Group).WithMany(p => p.Members)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Members__groupId__24927208");

            entity.HasOne(d => d.Role).WithMany(p => p.Members)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Members__roleId__25869641");

            entity.HasOne(d => d.User).WithMany(p => p.Members)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Members__userID__267ABA7A");
        });

        modelBuilder.Entity<MemberIssue>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.IssueId }).HasName("PK__MemberIs__1CD3F4F92A1491BC");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.IssueId).HasColumnName("issueId");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Issue).WithMany(p => p.MemberIssues)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberIss__issue__22AA2996");

            entity.HasOne(d => d.User).WithMany(p => p.MemberIssues)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberIss__userI__239E4DCF");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__11F14DA5C073452C");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(255)
                .HasColumnName("projectName");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Group).WithMany(p => p.Projects)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Projects__groupI__276EDEB3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__CD98462A8FEFE210");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .HasColumnName("roleName");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CDF2FF734FC");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
