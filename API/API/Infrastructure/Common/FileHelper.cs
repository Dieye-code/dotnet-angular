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
    public static string GetAbsoluteDirectory()
    {
        var result = Path.Combine(Directory.GetCurrentDirectory(), GetRelativeDirectory());
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }
    public static string GetRelativeDirectory()
    {
        return Path.Combine( "Ressources\\Images\\", DateTime.Now.ToString("ddMMyyyy"));
    }

    public static string GetFilePath(string fileName)
    {
        var _GetStaticContentDirectory = GetStaticContentDirectory();
        var result = Path.Combine(GetRelativeDirectory(), fileName);
        return result;
    }

    public static string GetFileName(string fileName)
    {
        return Path.Combine(GetRelativeDirectory(), string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(fileName)));
    }
}
