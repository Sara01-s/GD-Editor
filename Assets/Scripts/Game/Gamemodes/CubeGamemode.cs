using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class CubeGamemode : Gamemode, IPhysicUpdatable, IUpdatable {

		[SerializeField, Min(0.0f)] private float _jumpHeight;
		[SerializeField, Min(0.0f)] private float _secondsToJumpPeak;
		[SerializeField, Min(0.0f)] private float _secondsToLand;

		private float _jumpVelocity;
		private float _jumpGravity;
		private float _fallGravity;

		private CubeGraphics _cubeGraphics;
		private PlayerData _player;

		internal override void Enable(PlayerData playerData) {
			if (!TryGetComponent(out _cubeGraphics)) {
				Debug.LogError("Please add graphics to gamemode: " + name);
				return;
			}

			_cubeGraphics.Configure(playerData);
			_player = playerData;

			PlayerInput.OnMainInputHeld += Jump;

			_jumpVelocity =  2.0f * _jumpHeight / _secondsToJumpPeak;
			_jumpGravity  = -2.0f * _jumpHeight / pow(_secondsToJumpPeak, 2.0f);
			_fallGravity  = -2.0f * _jumpHeight / pow(_secondsToLand, 2.0f);
		}

		internal override void Disable() {
			PlayerInput.OnMainInputHeld -= Jump;
		}

		public void Update() {
			_cubeGraphics.UpdateGraphics();
		}

		public void PhysicsUpdate() {
			var gravity = new Vector2(0.0f, GetJumpGravity());
			_player.Body.velocity += gravity * Time.fixedDeltaTime;
		}

		private float GetJumpGravity() {
			return _player.Body.velocity.y > 0.0f ? _jumpGravity : _fallGravity;
		}

		private void Jump() {
			if (!_player.IsGrounded || _player.Body.velocity.y > 0.01f) return;

			var jumpVelocity = new Vector2(0.0f, _jumpVelocity);
			_player.Body.velocity += jumpVelocity;
		}

	}
}