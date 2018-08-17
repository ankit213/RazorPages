namespace RazorPagesMoive.Model
{
	public interface IConfigurationMonitor
	{
		bool MonitoringEnabled { get; set; }
		string CurrentState { get; set; }
	}
}
