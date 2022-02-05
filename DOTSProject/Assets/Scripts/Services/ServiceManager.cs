using System;
using System.Collections.Generic;
using Application.MessageLog;
using Cysharp.Threading.Tasks;

namespace Services
{
	public class ServiceManager : IServiceManager
	{
		private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
		
		public async UniTask RegisterService<T>(T serviceObject) where T : class, IService
		{
			if (_services.TryGetValue(typeof(T), out var service))
			{
				MessageLogger.LogError("Service already registered.");
			}

			if (serviceObject is IInitializable initializable)
			{
				await initializable.Initialize();
			}

			_services.Add(typeof(T), serviceObject);
		}

		public T GetService<T>() where T : IService
		{
			if (!_services.TryGetValue(typeof(T), out var service))
			{
				throw new Exception("Service is not registered.");
			}
			
			return (T) service;
		}
	}
}