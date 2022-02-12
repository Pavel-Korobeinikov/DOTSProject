using System;
using System.Collections.Generic;

namespace DotsCore
{
	public static class ColorsExtension
	{
		public static Color GetRandomDotColor(this List<Color> colors, Random random)
		{
			var randomColorIndex = random.Next(0, colors.Count);

			return colors[randomColorIndex];
		}
	}
}