using System;

namespace API.Infrastructure.Common;

public static class FileHelper
{
    public static string GetStaticContentDirectory()
    {
        var result = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\Images\\", DateTime.Now.ToString("ddMMyyyy"));
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }

    public static string GetFilePath(string fileName)
    {
        var _GetStaticContentDirectory = GetStaticContentDirectory();
        var result = Path.Combine(_GetStaticContentDirectory, fileName);
        return result;
    }

    public static string GetFileName(string fileName)
    {
        return Path.Combine(string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(fileName)));
    }

    public static string GetRelativeFileUrl(string fileName)
    {
        return "ressources/"+DateTime.Now.ToString("ddMMyyyy")+"/"+fileName;
    }

    public static string GetAbsoluteFilePath(string fileName)
    {
        fileName = fileName.Replace("ressources/", "");
        fileName = fileName.Replace("/", "\\");
        return Path.Combine("Uploads\\Images", fileName);
    }
}
