﻿namespace Application
{
	public class LaunchData
	{
		public string ConfigurationPath { get; }

		public LaunchData(string configurationPath)
		{
			ConfigurationPath = configurationPath;
		}
	}
}