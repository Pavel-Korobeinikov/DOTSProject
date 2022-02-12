namespace DotsCore
{
	public class Dot
	{
		public Color Color { get; }
		public Position Position { get; }

		public Dot(Color color, Position position)
		{
			Color = color;
			Position = position;
		}
	}
}