using UnityEngine;
using System;

namespace Game {
	
	internal enum CameraMode {
		Free, Confined
	}

	internal sealed class PlayerCameraMode : MonoBehaviour {

		internal static event Action<CameraData> OnCameraModeChanged;
		private CameraMode _currentCameraMode;

		private void OnEnable() {
			PlayerGamemode.OnGamemodeChanged += UpdateCameraMode;
		}

		private void OnDisable() {
			PlayerGamemode.OnGamemodeChanged -= UpdateCameraMode;
		}

				private CameraMode GetGamemodeCameraMode(Gamemode gamemode) {
			if (gamemode.MaxScreenHeight < 0.0f) {
				return CameraMode.Free;
			}

			return CameraMode.Confined;
		}

		private void UpdateCameraMode(PlayerData playerData) {
			
			_currentCameraMode = GetGamemodeCameraMode(playerData.CurrentGamemode);
			transform.position = new Vector3(transform.position.x, playerData.LastPortalY);
			
			var cameraData = new CameraData(_currentCameraMode, playerData.CurrentGamemode.MaxScreenHeight, playerData.LastPortalY);
			OnCameraModeChanged?.Invoke(cameraData);
		}

	}
}
