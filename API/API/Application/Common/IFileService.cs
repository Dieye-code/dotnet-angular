namespace API.Application.Common;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file);
    Task<(byte[], string, string)> DownloadFile(string fileName);
}
