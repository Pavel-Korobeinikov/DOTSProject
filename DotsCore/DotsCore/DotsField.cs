using System;
using System.Collections.Generic;
using DotsCore.Events;

namespace DotsCore
{
	public class DotsField
	{
		private readonly int _width;
		private readonly int _height;
		private readonly List<Color> _colors;
		private readonly Random _random;
		private readonly EventsNotifier _eventsNotifier;
		private readonly Dot[,] _grid;

		public Dot[,] Grid => _grid;

		public DotsField(
			int width,
			int height,
			List<Color> colors,
			Random random,
			EventsNotifier eventsNotifier)
		{
			_width = width;
			_height = height;
			_colors = colors;
			_random = random;
			_eventsNotifier = eventsNotifier;
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
			
			_eventsNotifier.RiseEvent(new GridGeneratedEvent(_grid));
		}

		public void RemoveDotFromGrid(Position position)
		{
			_grid[position.X, position.Y] = null;
			
			_eventsNotifier.RiseEvent(new DotRemovedFromGridEvent(position.X, position.Y));
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
			
			_eventsNotifier.RiseEvent(new GridFallenEvent(_grid));
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
			
			_eventsNotifier.RiseEvent(new GridFilledEvent(_grid));
		}
	}
}