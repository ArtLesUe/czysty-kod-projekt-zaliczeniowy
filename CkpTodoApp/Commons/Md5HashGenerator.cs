using System.Security.Cryptography;
using System.Text;

namespace CkpTodoApp.Commons;

public static class Md5HashGenerator
{
    public static string TextToMd5(string text)
    {
        var hasher = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(text);
        var hashBytes = hasher.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }    
}