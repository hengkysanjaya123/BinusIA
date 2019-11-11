using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Model
{
    public partial class BinusIAContext : DbContext
    {
        public BinusIAContext()
        {
        }

        public BinusIAContext(DbContextOptions<BinusIAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommentHeader> CommentHeader { get; set; }
        public virtual DbSet<IaActivityLog> IaActivityLog { get; set; }
        public virtual DbSet<IaHideVoting> IaHideVoting { get; set; }
        public virtual DbSet<IaLog> IaLog { get; set; }
        public virtual DbSet<JuryDetail> JuryDetail { get; set; }
        public virtual DbSet<PeriodManagement> PeriodManagement { get; set; }
        public virtual DbSet<Proposal> Proposal { get; set; }
        public virtual DbSet<ProposalDetail> ProposalDetail { get; set; }
        public virtual DbSet<RubricDescription> RubricDescription { get; set; }
        public virtual DbSet<RubricDetail> RubricDetail { get; set; }
        public virtual DbSet<RubricHeader> RubricHeader { get; set; }
        public virtual DbSet<ScoringDetail> ScoringDetail { get; set; }
        public virtual DbSet<StatusProposal> StatusProposal { get; set; }
        public virtual DbSet<TeamDetail> TeamDetail { get; set; }
        public virtual DbSet<TeamHeader> TeamHeader { get; set; }
        public virtual DbSet<TemplateProposal> TemplateProposal { get; set; }
        public virtual DbSet<TemplateProposalDetail> TemplateProposalDetail { get; set; }
        public virtual DbSet<UserHeader> UserHeader { get; set; }
        public virtual DbSet<Voting> Voting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=Binus_IA;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CommentHeader>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("Comment_Header");

                entity.Property(e => e.CommentId)
                    .HasColumnName("Comment_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.JuryEmail)
                    .IsRequired()
                    .HasColumnName("Jury_Email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProposalId).HasColumnName("Proposal_Id");

                entity.Property(e => e.SectionId).HasColumnName("Section_Id");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.CommentHeader)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Header_Proposal");
            });

            modelBuilder.Entity<IaActivityLog>(entity =>
            {
                entity.ToTable("IA_Activity_Log");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Activity)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IaHideVoting>(entity =>
            {
                entity.ToTable("IA_Hide_Voting");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EditBy)
                    .IsRequired()
                    .HasColumnName("Edit_By")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.EditDate)
                    .HasColumnName("Edit_Date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<IaLog>(entity =>
            {
                entity.ToTable("IA_Log");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EmailLogin)
                    .IsRequired()
                    .HasColumnName("Email_Login")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTry)
                    .IsRequired()
                    .HasColumnName("Email_Try")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProposalId).HasColumnName("Proposal_Id");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.IaLog)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IA_Log_Proposal");
            });

            modelBuilder.Entity<JuryDetail>(entity =>
            {
                entity.ToTable("Jury_Detail");

                entity.Property(e => e.JuryDetailId)
                    .HasColumnName("Jury_Detail_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ApprovalCode)
                    .HasColumnName("Approval_Code")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalDate)
                    .HasColumnName("Approval_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApproveScore).HasColumnName("Approve_Score");

                entity.Property(e => e.AssignDate)
                    .HasColumnName("Assign_Date")
                    .HasColumnType("date");

                entity.Property(e => e.EmailJury)
                    .IsRequired()
                    .HasColumnName("Email_Jury")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProposalId).HasColumnName("Proposal_Id");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.JuryDetail)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Jury_Detail_Proposal");
            });

            modelBuilder.Entity<PeriodManagement>(entity =>
            {
                entity.HasKey(e => e.PeriodId);

                entity.ToTable("Period_Management");

                entity.Property(e => e.PeriodId)
                    .HasColumnName("Period_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnName("End_Date")
                    .HasColumnType("date");

                entity.Property(e => e.StartDate)
                    .HasColumnName("Start_Date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity.Property(e => e.ProposalId)
                    .HasColumnName("Proposal_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ApprovalDate)
                    .HasColumnName("Approval_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("Created_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FinalizedBy)
                    .IsRequired()
                    .HasColumnName("Finalized_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FinalizedDate)
                    .HasColumnName("Finalized_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Keyword).HasColumnType("text");

                entity.Property(e => e.ProposalTitle)
                    .IsRequired()
                    .HasColumnName("Proposal_Title")
                    .HasMaxLength(151)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("Status_Id");

                entity.Property(e => e.SubmitDate)
                    .HasColumnName("Submit_Date")
                    .HasColumnType("date");

                entity.Property(e => e.Summary).HasColumnType("text");

                entity.Property(e => e.TeamId).HasColumnName("Team_Id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Proposal)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposal_Status_Proposal");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Proposal)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposal_Team_Header");
            });

            modelBuilder.Entity<ProposalDetail>(entity =>
            {
                entity.ToTable("Proposal_Detail");

                entity.Property(e => e.ProposalDetailId)
                    .HasColumnName("Proposal_Detail_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.EditedBy)
                    .IsRequired()
                    .HasColumnName("Edited_By")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastEdited)
                    .HasColumnName("Last_Edited")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProposalId).HasColumnName("Proposal_Id");

                entity.Property(e => e.Subchapter)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubchapterId).HasColumnName("Subchapter_Id");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.ProposalDetail)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposal_Detail_Proposal");

                entity.HasOne(d => d.SubchapterNavigation)
                    .WithMany(p => p.ProposalDetail)
                    .HasForeignKey(d => d.SubchapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposal_Detail_Template_Proposal_Detail");
            });

            modelBuilder.Entity<RubricDescription>(entity =>
            {
                entity.ToTable("Rubric_Description");

                entity.Property(e => e.RubricDescriptionId)
                    .HasColumnName("Rubric_Description_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bad)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Criteria)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Distinction)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Excellence)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Good)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RubricDetail>(entity =>
            {
                entity.ToTable("Rubric_Detail");

                entity.Property(e => e.RubricDetailId)
                    .HasColumnName("Rubric_Detail_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.RubricDescriptionId).HasColumnName("Rubric_Description_Id");

                entity.Property(e => e.RubricHeaderId).HasColumnName("Rubric_Header_Id");

                entity.HasOne(d => d.RubricDescription)
                    .WithMany(p => p.RubricDetail)
                    .HasForeignKey(d => d.RubricDescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rubric_Detail_Rubric_Description");

                entity.HasOne(d => d.RubricHeader)
                    .WithMany(p => p.RubricDetail)
                    .HasForeignKey(d => d.RubricHeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rubric_Detail_Rubric_Header");
            });

            modelBuilder.Entity<RubricHeader>(entity =>
            {
                entity.ToTable("Rubric_Header");

                entity.Property(e => e.RubricHeaderId)
                    .HasColumnName("Rubric_Header_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ScoringDetail>(entity =>
            {
                entity.ToTable("Scoring_Detail");

                entity.Property(e => e.ScoringDetailId)
                    .HasColumnName("Scoring_Detail_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.JuryDetailId).HasColumnName("Jury_Detail_Id");

                entity.Property(e => e.RubricDetailId).HasColumnName("Rubric_Detail_Id");

                entity.HasOne(d => d.JuryDetail)
                    .WithMany(p => p.ScoringDetail)
                    .HasForeignKey(d => d.JuryDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Scoring_Detail_Jury_Detail");

                entity.HasOne(d => d.RubricDetail)
                    .WithMany(p => p.ScoringDetail)
                    .HasForeignKey(d => d.RubricDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Scoring_Detail_Rubric_Detail");
            });

            modelBuilder.Entity<StatusProposal>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("Status_Proposal");

                entity.Property(e => e.StatusId)
                    .HasColumnName("Status_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TeamDetail>(entity =>
            {
                entity.ToTable("Team_Detail");

                entity.Property(e => e.TeamDetailId)
                    .HasColumnName("Team_Detail_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmailMember)
                    .IsRequired()
                    .HasColumnName("Email_Member")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SizeShirt)
                    .HasColumnName("Size_Shirt")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TeamId).HasColumnName("Team_Id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamDetail)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Team_Detail_Team_Header");
            });

            modelBuilder.Entity<TeamHeader>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.ToTable("Team_Header");

                entity.Property(e => e.TeamId)
                    .HasColumnName("Team_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TemplateProposal>(entity =>
            {
                entity.HasKey(e => e.ChapterId);

                entity.ToTable("Template_Proposal");

                entity.Property(e => e.ChapterId)
                    .HasColumnName("Chapter_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Chapter)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TemplateProposalDetail>(entity =>
            {
                entity.HasKey(e => e.SubchapterId);

                entity.ToTable("Template_Proposal_Detail");

                entity.Property(e => e.SubchapterId)
                    .HasColumnName("Subchapter_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChapterId).HasColumnName("Chapter_Id");

                entity.Property(e => e.Subchapter)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.TemplateProposalDetail)
                    .HasForeignKey(d => d.ChapterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Template_Proposal_Detail_Template_Proposal");
            });

            modelBuilder.Entity<UserHeader>(entity =>
            {
                entity.HasKey(e => e.BinusianId);

                entity.ToTable("User_Header");

                entity.Property(e => e.BinusianId)
                    .HasColumnName("Binusian_Id")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BusinessUnit)
                    .HasColumnName("Business_Unit")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Handphone)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.JobBand)
                    .IsRequired()
                    .HasColumnName("Job_Band")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LeadId)
                    .HasColumnName("Lead_Id")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Role).HasDefaultValueSql("((3))");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.InverseLead)
                    .HasForeignKey(d => d.LeadId)
                    .HasConstraintName("FK_User_Header_User_Header");
            });

            modelBuilder.Entity<Voting>(entity =>
            {
                entity.Property(e => e.VotingId)
                    .HasColumnName("Voting_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProposalId).HasColumnName("Proposal_Id");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.Voting)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voting_Proposal");
            });
        }
    }
}
