using System;
using Cysharp.Threading.Tasks;
using Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities;
using Services.Configuration.Structure;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration
{
	// Configuration better to separate from engine and make it with XML or something like that to make life of Game Designers simply.
	// But for test in small game it's enough.
	public class ScriptableObjectConfigurationProvider : IConfigurationProvider
	{
		private readonly string _configurationPath;
		
		private GameConfigurationScriptableObjectEntity _configurationEntity;

		public ScriptableObjectConfigurationProvider(string configurationPath)
		{
			_configurationPath = configurationPath;
		}

		public async UniTask<GameConfigurationEntity> LoadConfiguration()
		{
			_configurationEntity = await Resources.LoadAsync<GameConfigurationScriptableObjectEntity>(_configurationPath) as GameConfigurationScriptableObjectEntity;

			if (_configurationEntity == null)
			{
				throw new Exception($"Wrong configuration path: {_configurationPath}");
			}

			return await CreateGameConfiguration();
		}

		private async UniTask<GameConfigurationEntity> CreateGameConfiguration()
		{
			return await UniTask.FromResult(_configurationEntity.GetStructureData());
		}
	}
}