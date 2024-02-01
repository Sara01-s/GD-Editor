using UnityEngine;

namespace Game {

	internal sealed class ShipGamemode : Gamemode, IPhysicUpdatable, IUpdatable {

		[SerializeField] private float _gravityFactor = 2.93f;
		[SerializeField] private float _yVelocityLimit = 9.95f;

		private IGamemodeGraphics _shipGraphics;
		private PlayerData _player;

		internal override void Enable(PlayerData playerData) {
			_shipGraphics = GetComponent<IGamemodeGraphics>();
			_shipGraphics.Configure(playerData);
			_player = playerData;
		}
		
		internal override void Disable() {
			_player = null;
		}

		public void UpdatePhysics() {
			if (_player == null) return;
			float shipYDirection = PlayerInput.IsMainInputHeld ? -1.0f : 1.0f;
			float shipGravityScale = _gravityFactor * shipYDirection;

			_player.Body.gravityScale = shipGravityScale;
			_player.Body.LimitYVelocity(_yVelocityLimit);
		}

		public void Update() {
			_shipGraphics.UpdateGraphics();
		}
	}
}