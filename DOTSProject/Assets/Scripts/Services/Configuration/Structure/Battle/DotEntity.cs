namespace Services.Configuration.Structure.Battle
{
	public class DotEntity
	{
		public DotColorEntity Color { get; }
		
		public DotEntity(DotColorEntity color)
		{
			Color = color;
		}
	}
}