using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Data;
using MobileShopAPI.Helpers;
using MobileShopAPI.Models;
using MobileShopAPI.Responses;
using MobileShopAPI.ViewModel;
using System.Collections.Immutable;

namespace MobileShopAPI.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetAllAsync();

        Task<List<Report>> GetAllReportOfUserAsync(string userId);

        Task<List<Report>> GetAllReportedInfoOfUserAsync(string reportedUserId);

        Task<List<Report>> GetAllReportedInfoOfProductAsync(long reportedProductId);

        Task<Report?> GetByIdAsync(long id);

        Task<ReportResponse> AddAsync(ReportViewModel report);

        Task<ReportResponse> UpdateAsync(long id, ReportViewModel report);

        Task<ReportResponse> DeleteAsync(long id);
    }

    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEvidenceService _evidenceService;

        public ReportService(ApplicationDbContext context, IEvidenceService evidenceService)
        {
            _context = context;
            _evidenceService = evidenceService;
        }
        public async Task<ReportResponse> AddAsync(ReportViewModel report)
        {
            var _report = new Report
            {
                Subject = report.Subject,
                Body = report.Body,
                ReportedUserId = report.ReportedUserId,
                ReportedProductId = report.ReportedProductId,
                UserId = report.UserId,
                ReportCategoryId = report.ReportCategoryId,
                CreatedDate = null
            };
            _context.Add(_report);
            await _context.SaveChangesAsync();

            if (report.Evidences != null)
                foreach (var item in report.Evidences)
                {
                    var evidence = new EvidenceViewModel();
                    evidence.ImageUrl = item.ImageUrl;
                    evidence.ReportId = item.ReportId;
                    await _evidenceService.AddAsync(_report.Id, evidence);//Method of EvidenceService
                }

            return new ReportResponse
            {
                Message = "New report Added!",
                isSuccess = true
            };

        }

        public async Task<ReportResponse> DeleteAsync(long id)
        {
            var report = await _context.Reports.SingleOrDefaultAsync(report => report.Id == id);
            if (report == null)
            {
                return new ReportResponse
                {
                    Message = "Report not found!",
                    isSuccess = false
                };
            }

            if (report.Evidences != null)
                foreach (var item in report.Evidences)
                {
                    await _evidenceService.DeleteAsync(id, item.Id);//Method of EvidenceService
                }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();


            return new ReportResponse
            {
                Message = "Report has been deleted!",
                isSuccess = true
            };
        }

        public async Task<List<Report>> GetAllAsync()
        {
            var reports = await _context.Reports.Include(r => r.Evidences).ToListAsync();
            return reports;
        }

        public async Task<List<Report>> GetAllReportOfUserAsync(string userId)
        {
            var reportOfUser = await _context.Reports.Where(r => r.UserId == userId).Include(r => r.Evidences).ToListAsync();
            return reportOfUser;
        }

        public async Task<List<Report>> GetAllReportedInfoOfUserAsync(string reportedUserId)
        {
            var reportedOfUser = await _context.Reports.Where(ru => ru.ReportedUserId == reportedUserId && (ru.Status == 0 || ru.Status == 1)).Include(ru => ru.Evidences).ToListAsync();
            return reportedOfUser;
        }

        public async Task<List<Report>> GetAllReportedInfoOfProductAsync(long reportedProductId)
        {
            var reportedOfProduct = await _context.Reports.Where(rpid => rpid.ReportedProductId == reportedProductId && rpid.Status != 2).Include(rpid => rpid.Evidences).ToListAsync();
            return reportedOfProduct;
        }

        public async Task<Report?> GetByIdAsync(long id)
        {
            var report = await _context.Reports.Include(r => r.Evidences).SingleOrDefaultAsync(report => report.Id == id);
            if (report != null)
            {
                return report;
            }
            return null;
        }

        public async Task<ReportResponse> UpdateAsync(long id, ReportViewModel report)
        {
            var _report = await _context.Reports.FindAsync(id);

            if (_report == null)
                return new ReportResponse
                {
                    Message = "Bad request",
                    isSuccess = false
                };

            _report.Subject = report.Subject;
            _report.Body = report.Body;
            _report.ReportedProductId = report.ReportedProductId;
            _report.ReportCategoryId = report.ReportCategoryId;
            await _context.SaveChangesAsync();

            if (report.Evidences != null)
                foreach (var item in report.Evidences)
                {
                    await _evidenceService.UpdateAsync(id, item);//Method of EvidenceService
                }

            return new ReportResponse
            {
                Message = "This report Updated!",
                isSuccess = true
            };
        }
    }
}
