using System.Collections.Generic;

namespace DotsCore
{
	public class DotsGameInitializationData
	{
		public int Seed { get; }
		public int Width { get; }
		public int Height { get; }
		public List<Color> Colors { get; }

		public DotsGameInitializationData(
			int seed,
			int width,
			int height,
			List<Color> colors)
		{
			Seed = seed;
			Width = width;
			Height = height;
			Colors = colors;
		}
	}
}