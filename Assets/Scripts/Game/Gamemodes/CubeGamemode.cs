using UnityEngine;

namespace Game {

	internal sealed class CubeGamemode : Gamemode {

		[SerializeField] private float _jumpForce = 26.6581f;
		[SerializeField] private float _rotationFactor = 452.4152186f;

		private PlayerData _player;

		internal override void Enable(PlayerData playerData) {
			_player = playerData;
			PlayerInput.OnMainInputHeld += Jump;
		}

		internal override void Disable() {
			_player = null;
			PlayerInput.OnMainInputHeld -= Jump;
		}

		public void Jump() {
			if (!_player.IsGrounded) return;

			_player.Body.velocity = Vector2.zero;
			_player.Body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		}

		
	}
}