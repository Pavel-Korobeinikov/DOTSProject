using System.Collections.Generic;

namespace Services.Configuration.Structure.Battle
{
	public class BattleEntity
	{
		public int Width { get; }
		public int Height { get; }
		public List<DotEntity> Dots { get; }

		public BattleEntity(
			int width,
			int height,
			List<DotEntity> dots)
		{
			Width = width;
			Height = height;
			Dots = dots;
		}
	}
}