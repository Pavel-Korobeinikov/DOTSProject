using Services.Configuration.Structure.Battle;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities.Battle
{
	public class DotColorScriptableObjectEntity : ScriptableObject
	{
		[SerializeField] private string _colorName = default;
		
		public DotColorEntity GetStructureData()
		{
			return new DotColorEntity(_colorName);
		}
	}
}