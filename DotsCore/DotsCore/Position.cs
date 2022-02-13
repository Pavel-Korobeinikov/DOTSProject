namespace DotsCore
{
	public struct Position
	{
		public int X { get; }
		public int Y { get; }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}
		
		public override bool Equals(object obj)
		{
			if ((obj == null) || GetType() != obj.GetType())
			{
				return false;
			}
			
			var comparePosition = (Position) obj;
			return X == comparePosition.X && Y == comparePosition.Y;
		}

		public override int GetHashCode()
		{
			return (X << 2) ^ Y;
		}
	}
}