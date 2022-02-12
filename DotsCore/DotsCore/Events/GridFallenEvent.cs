namespace DotsCore.Events
{
	public class GridFallenEvent : GridUpdatedBase
	{
		public GridFallenEvent(Dot[,] updatedGrid) : base(updatedGrid)
		{
			
		}
	}
}