using UnityEngine;

namespace Game {

	internal sealed class FollowTransform : MonoBehaviour {
    
		[SerializeField] private Transform _target;
		[SerializeField] private bool _followX;
		[SerializeField] private bool _followY;

		private void LateUpdate() {
			if (!_followX && !_followY) return;

			if (_followX) {
				FollowPosition(new Vector3(_target.position.x, transform.position.y, _target.position.z));
				return;
			}

			if (_followY) {
				FollowPosition(new Vector3(transform.position.x, _target.position.y, _target.position.z));
				return;
			}

			FollowPosition(_target.position);
		}

		private void FollowPosition(Vector3 position) {
			transform.position = position;
		}

	}
}

