using Services.Configuration.Structure.Battle;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities.Battle
{
	[CreateAssetMenu(fileName = "Dot", menuName = "Configuration/Battle/Dot", order = 1)]
	public class DotScriptableObjectEntity : ScriptableObject
	{
		[SerializeField] private DotColorScriptableObjectEntity _color = default;
		
		public DotEntity GetStructureData()
		{
			return new DotEntity(_color.GetStructureData());
		}
	}
}