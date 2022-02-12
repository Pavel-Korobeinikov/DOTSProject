namespace DotsCore.Events
{
	public class GridFilledEvent : GridUpdatedBase
	{
		public GridFilledEvent(Dot[,] updatedGrid) : base(updatedGrid)
		{
			
		}
	}
}