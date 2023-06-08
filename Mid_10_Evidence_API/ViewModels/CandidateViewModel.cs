using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Mid_10_Evidence_API.Models;

namespace Mid_10_Evidence_API.ViewModels
{
    public class CandidateViewModel
    {
        public int CandidateId { get; set; }
        
        public string CandidateName { get; set; } = default!;

        public System.DateTime BirthDate { get; set; } = default!;

        public string AppliedFor { get; set; } = default!;

        public decimal ExpectedSalary { get; set; } = default!;
        public bool WorkFromHome { get; set; }
        
        public string Picture { get; set; } = default!;
        
        public int QualifactionCount { get; set; }
        public bool CanDelete { get; set; }
        public virtual ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
}