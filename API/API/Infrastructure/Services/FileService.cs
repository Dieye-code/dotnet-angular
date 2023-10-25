using API.Application.Common;
using API.Infrastructure.Common;
using Microsoft.AspNetCore.StaticFiles;

namespace API.Infrastructure.Services;

public class FileService : IFileService
{
    public async Task<(byte[], string, string)> DownloadFile(string fileName)
    {
        try
        {
            var _GetFilePath = FileHelper.GetFilePath(fileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
            {
                _ContentType = "application/octet-stream";
            }
            var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
            return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
        }
        catch (Exception )
        {
            throw ;
        }
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        try
        {
            FileInfo _FileInfo = new(file.FileName);
            string FileName = file.FileName;
            //C:\Users\moust\Documents\dot-angular\API\API\Ressources\Images\638338382714736643\3d7f79e8-d746-4721-b6a1-ce760489bcba.jpg
            var _GetFilePath = FileHelper.GetFilePath(FileName);
            using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
            {
                await file.CopyToAsync(_FileStream);
            }
            return _GetFilePath.Substring(_GetFilePath.LastIndexOf("\\")+1);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
