using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mid_10_Evidence_API.Models;
using Mid_10_Evidence_API.ViewModels;

namespace Mid_10_Evidence_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly CandidateDbContext db;
        private readonly IWebHostEnvironment env;
        public CandidatesController(CandidateDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            return await db.Candidates.ToListAsync();
        }
        [HttpGet("VM")]
        public async Task<ActionResult<IEnumerable<CandidateViewModel>>> GetCandidateViewModels()
        {
            var data = await db.Candidates.Include(x => x.Qualifications)
                .Select(c =>
               new CandidateViewModel
               {
                   CandidateId = c.CandidateId,
                   CandidateName = c.CandidateName,
                   AppliedFor = c.AppliedFor,
                   BirthDate = c.BirthDate,
                   ExpectedSalary = c.ExpectedSalary,
                   Picture = c.Picture,
                   WorkFromHome = c.WorkFromHome,
                   QualifactionCount = c.Qualifications == null ? 0 : c.Qualifications.Count(),
                   CanDelete = c.Qualifications == null ? true : !c.Qualifications.Any(),
                   Qualifications = c.Qualifications == null ? new List<Qualification>():c.Qualifications.ToList()

               })
                .ToListAsync();
             return data;
        }
        [HttpGet("VM/{id}")]
        public async Task<ActionResult<CandidateViewModel>> GetCandidateViewModel(int id)
        {
            var data = await db.Candidates.Include(x => x.Qualifications)
                .Select(c =>
               new CandidateViewModel
               {
                   CandidateId = c.CandidateId,
                   CandidateName = c.CandidateName,
                   AppliedFor = c.AppliedFor,
                   BirthDate = c.BirthDate,
                   ExpectedSalary = c.ExpectedSalary,
                   Picture = c.Picture,
                   WorkFromHome = c.WorkFromHome,
                   QualifactionCount = c.Qualifications == null ? 0 : c.Qualifications.Count(),
                   CanDelete = c.Qualifications == null ? true : !c.Qualifications.Any(),
                   Qualifications = c.Qualifications == null ? new List<Qualification>() : c.Qualifications.ToList()

               })
                .FirstOrDefaultAsync(x => x.CandidateId == id);
            if(data == null) return NotFound();
            else return data;
        }

        [HttpGet("{id}/Qualifications")]
        public async Task<ActionResult<IEnumerable<Qualification>>> GetQualificationsOfCandidate(int id)
        {
            return await db.Qualifications.Where(x=> x.CandidateId == id).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidateInputModel(CandidateInputModel model)
        {
            var candidate = new Candidate
            {
                CandidateName = model.CandidateName,
                BirthDate = model.BirthDate,
                ExpectedSalary = model.ExpectedSalary,
                AppliedFor = model.AppliedFor,
                WorkFromHome = model.WorkFromHome,
                Picture = model.Picture,
                Qualifications = model.Qualifications
            };
            await db.Candidates.AddAsync(candidate);
            await db.SaveChangesAsync();
            return candidate;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Candidate>> PutCandidateInputModel(int id, CandidateEditModel model)
        {
            var c= await db.Candidates.FirstOrDefaultAsync(x=> id==x.CandidateId);
           if(c==null) return NotFound();

            c.CandidateName = model.CandidateName;
            c.BirthDate = model.BirthDate;
            c.ExpectedSalary = model.ExpectedSalary;
                c.AppliedFor = model.AppliedFor;
            c.WorkFromHome = model.WorkFromHome;
            var qs = db.Qualifications.Where(q=> q.CandidateId == c.CandidateId); ;
            db.Qualifications.RemoveRange(qs);
            if (model.Qualifications != null)
            {
                
                foreach (var q in model.Qualifications)
                {
                    c.Qualifications?.Add(q);
                }
            }
                
            
            
            await db.SaveChangesAsync();
            return c;
        }
        [HttpPost("Upload/{id}")]
        public async Task<ActionResult<ImagePathResponse>> UploadPicture(int id, IFormFile picture)
        {
            var candidate = await db.Candidates.FirstOrDefaultAsync(x => x.CandidateId == id);
            if(candidate == null) return NotFound();
            var ext = Path.GetExtension(picture.FileName);
            string fileName = Path.GetFileNameWithoutExtension( Path.GetRandomFileName()) + ext;
            string savePath = Path.Combine(this.env.WebRootPath, "Pictures", fileName);
            FileStream fs = new FileStream(savePath, FileMode.Create);
            picture.CopyTo(fs);
            fs.Close();
            candidate.Picture = fileName;

            await db.SaveChangesAsync();
            return new ImagePathResponse { ImgPath = fileName };
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Candidate>> Delete(int id)
        {
            var c = await db.Candidates.FirstOrDefaultAsync(x=> x.CandidateId == id);
            if(c == null) return NotFound();
            db.Candidates.Remove(c);
            await db.SaveChangesAsync();
            return c;
        }
    }
}
