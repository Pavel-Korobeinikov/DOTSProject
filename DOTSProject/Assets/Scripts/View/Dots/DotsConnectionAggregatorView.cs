using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ViewModel.Dots;

namespace View.Dots
{
	public class DotsConnectionAggregatorView : BaseView<DotsConnectionAggregatorViewModel>
	{
		[SerializeField] private LineRenderer _line = default;
		
		private DotsFieldView _fieldView;
		
		private readonly List<Vector3> _linePoints = new List<Vector3>();
		
		public void Connect(DotsFieldView fieldView)
		{
			_fieldView = fieldView;
		}

		protected override UniTask Activate()
		{
			ViewModel.ConnectionUpdated += OnConnectionUpdated;
			
			return UniTask.CompletedTask;
		}

		protected override UniTask Deactivate()
		{
			ViewModel.ConnectionUpdated -= OnConnectionUpdated;
			
			return UniTask.CompletedTask;
		}

		private void OnConnectionUpdated()
		{
			_linePoints.Clear();

			if (ViewModel.ConnectedDots.Count >= 1)
			{
				var lastConnectedDot = ViewModel.ConnectedDots.Last();
				SetColor(lastConnectedDot.R, lastConnectedDot.G, lastConnectedDot.B);
				
				foreach (var dotViewModel in ViewModel.ConnectedDots)
				{
					var dotView = _fieldView.Grid[dotViewModel.Position.X, dotViewModel.Position.Y];
					_linePoints.Add(dotView.transform.position);
				}
			}

			_line.positionCount = _linePoints.Count;
			_line.SetPositions(_linePoints.ToArray());
		}

		private void SetColor(float r, float g, float b)
		{
			_line.startColor = new Color(r, g, b);
			_line.endColor = new Color(r, g, b);
		}
	}
}