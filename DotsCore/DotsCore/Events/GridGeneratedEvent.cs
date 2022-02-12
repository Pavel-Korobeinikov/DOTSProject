namespace DotsCore.Events
{
	public class GridGeneratedEvent : GridUpdatedBase
	{
		public GridGeneratedEvent(Dot[,] updatedGrid) : base(updatedGrid)
		{
			
		}
	}
}