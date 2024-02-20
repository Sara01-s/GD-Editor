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
		}

		private void OnEnable() {
			_player.OnDeath += StopPlayer;
		}

		private void OnDisable() {
			_player.OnDeath -= StopPlayer;
		}

		private void FixedUpdate() {
			if (_player.IsDead) return;

			if (_player.CurrentGamemode is IPhysicUpdatable gamemode) {
				gamemode.PhysicsUpdate();
			}

			float clampedYSpeed = max(-24.2f, _playerBody.velocity.y);

			_player.Body.velocity = new Vector2(_playerBody.velocity.x, clampedYSpeed);
			_player.Position = transform.position;

			transform.position += _player.Speed * Time.deltaTime * Vector3.right;
		}

		private void Update() {
			if (_player.IsDead) return;

			if (_player.CurrentGamemode is IUpdatable gamemode) {
				gamemode.Update();
			}
		}

		private void StopPlayer() {
			_player.Body.velocity = Vector2.zero;
			_player.Body.gravityScale = 0.0f;
		}

		private void LateUpdate() {
			if (_player.IsDead) return;

			if (_player.CurrentGamemode is ILateUpdatable gamemode) {
				gamemode.LateUpdate();
			}
		}
		
	}
}
