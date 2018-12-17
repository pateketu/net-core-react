using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => 
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static IWebHost BuildWebHost(string[] args) =>
            CreateWebHostBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
