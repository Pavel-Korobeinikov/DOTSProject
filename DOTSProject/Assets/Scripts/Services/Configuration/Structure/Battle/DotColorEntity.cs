namespace Services.Configuration.Structure.Battle
{
	public class DotColorEntity
	{
		public string Name { get; }
		public float R { get; }
		public float G { get; }
		public float B { get; }

		public DotColorEntity(string name, float r, float g, float b)
		{
			Name = name;
			R = r;
			G = g;
			B = b;
		}
	}
}