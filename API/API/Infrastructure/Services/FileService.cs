using API.Application.Common;
using API.Infrastructure.Common;
using Microsoft.AspNetCore.StaticFiles;

namespace API.Infrastructure.Services;

public class FileService : IFileService
{

    public async Task<string> UploadFile(IFormFile file)
    {
        try
        {
            FileInfo _FileInfo = new(file.FileName);
            string FileName = FileHelper.GetFileName(file.FileName);
            //C:\Users\moust\Documents\dot-angular\API\API\Ressources\Images\638338382714736643\3d7f79e8-d746-4721-b6a1-ce760489bcba.jpg
            var _GetFilePath = FileHelper.GetFilePath(FileName);
            using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
            {
                await file.CopyToAsync(_FileStream);
            }
            return FileHelper.GetRelativeFileUrl(FileName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
