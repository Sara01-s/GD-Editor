using UnityEngine;

namespace Game {

	internal sealed class CubeGamemode : Gamemode, IUpdatable {

		[SerializeField] private float _jumpForce = 26.6581f;

		private IGamemodeGraphics _cubeGraphics;
		private PlayerData _player;

		internal override void Enable(PlayerData playerData) {
			if (!TryGetComponent<IGamemodeGraphics>(out var graphics)) {
				Debug.LogError("Please add graphics to gamemode: " + name);
				return;
			}

			_cubeGraphics = graphics;
			_cubeGraphics.Configure(playerData);
			_player = playerData;

			PlayerInput.OnMainInputHeld += Jump;
		}

		internal override void Disable() {
			_player = null;
			PlayerInput.OnMainInputHeld -= Jump;
		}

		public void Jump() {
			if (!_player.IsGrounded) return;
			print("Jump");
			_player.Body.velocity = Vector2.zero;
			_player.Body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
		}

		public void Update() {
			_cubeGraphics.UpdateGraphics();
		}
	}
}