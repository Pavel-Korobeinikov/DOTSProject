using System.Collections.Generic;
using Application.MessageLog;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ViewModel;

namespace View
{
	public abstract class BaseView : MonoBehaviour
	{
		private readonly List<BaseView> _childViews = new List<BaseView>();
		private readonly List<UniTask> _taskCache = new List<UniTask>();
		
		public void InitializeWithChildViews()
		{
			Initialize();
			
			foreach (var child in _childViews)
			{
				child.InitializeWithChildViews();
			}
		}
		
		public void SetDependencies()
		{
			SetChildViews();
			
			foreach (var child in _childViews)
			{
				child.SetDependencies();
			}
		}

		public void SubscribeWithChildViews()
		{
			Subscribe();
			
			foreach (var child in _childViews)
			{
				child.SubscribeWithChildViews();
			}
		}
		
		public async UniTask ActivateWithChildViews()
		{
			_taskCache.Clear();
			_taskCache.Add(Activate());
			foreach (var child in _childViews)
			{
				_taskCache.Add(child.ActivateWithChildViews());
			}

			await UniTask.WhenAll(_taskCache);
		}
		
		public async UniTask DeactivateWithChildViews()
		{
			_taskCache.Clear();
			_taskCache.Add(Deactivate());
			foreach (var child in _childViews)
			{
				_taskCache.Add(child.DeactivateWithChildViews());
			}

			await UniTask.WhenAll(_taskCache);
		}
		
		public void UnsubscribeWithChildViews()
		{
			Unsubscribe();
			
			foreach (var child in _childViews)
			{
				child.UnsubscribeWithChildViews();
			}
		}
		
		public void UtilizeWithChildViews()
		{
			Utilize();
			
			foreach (var child in _childViews)
			{
				child.UtilizeWithChildViews();
			}

			RemoveAllDependencies();
		}

		protected virtual void SetChildViews()
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

		protected void AddChildView(BaseView child)
		{
			if (child == null)
			{
				MessageLogger.LogError($"Child View in parent with type {GetType()} is null");
				
				return;	
			}
			if (!_childViews.Contains(child))
			{
				child.Initialize();
				_childViews.Add(child);
			}
			else
			{
				MessageLogger.LogError("View already have same child.");
			}
		}

		private void RemoveAllDependencies()
		{
			_childViews.Clear();
		}
	}
	
	public class BaseView<TViewModel> : BaseView, IViewModelHolder where TViewModel : BaseViewModel, new()
    {
	    protected TViewModel ViewModel { get; set; }

        public void SetViewModel(BaseViewModel viewModel)
        {
	        ViewModel = (TViewModel) viewModel;
        }
    }
}