using UnityEngine;

namespace Game {

	public static class CameraExtensions {

		public static Bounds GetOrthographicBounds(this Camera camera) {
			float screenAspect = (float)Screen.width / (float)Screen.height;
			float cameraHeight = camera.orthographicSize * 2.0f;

			var bounds = new Bounds (
				camera.transform.position,
				new Vector3(cameraHeight * screenAspect, cameraHeight, 0)
			);

			return bounds;
		}

	}
}