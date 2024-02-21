using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	[SelectionBase, RequireComponent(typeof(Rigidbody2D))]
	internal sealed class PlayerMovement : MonoBehaviour {

		[SerializeField] private PlayerData _player;
		[Space(20.0f)] private Rigidbody2D _playerBody;

		private void Start() {
			_playerBody = GetComponent<Rigidbody2D>();
			_player.Body = _playerBody;
			_player.Transform = transform;

			ChangePlayerSpeed(_player.SpeedType);
		}

		public void ChangePlayerSpeed(SpeedType newSpeed) {
			_player.SpeedType = newSpeed;
		}

		public void StopMovement() {
			_player.Body.velocity = Vector2.zero;
		}

		private void FixedUpdate() {
			if (_player.IsDead) return;

			if (_player.CurrentGamemode is IPhysicUpdatable gamemode) {
				gamemode.PhysicsUpdate();
			}

			_player.Position = transform.position;

			float clampedYSpeed = max(-24.2f, _playerBody.velocity.y);
			_player.Body.velocity = new Vector2(_playerBody.velocity.x, clampedYSpeed);

			transform.position += _player.Speed * Time.deltaTime * Vector3.right;
		}

		private void Update() {
			if (_player.IsDead) return;

			if (_player.CurrentGamemode is IUpdatable gamemode) {
				gamemode.Update();
			}
		}

		private void LateUpdate() {
			if (_player.IsDead) return;

			if (_player.CurrentGamemode is ILateUpdatable gamemode) {
				gamemode.LateUpdate();
			}
		}
		
	}
}
