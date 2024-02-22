using UnityEngine;

namespace Game {

	internal sealed class FollowTransform : MonoBehaviour {

		[field:SerializeField] internal Transform Target { get; set; }
		[field:SerializeField] internal bool FollowX { get; set; }
		[field:SerializeField] internal bool FollowY { get; set; }
		[field:SerializeField] internal bool FollowEnabled { get; set; }
		[field:SerializeField] internal bool KeepOffset { get; set; }

		private Vector3 _startingOffset;

		private void Awake() {
			if (Target == null && FollowEnabled) {
				Debug.LogError("Please assign a target to follow.");
				return;
			}
			
			_startingOffset = KeepOffset
				? transform.position - Target.position
				: Vector3.zero;
		}

		private void LateUpdate() {
			if (!FollowEnabled) return;

			var newPosition = Target.position + _startingOffset;

			if (!FollowX) {
                newPosition.x = transform.position.x;
            }

            if (!FollowY) {
                newPosition.y = transform.position.y;
            }

			transform.position = newPosition;
		}

		public void EnableFollowing() {
			FollowEnabled = true;
		}

		public void DisableFollowing() {
			FollowEnabled = false;
		}

	}
}
