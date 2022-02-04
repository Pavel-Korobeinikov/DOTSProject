using System;
using Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities;
using Configuration.Structure;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Configuration.Providers.ScriptableObjectConfiguration
{
	public class ScriptableObjectConfigurationProvider : IConfigurationProvider
	{
		private readonly string _configurationPath;
		
		private GameConfigurationScriptableObjectEntity _configurationEntity;

		public ScriptableObjectConfigurationProvider(string configurationPath)
		{
			_configurationPath = configurationPath;
		}

		public async UniTask<GameConfiguration> LoadConfiguration()
		{
			_configurationEntity = await Resources.LoadAsync<GameConfigurationScriptableObjectEntity>(_configurationPath) as GameConfigurationScriptableObjectEntity;

			if (_configurationEntity == null)
			{
				throw new Exception($"Wrong configuration path: {_configurationPath}");
			}

			return await CreateGameConfiguration();
		}

		private async UniTask<GameConfiguration> CreateGameConfiguration()
		{
			return await UniTask.FromResult(_configurationEntity.GetStructureData());
		}
	}
}