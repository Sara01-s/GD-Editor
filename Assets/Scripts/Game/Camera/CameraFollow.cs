using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class CameraFollow : MonoBehaviour {

		[SerializeField] private Transform _targetToFollow;
		[SerializeField] private Vector3 _cameraOffset;
		[SerializeField] private float _maxCameraY;
		[SerializeField] private float _minCameraY;
		[SerializeField] private float _upDeadzoneThreshold;
		[SerializeField] private float _downDeadzoneThreshold;
		[SerializeField] private float _startFollowThreshold;
		[SerializeField, Min(0.0f)] private float _followYSpeed;

		[Space(20.0f)]
		[SerializeField] private bool _updateOnValidate;
		[SerializeField] private bool _showCameraGuides;

		private Vector3 _transformPuppetPosition;
		private Vector3 _startPosition;
		private bool _canFollow;
		private Camera _camera;

		private void Awake() {
			_camera = GetComponent<Camera>();
			_transformPuppetPosition = transform.position;
			_startPosition = transform.position;
		}

		private void Update() {
			if (_targetToFollow.position.x > transform.position.x + _startFollowThreshold && !_canFollow) {
				_canFollow = true;
			}
		}

		private void FixedUpdate() {
			FollowTarget();
		}

		public void TeleportCameraToTarget() {
			transform.position = _startPosition;
		}
		
		public void EnableFollow() {
			_canFollow = true;
		}
		
		public void DisableFollow() {
			_canFollow = false;
		}

		private void FollowTarget() {
			if (!_canFollow) return;
			// La cámara NO debe seguir al jugador en Y
			
			var finalPosition = _transformPuppetPosition;
			var cameraBounds = _camera.GetOrthographicBounds();

			// Si el jugador supera la altura en Y de un umbral la cámara se ajusta a la altura superior.
			if (_targetToFollow.position.y > finalPosition.y + _upDeadzoneThreshold) {
				finalPosition.y = _targetToFollow.position.y - _upDeadzoneThreshold;
			}

			// Si el jugador baja de la altura en Y de un umbral inferior se ajusta a la altura baja.
			if (_targetToFollow.position.y < finalPosition.y + _downDeadzoneThreshold) {
				finalPosition.y = _targetToFollow.position.y - _downDeadzoneThreshold;
			}

			// Quitamos el camera offset para que no moleste en los siguientes cálculos
			finalPosition -= _cameraOffset;

			// La cámara NUNCA puede superar los límites de altura en Y de _minCameraY y _maxCameraY
			if (finalPosition.y - cameraBounds.extents.y < _minCameraY) {
				finalPosition.y = _minCameraY + cameraBounds.extents.y;
			}

			if (finalPosition.y + cameraBounds.extents.y > _maxCameraY) {
				finalPosition.y = _maxCameraY - cameraBounds.extents.y;
			}

			// La cámara debe seguir al jugador en X SIEMPRE
			finalPosition.x = _targetToFollow.position.x;

			_transformPuppetPosition = finalPosition + _cameraOffset;
			// damping (exponential smoothing)
			transform.position = lerp(transform.position, _transformPuppetPosition, 0.3f);
		}

		private void OnValidate() {
			if (!_updateOnValidate) return;

			if (_targetToFollow == null) {
				Debug.LogError("To update camera On Validate, first assign a valid target to follow");
				return;
			}

			_camera = GetComponent<Camera>();
			FollowTarget();
		}

		private void OnDrawGizmos() {
			if (!_showCameraGuides) return;

			var minYPosition = new Vector3(transform.position.x, _minCameraY);
			var maxYPosition = new Vector3(transform.position.x, _maxCameraY);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, minYPosition);
			Gizmos.DrawCube(minYPosition, Vector2.one * 0.5f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(transform.position, maxYPosition);
			Gizmos.DrawCube(maxYPosition, Vector2.one * 0.5f);

			var upDeadzonePosition = new Vector3(transform.position.x, transform.position.y + _upDeadzoneThreshold);
			var downDeadzonePosition = new Vector3(transform.position.x, transform.position.y + _downDeadzoneThreshold);

			Gizmos.color = Color.magenta;
			Gizmos.DrawCube(upDeadzonePosition, new Vector2(20.0f, 0.1f));
			Gizmos.color = Color.blue;
			Gizmos.DrawCube(downDeadzonePosition, new Vector2(20.0f, 0.1f));

			Gizmos.color = Color.cyan;
			var startFollowThreshold = new Vector3(_startFollowThreshold, transform.position.y);
			Gizmos.DrawCube(transform.position + startFollowThreshold, new Vector3(0.1f, 20.0f));
		}

	}
}
