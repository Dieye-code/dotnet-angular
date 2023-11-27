namespace API.Application.Common;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file);
}
