namespace DotsCore.Events
{
	public abstract class GridUpdatedBase : ICoreEvent
	{
		public Dot[,] UpdatedGrid { get; }

		public GridUpdatedBase(Dot[,] updatedGrid)
		{
			UpdatedGrid = updatedGrid;
		}
	}
}