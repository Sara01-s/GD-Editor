using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	[SelectionBase, RequireComponent(typeof(Rigidbody2D))]
	internal sealed class PlayerMovement : MonoBehaviour {

		[SerializeField] private PlayerData _player;
		[Space(20.0f)] private Rigidbody2D _playerBody;

		[SerializeField] private GamemodeType _initialGamemode;

		private bool _movementStopped;

		private void Start() {
			QualitySettings.vSyncCount = 1;
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
			if (_player.CurrentGamemode is IPhysicUpdatable gamemode) {
				gamemode.UpdatePhysics();
			}
		}

		private void Update() {
			if (_movementStopped) return;

			float clampedYSpeed = max(-24.2f, _playerBody.velocity.y);

			_playerBody.velocity = new Vector2(_playerBody.velocity.x, clampedYSpeed);
			_player.Position = transform.position;

			transform.position += _player.SpeedValue * Time.deltaTime * Vector3.right;

			if (_player.CurrentGamemode is IUpdatable gamemode) {
				gamemode.Update();
			}
		}

		private void StopPlayer() {
			_movementStopped = true;
			_player.Body.velocity = Vector2.zero;
			_player.Body.gravityScale = 0.0f;
			_player.Body.isKinematic = true;
		}
		
	}
}
