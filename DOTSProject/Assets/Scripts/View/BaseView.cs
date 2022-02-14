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

		public ViewState ViewState { get; private set; } = ViewState.NotInitialized;

		public void InitializeWithChildViews()
		{
			Initialize();

			ViewState = ViewState.Initialized;
			
			foreach (var child in _childViews)
			{
				child.InitializeWithChildViews();
			}
		}
		
		public void SetDependencies()
		{
			SetChildViews();
			
			ViewState = ViewState.WithDependencies;
			
			foreach (var child in _childViews)
			{
				child.SetDependencies();
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
			
			ViewState = ViewState.Activated;
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
			
			ViewState = ViewState.Deactivated;
		}

		public void UtilizeWithChildViews()
		{
			Utilize();
			
			ViewState = ViewState.Utilized;
			
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

		protected virtual UniTask Activate()
		{
			return UniTask.CompletedTask;
		}

		protected virtual UniTask Deactivate()
		{
			return UniTask.CompletedTask;
		}
		
		protected virtual void Utilize()
		{
			
		}

		protected async UniTaskVoid AddChildView(BaseView child)
		{
			if (child == null)
			{
				MessageLogger.LogError($"Child View in parent with type {GetType()} is null");
				
				return;	
			}
			if (ViewState == ViewState.NotInitialized)
			{
				MessageLogger.LogError($"Can't add child view in non initialized view");
				
				return;
			}
			
			if (!_childViews.Contains(child))
			{
				if (ViewState == ViewState.Initialized ||
				    ViewState == ViewState.WithDependencies ||
				    ViewState == ViewState.Activated)
				{
					child.InitializeWithChildViews();
				}
				if (ViewState == ViewState.WithDependencies ||
				    ViewState == ViewState.Activated)
				{
					child.SetDependencies();
				}
				if (ViewState == ViewState.Activated)
				{
					await child.ActivateWithChildViews();
				}
				if (ViewState == ViewState.Deactivated ||
				    ViewState == ViewState.Utilized)
				{
					MessageLogger.LogError("Can't add child view when base view was deactivated or utilized");
				}

				_childViews.Add(child);
			}
			else
			{
				MessageLogger.LogError("View already have same child.");
			}
		}
		
		protected void RemoveChildView(BaseView child)
		{
			if (child == null)
			{
				MessageLogger.LogError($"Child View in parent with type {GetType()} is null");
				
				return;	
			}
			if (!_childViews.Contains(child))
			{
				MessageLogger.LogError($"View is not contains child {child}.");
			}
			else
			{
				if (child.ViewState == ViewState.Activated)
				{
					child.Deactivate();
				}

				child.Utilize();
				_childViews.Remove(child);
			}
		}

		private void RemoveAllDependencies()
		{
			_childViews.Clear();
		}
	}
	
	public class BaseView<TViewModel> : BaseView, IViewModelHolder where TViewModel : BaseViewModel
    {
	    public TViewModel ViewModel { get; private set; }

        public void SetViewModel(BaseViewModel viewModel)
        {
	        ViewModel = (TViewModel) viewModel;
        }
    }
}