using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BDDValidator.Helpers;

public class Config
{
    public static void Limpar()
    {
        var host = Environment.GetEnvironmentVariable("HOST_CHROMEDRIVER");
        HttpClient client = new HttpClient();
        string url = host + "/api/Administradores/1/truncate";
        Task.FromResult(client.GetAsync(url));
    }
}