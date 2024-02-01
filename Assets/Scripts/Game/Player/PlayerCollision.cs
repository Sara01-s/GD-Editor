using UnityEngine;

namespace Game {

	internal sealed class PlayerCollision : MonoBehaviour {

		[SerializeField] private PlayerData _playerData;

		[Space(20.0f)]
		[SerializeField] private Vector2 _groundCheckPosition;
		[SerializeField] private Vector2 _groundCheckScale;
		[SerializeField] private LayerMask _groundLayer;

		private void Update() {
			_playerData.IsGrounded = IsGrounded();
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.TryGetComponent<IInteractable>(out var interactable)) {
				interactable.Interact(_playerData);
			}
		}

		private bool IsGrounded() {
			var boxScale = Vector2.right * _groundCheckScale.x + Vector2.up * _groundCheckScale.y;
			return Physics2D.OverlapBox(_playerData.Position + _groundCheckPosition, boxScale, 0.0f, _groundLayer);
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = Color.green;

			var boxScale = Vector2.right * _groundCheckScale.x + Vector2.up * _groundCheckScale.y;
			Gizmos.DrawWireCube((Vector2)transform.position + _groundCheckPosition, boxScale);
		}

	}
}