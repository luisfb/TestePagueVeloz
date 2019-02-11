using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PagueVeloz.Domain.Interfaces;
using PagueVeloz.Infra;

namespace PagueVeloz.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webhost = BuildWebHost(args);
            webhost.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
