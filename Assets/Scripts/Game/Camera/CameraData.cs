namespace Game {

	internal struct CameraData {
		internal CameraMode CameraMode;
		internal float MaxScreenHeight;
		internal float LastPortalY;

		public CameraData(CameraMode cameraMode, float maxScreenHeight, float lastPortalY) {
			CameraMode = cameraMode;
			MaxScreenHeight = maxScreenHeight;
			LastPortalY = lastPortalY;
		}
		
	}
}