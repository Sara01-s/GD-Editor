using UnityEngine;
using System;

namespace Game {

	internal sealed class PlayerCollision : MonoBehaviour {

		internal event Action OnSideCollision;
		internal event Action OnUpCollision;
		internal event Action OnHazardCollision;
		internal event Action OnLand;

		[SerializeField] private PlayerData _playerData;

		[Space(20.0f)]
		[SerializeField] private Vector2 _groundCheckPosition;
		[SerializeField] private Vector2 _groundCheckBoxScale;
		[SerializeField] private LayerMask _groundLayer;
		[Space(20.0f)]
		[SerializeField] private Vector2 _sideCheckPosition;
		[SerializeField] private Vector2 _sideCheckBoxScale;
		[Space(20.0f)]
		[SerializeField] private Vector2 _upCheckPosition;
		[SerializeField] private Vector2 _upCheckBoxScale;
		[Space(20.0f)]
		[SerializeField] private LayerMask _hazardLayer;

		private bool _checkCollisions = true;
		private bool _wasGroundedLastFrame;

		private void Update() {
			if (!_checkCollisions) return;

			_playerData.IsGrounded = IsGrounded();

			CheckIfLanded();
			CheckSideCollision();
			CheckUpCollision();
		}

		public void EnableCollisions() {
			_checkCollisions = true;
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.TryGetComponent<IInteractable>(out var interactable)) {
				interactable.Interact(_playerData);
			}

			if ((_hazardLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer) {
				OnHazardCollision?.Invoke();
			}
		}

		private bool IsGrounded() {
			var groundBox = Vector2.right * _groundCheckBoxScale.x + Vector2.up * _groundCheckBoxScale.y;
			return Physics2D.OverlapBox(_playerData.Position + _groundCheckPosition, groundBox, 0.0f, _groundLayer);
		}

		private void CheckIfLanded() {
			if (_playerData.IsGrounded != _wasGroundedLastFrame) {
				if (_playerData.IsGrounded) {
					OnLand?.Invoke();
				}
			}

			_wasGroundedLastFrame = _playerData.IsGrounded;
		}

		private void CheckSideCollision() {
			var sideBox = Vector2.right * _sideCheckBoxScale.x + Vector2.up * _sideCheckBoxScale.y;
			if (Physics2D.OverlapBox((Vector2) transform.position + _sideCheckPosition, sideBox, 0.0f, _groundLayer)) {
				OnSideCollision?.Invoke();
				_checkCollisions = false;
			}
		}

		private void CheckUpCollision() {
			var upBox = Vector2.right * _upCheckPosition.x + Vector2.up * _upCheckPosition.y;
			if (Physics2D.OverlapBox((Vector2) transform.position + _upCheckPosition, upBox, 0.0f, _groundLayer)) {
				OnUpCollision?.Invoke();
				_checkCollisions = false;
			}
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = Color.green;
			var groundBox = Vector2.right * _groundCheckBoxScale.x + Vector2.up * _groundCheckBoxScale.y;
			Gizmos.DrawWireCube(transform.position + (Vector3) _groundCheckPosition, groundBox);

			Gizmos.color = Color.red;
			var sideBox = Vector2.right * _sideCheckBoxScale.x + Vector2.up * _sideCheckBoxScale.y;
			Gizmos.DrawWireCube(transform.position + (Vector3) _sideCheckPosition, sideBox);

			Gizmos.color = Color.red;
			var upBox = Vector2.right * _upCheckPosition.x + Vector2.up * _upCheckPosition.y;
			Gizmos.DrawWireCube(transform.position + (Vector3) _upCheckPosition, upBox);
		}

	}
}