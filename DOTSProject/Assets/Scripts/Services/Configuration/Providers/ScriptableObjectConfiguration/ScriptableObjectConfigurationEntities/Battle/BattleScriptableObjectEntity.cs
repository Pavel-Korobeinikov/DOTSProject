using System.Collections.Generic;
using System.Linq;
using Services.Configuration.Structure.Battle;
using UnityEngine;

namespace Services.Configuration.Providers.ScriptableObjectConfiguration.ScriptableObjectConfigurationEntities.Battle
{
	[CreateAssetMenu(fileName = "Battle Configuration", menuName = "Configuration/Battle/Battle Configuration", order = 1)]
	public class BattleScriptableObjectEntity : ScriptableObject
	{
		[Header("Grid Settings")]
		[SerializeField] private int _width = default;
		[SerializeField] private int _height = default;
		[SerializeField] private List<DotScriptableObjectEntity> _dots = default;

		public BattleEntity GetStructureData()
		{
			return new BattleEntity(
				_width,
				_height,
				_dots.Select(dot => dot.GetStructureData()).ToList());
		}
	}
}