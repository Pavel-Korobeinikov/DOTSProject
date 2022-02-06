using System.Collections.Generic;
using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ViewModel
{
	public abstract class BaseViewModel : MonoBehaviour
	{
		private readonly List<BaseViewModel> _childViewModels = new List<BaseViewModel>();
		private readonly List<UniTask> _taskCache = new List<UniTask>();
		
		public void SetDependencies()
		{
			SetChildViewModels();
			
			foreach (var child in _childViewModels)
			{
				child.SetDependencies();
			}
		}
		
		public void InitializeWithChildViewModels()
		{
			Initialize();
			
			foreach (var child in _childViewModels)
			{
				child.InitializeWithChildViewModels();
			}
		}

		public void SubscribeWithChildViewModels()
		{
			Subscribe();
			
			foreach (var child in _childViewModels)
			{
				child.SubscribeWithChildViewModels();
			}
		}
		
		public async UniTask ActivateWithChildViewModels()
		{
			_taskCache.Clear();
			foreach (var child in _childViewModels)
			{
				_taskCache.Add(Activate());
				_taskCache.Add(child.ActivateWithChildViewModels());
			}

			await UniTask.WhenAll(_taskCache);
		}
		
		public async UniTask DeactivateWithChildViewModels()
		{
			_taskCache.Clear();
			foreach (var child in _childViewModels)
			{
				_taskCache.Add(Deactivate());
				_taskCache.Add(child.DeactivateWithChildViewModels());
			}

			await UniTask.WhenAll(_taskCache);
		}
		
		public void UnsubscribeWithChildViewModels()
		{
			Unsubscribe();
			
			foreach (var child in _childViewModels)
			{
				child.UnsubscribeWithChildViewModels();
			}
		}
		
		public void UtilizeWithChildViewModels()
		{
			Utilize();
			
			foreach (var child in _childViewModels)
			{
				child.UtilizeWithChildViewModels();
			}

			RemoveAllDependencies();
		}

		protected virtual void SetChildViewModels()
		{
			
		}

		protected virtual void Initialize()
		{
			
		}

		protected virtual void Subscribe()
		{
			
		}

		protected virtual UniTask Activate()
		{
			return UniTask.CompletedTask;
		}

		protected virtual UniTask Deactivate()
		{
			return UniTask.CompletedTask;
		}

		protected virtual void Unsubscribe()
		{
			
		}
		
		protected virtual void Utilize()
		{
			
		}

		protected void AddChildViewModel(BaseViewModel child)
		{
			if (child == null)
			{
				MessageLogger.LogError($"Child ViewModel in parent with type {GetType()} is null");
				
				return;	
			}
			if (!_childViewModels.Contains(child))
			{
				_childViewModels.Add(child);
			}
			else
			{
				MessageLogger.LogError("View Model already have same child.");
			}
		}

		private void RemoveAllDependencies()
		{
			_childViewModels.Clear();
		}
	}
}