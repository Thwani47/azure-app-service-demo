using System.Security.Cryptography;
using System.Text;

namespace MarvellousApi.Helpers;

public static class ApiUtils
{
    public static string ComputeKeyHash(string timestamp, string privateKey, string publicKey)
    {
        var md5 = MD5.Create();
        var input = $"{timestamp}{privateKey}{publicKey}";
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = md5.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}