using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace RazorPagesMoive.Model
{
	public class ConfigurationMonitor : IConfigurationMonitor
	{
		private byte[] _appsettingsHash = new byte[20];
		private byte[] _appsettingsEnvHash = new byte[20];
		private readonly IHostingEnvironment _env;

		#region snippet2
		public ConfigurationMonitor(IConfiguration config, IHostingEnvironment env)
		{
			_env = env;

			ChangeToken.OnChange<IConfigurationMonitor>(
				() => config.GetReloadToken(),
				InvokeChanged,
				this);
		}

		public bool MonitoringEnabled { get; set; } = false;
		public string CurrentState { get; set; } = "Not monitoring";
		#endregion

		#region snippet3
		private void InvokeChanged(IConfigurationMonitor state)
		{
			if (MonitoringEnabled)
			{
				byte[] appsettingsHash = Utilities.ComputeHash("appSettings.json");
				byte[] appsettingsEnvHash =
					Utilities.ComputeHash($"appSettings.{_env.EnvironmentName}.json");

				if (!_appsettingsHash.SequenceEqual(appsettingsHash) ||
					!_appsettingsEnvHash.SequenceEqual(appsettingsEnvHash))
				{
					string message = $"State updated at {DateTime.Now}";


					_appsettingsHash = appsettingsHash;
					_appsettingsEnvHash = appsettingsEnvHash;
				}
			}
		}
		#endregion
	}
}
