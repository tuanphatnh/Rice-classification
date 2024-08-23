using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Models;
using MobileShopAPI.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace MobileShopAPI.Services
{
    public interface IEvidenceService
    {
        Task AddAsync(long reportId, EvidenceViewModel model);
        Task DeleteAsync(long reportId, long id);
        Task UpdateAsync(long reportId, EvidenceViewModel evidence);
    }

    public class EvidenceService : IEvidenceService
    {
        private readonly ApplicationDbContext _context;

        public EvidenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(long reportId, EvidenceViewModel model)
        {
            var ev = new Evidence
            {
                ImageUrl = model.ImageUrl,
                ReportId = reportId
            };
            _context.Evidences.Add(ev);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(long reportId, EvidenceViewModel evidence)
        {
            var _evidence = await _context.Evidences.FindAsync(evidence.Id);
            if (_evidence == null)
                return;
            _evidence.ImageUrl = evidence.ImageUrl;
            _context.Evidences.Update(_evidence);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long reportId, long id)
        {
            var product = await _context.Reports
                .Include(r => r.Evidences)
                .Where(r => r.Id == reportId)
                .SingleOrDefaultAsync();
            if (product == null) return;
            if (product.Evidences.Count <= 2) return;
            var evidence = await _context.Evidences.FindAsync(id);
            if (evidence != null)
            {
                _context.Evidences.Remove(evidence);
            }
            await _context.SaveChangesAsync();
        }
    }
}
