using System;
using System.Collections.Generic;

namespace DotsCore
{
	public class DotsField
	{
		private readonly int _width;
		private readonly int _height;
		private readonly List<Color> _colors;
		private readonly Random _random;
		private readonly Dot[,] _grid;

		private readonly List<Dot> _connections = new List<Dot>();

		public DotsField(
			int width,
			int height,
			List<Color> colors,
			Random random)
		{
			_width = width;
			_height = height;
			_colors = colors;
			_random = random;
			_grid = new Dot[width, height];
		}

		public void Generate()
		{
			for (var x = 0; x < _width; x++)
			{
				for (var y = 0; y < _height; y++)
				{
					var position = new Position(x, y);
					_grid[x, y] = new Dot(_colors.GetRandomDotColor(_random), position);
				}
			}
		}

		public void AddConnection(
			Position fromPosition,
			Position toPosition)
		{
			var fromDotConnection = _grid[fromPosition.X, fromPosition.Y];
			var toDotConnection = _grid[toPosition.X, toPosition.Y];

			if (fromDotConnection.Color.Name == toDotConnection.Color.Name &&
			    (_connections.Count == 0 || _connections[_connections.Count - 1] == fromDotConnection) &&
			    Math.Abs(fromPosition.X - toPosition.X) <= 1 && Math.Abs(fromPosition.Y - toPosition.Y) <= 1 &&
			    (Math.Abs(fromPosition.X - toPosition.X) != 1 || Math.Abs(fromPosition.Y - toPosition.Y) != 1))
			{
				_connections.Add(toDotConnection);
			}
			else
			{
				throw new Exception(
					$"Can't connect X: {fromPosition} Y: {fromPosition} with X: {toPosition.X} Y: {toPosition.Y}");
			}
		}

		public void RemoveConnection(Position fromPosition)
		{
			var lastDotConnection = _grid[fromPosition.X, fromPosition.Y];

			if (_connections.Count != 0 &&
			    _connections[_connections.Count - 1] == lastDotConnection)
			{
				_connections.Remove(lastDotConnection);
			}
			else
			{
				throw new Exception($"Can't disconnect X: {fromPosition.X} Y: {fromPosition.Y}");
			}
		}

		public void RemoveConnectionsFromGrid()
		{
			if (_connections.Count > 1)
			{
				foreach (var dot in _connections)
				{
					RemoveDotFromGrid(dot.Position);
				}
			}
		}

		public void Fall()
		{
			for (var x = 0; x < _width; x++)
			{
				for (var y = _height - 1; y >= 0; y--)
				{
					var upperY = y - 1;
					if (upperY != 0)
					{
						break;
					}

					var currentDot = _grid[x, y];
					if (currentDot != null)
					{
						continue;
					}

					var upperDot = _grid[x, upperY];
					if (upperDot == null)
					{
						continue;
					}

					_grid[x, y] = upperDot;
					_grid[x, upperY] = null;
				}
			}

			for (var x = 0; x < _width; x++)
			{
				for (var y = _height - 1; y >= 0; y--)
				{
					var upperY = y - 1;
					if (upperY != 0)
					{
						break;
					}

					var currentDot = _grid[x, y];
					var upperDot = _grid[x, upperY];
					if (currentDot == null && upperDot != null)
					{
						Fall();
					}
				}
			}
		}

		public void Fill()
		{
			for (var x = 0; x < _width; x++)
			{
				for (var y = _height - 1; y >= 0; y--)
				{
					if (_grid[x, y] != null)
					{
						continue;
					}

					var position = new Position(x, y);
					_grid[x, y] = new Dot(_colors.GetRandomDotColor(_random), position);
				}
			}
		}

		private void RemoveDotFromGrid(Position position)
		{
			_grid[position.X, position.Y] = null;
		}
	}
}