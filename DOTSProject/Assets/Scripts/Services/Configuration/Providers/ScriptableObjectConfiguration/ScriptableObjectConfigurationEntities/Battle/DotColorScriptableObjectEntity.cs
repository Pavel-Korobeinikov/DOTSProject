﻿using Services.Configuration.Structure.Battle;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities.Battle
{
	[CreateAssetMenu(fileName = "Dot Color", menuName = "Configuration/Battle/Dot Color", order = 1)]
	public class DotColorScriptableObjectEntity : ScriptableObject
	{
		[SerializeField] private string _colorName = default;
		
		public DotColorEntity GetStructureData()
		{
			return new DotColorEntity(_colorName);
		}
	}
}