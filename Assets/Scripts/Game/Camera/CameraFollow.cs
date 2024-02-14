using UnityEngine;

namespace Game {

	internal enum CameraMode {
		Free, Static
	}

	internal sealed class CameraFollow : MonoBehaviour {
		
		internal static ReactiveValue<CameraMode> CurrentCameraMode = new();

		[SerializeField] private Transform _targetToFollow;
		[SerializeField] private Vector3 _cameraOffset;

		[Space(20.0f)]
		[SerializeField] private bool _updateOnValidate;

		private void OnEnable() {
			PlayerGamemode.OnGamemodeChanged += UpdateMaxScreenHeight;
		}

		private void OnDisable() {
			PlayerGamemode.OnGamemodeChanged -= UpdateMaxScreenHeight;
		}

		private void LateUpdate() {
			FollowTarget();
		}

		private void UpdateMaxScreenHeight(PlayerData playerData) {
			
			CurrentCameraMode.Value = GetGamemodeCameraMode(playerData.CurrentGamemode);

		}

		private CameraMode GetGamemodeCameraMode(Gamemode gamemode) {
			if (gamemode.MaxScreenHeight <= 0.0f) { // Freecam
				return CameraMode.Free;
			}

			return CameraMode.Static;
		}

		private void FollowTarget() {
			transform.position = _targetToFollow.position + _cameraOffset;
		}

		private void OnValidate() {
			if (!_updateOnValidate) return;

			if (_targetToFollow == null) {
				Debug.LogError("To update camera On Validate, first assign a valid target to follow");
				return;
			}

			FollowTarget();
		}
		
	}
}
