using UnityEngine;

namespace Game {

	internal sealed class ShipGamemode : Gamemode, IUpdatable, IPhysicUpdatable {

		[SerializeField] private float _gravityFactor = 2.93f;
		[SerializeField] private float _yVelocityLimit = 9.95f;

		private PlayerData _player;

		internal override void Enable(PlayerData data) {
			_player = data;
		}
		
		internal override void Disable() {
			_player = null;
		}

		public  void Update() {
			print("alo");
		}

		public void UpdatePhysics() {
			float shipYDirection = PlayerInput.IsMainInputHeld ? -1.0f : 1.0f;
			float shipGravityScale = _gravityFactor * shipYDirection;

			_player.Body.gravityScale = shipGravityScale;
			_player.Body.LimitYVelocity(_yVelocityLimit);
		}
		
	}
}