using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mid_10_Evidence_API.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        [Required, StringLength(50)]
        public string CandidateName { get; set; } = default!;
        [Required, DataType(DataType.Date)]
        public System.DateTime BirthDate { get; set; }
        [Required, StringLength(30)]
        public string AppliedFor { get; set; } = default!;
        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal ExpectedSalary { get; set; }
        public bool WorkFromHome { get; set; }
        [Required, StringLength(120)]
        public string Picture { get; set; } = default!;
        public virtual ICollection<Qualification>? Qualifications { get; set; } = new List<Qualification>();
    }
    public class Qualification
    {
        public int QualificationId { get; set; }
        [Required, StringLength(50)]
        public string Degree { get; set; } = default!;
        [Required]
        public int PassingYear { get; set; }
        [Required, StringLength(50)]
        public string Institute { get; set; } = default!;
        [Required, StringLength(20)]
        public string Result { get; set; } = default!;
        [Required, ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public virtual Candidate? Candidate { get; set; } = default!;
    }
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; } = default!;
        public DbSet<Qualification> Qualifications { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                CandidateId=1,
                CandidateName = "Candidate 1",
                AppliedFor = "Executive",
                BirthDate = new DateTime(1999, 4, 1),
                ExpectedSalary = 10000.00M,
                Picture = "1.jpg",
                WorkFromHome = true
            });
            modelBuilder.Entity<Qualification>().HasData(new Qualification 
            { 
                QualificationId = 1, 
                CandidateId = 1, 
                Degree = "HSC", 
                Institute = "ABC", 
                PassingYear = 1999, 
                Result = "B" 
            });
            
        }
    }
    
}
