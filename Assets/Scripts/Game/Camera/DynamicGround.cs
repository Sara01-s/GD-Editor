using UnityEngine;

namespace Game {

	internal sealed class DynamicGround : MonoBehaviour {

		[SerializeField] private Transform _topGround;
		[SerializeField] private Transform _botGround;

		private void OnEnable() {
			PlayerCameraMode.OnCameraModeChanged += AdjustDynamicGround;
		}

		private void OnDisable() {
			PlayerCameraMode.OnCameraModeChanged -= AdjustDynamicGround;
		}

		private void AdjustDynamicGround(CameraData cameraData) {
			switch (cameraData.CameraMode) {
				case CameraMode.Free:
					break;
				case CameraMode.Confined:
					var halfScreenHeight = cameraData.MaxScreenHeight * 0.5f;

					_topGround.localPosition = new Vector3(_topGround.position.x, cameraData.LastPortalY + halfScreenHeight);
					_botGround.localPosition = new Vector3(_botGround.position.x, cameraData.LastPortalY - halfScreenHeight);
					break;
				default:
					Debug.LogError("Unsupported camera mode");
					break;
			}
		}

	}
}
