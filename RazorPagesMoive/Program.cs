using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RazorPagesMoive
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//var config = new ConfigurationBuilder().AddCommandLine(args).Build();

			//var host = new WebHostBuilder()
			//	.UseConfiguration(config)
			//	.UseContentRoot(Directory.GetCurrentDirectory())
			//	.UseKestrel()
			//	.UseIISIntegration()
			//	.UseStartup<Startup>()
			//	.Build();

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
