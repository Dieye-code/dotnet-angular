using System;

namespace API.Infrastructure.Common;

public static class FileHelper
{
    public static string GetCurrentDirectory()
    {
        var result = Directory.GetCurrentDirectory();
        return result;
    }
    public static string GetStaticContentDirectory()
    {
        var result = Path.Combine(Directory.GetCurrentDirectory(), "Ressources\\Images\\", DateTime.Now.ToString("ddMMyyyy"));
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }
    public static string GetFilePath(string fileName)
    {
        var _GetStaticContentDirectory = GetStaticContentDirectory();
        var result = Path.Combine(_GetStaticContentDirectory, string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(fileName)));
        return result;
    }
}
