using System.Text;

namespace Selenium;

public class Utils
{
    public static string getRandomData()
    {
        const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
 
        StringBuilder sb = new StringBuilder();
        Random rnd = new Random();
 
        for (int i = 0; i < 10; i++)
        {
            int index = rnd.Next(chars.Length);
            sb.Append(chars[index]);
        }
        return sb.ToString();
    }
    
    public static string GetFilePathByFileName(string fileName)
    {
        string directory = AppDomain.CurrentDomain.BaseDirectory;
        string sFile = System.IO.Path.Combine(directory, "../../../" + fileName);
        string sFilePath = Path.GetFullPath(sFile);
        return sFilePath;
    }
}