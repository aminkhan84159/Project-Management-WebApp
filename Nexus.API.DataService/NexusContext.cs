using Microsoft.EntityFrameworkCore;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService;

public partial class NexusContext : DbContext
{
    public NexusContext()
    {
    }

    public NexusContext(DbContextOptions<NexusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Relation> Relations { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Nexus.API.DataService.Models.Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DT002\\SQLEXPRESS;Database=Nexus;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK_Issue_IssueId");

            entity.ToTable("Issue");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Project).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Issue_ProjectId__Project_ProjectId");

            entity.HasOne(d => d.Task).WithMany(p => p.Issues)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issue_TaskId__Task_TaskId");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK_Project_ProjectId");

            entity.ToTable("Project");

            entity.HasIndex(e => e.ProjectName, "UQ_Project_ProjectName").IsUnique();

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProjectName).HasMaxLength(80);
            entity.Property(e => e.StatusId).HasDefaultValue(1);

            entity.HasOne(d => d.Status).WithMany(p => p.Projects)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Project_StatusId__Status_StatusId");
        });

        modelBuilder.Entity<Relation>(entity =>
        {
            entity.HasKey(e => e.RelationId).HasName("PK_Relation_RelationId");

            entity.ToTable("Relation");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Role).HasMaxLength(30);

            entity.HasOne(d => d.Project).WithMany(p => p.Relations)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Relation_ProjectId__Project_ProjectId");

            entity.HasOne(d => d.User).WithMany(p => p.Relations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_UserId");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK_Status_StatusId");

            entity.ToTable("Status");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Type).HasMaxLength(30);
        });

        modelBuilder.Entity<Nexus.API.DataService.Models.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK_Task_TaskId");

            entity.ToTable("Task");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Priority)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StatusId).HasDefaultValue(1);
            entity.Property(e => e.TaskName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).HasDefaultValue(101);

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Task_ProjectId__Project_ProjectId");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_StatusId__Status_StatusId");

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Task_UserId__User_UserId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User_UserId");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ_User_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_User_Username").IsUnique();

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(80);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JoinedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailId).HasName("PK_UserDetails_UserDetail_Id");

            entity.HasIndex(e => e.PhoneNo, "UQ_UserDetails_PhoneNo").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ_UserDetails_UserId").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.Gender).HasMaxLength(6);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.PhoneNo).HasMaxLength(13);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.UserId).HasDefaultValue(101);

            entity.HasOne(d => d.User).WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserDetails_UserId__Users_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
