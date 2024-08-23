using Microsoft.AspNetCore.Http;

namespace SignalRDemo.Helpers
{
    public interface IFileValidator
    {
        bool IsValid(IFormFile file);
    }
}
