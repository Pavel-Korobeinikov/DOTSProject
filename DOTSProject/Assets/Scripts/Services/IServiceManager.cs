namespace Services
{
	public interface IServiceManager
	{
		T GetService<T>() where T : IService;
	}
}