using UnityEngine;

namespace Game {

	internal sealed class FollowTransform : MonoBehaviour {
    
		[SerializeField] private Transform _target;
		[SerializeField] private bool _followX;
		[SerializeField] private bool _followY;

		private Vector3 _startPosition;

		private void Start() {
			_startPosition = transform.position;
		}

		private void LateUpdate() {
			if (_target == null) {
				Debug.LogError("Please assign a target to follow.");
				return;
			}

			var newPosition = transform.position;

			if (_followX) {
				newPosition.x = _startPosition.x + (_target.position.x - _startPosition.x);
			}

			if (_followY) {
				newPosition.y = _startPosition.y + (_target.position.y - _startPosition.y);
			}

			transform.position = newPosition;
		}

	}
}

