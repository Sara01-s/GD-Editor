using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	[SelectionBase, RequireComponent(typeof(Rigidbody2D))]
	internal sealed class PlayerMovement : MonoBehaviour {

		[SerializeField] private PlayerData _player;
		[Space(20.0f)] private Rigidbody2D _playerBody;

		[SerializeField] private Gamemodes _initialGamemode;

		private void Awake() {
			_playerBody = GetComponent<Rigidbody2D>();
			_player.Body = _playerBody;
			_player.Transform = transform;

			_player.SwitchGamemode(_initialGamemode, Gamemodes.None);
		}

		private void FixedUpdate() {
			if (_player.CurrentGamemode is IPhysicUpdatable gamemode) {
				gamemode.UpdatePhysics();
			}
		}

		private void Update() {
			float clampedYSpeed = max(-24.2f, _playerBody.velocity.y);

			_playerBody.velocity = new Vector2(_playerBody.velocity.x, clampedYSpeed);
			_player.Position = transform.position;

			transform.position += _player.SpeedValue * Time.deltaTime * Vector3.right;

			if (_player.CurrentGamemode is IUpdatable gamemode) {
				gamemode.Update();
			}
		}
		
	}
}
