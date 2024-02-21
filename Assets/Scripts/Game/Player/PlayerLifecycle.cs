using UnityEngine;

namespace Game {

	[RequireComponent(typeof(PlayerCollision))]
	internal sealed class PlayerLifecycle : MonoBehaviour {

		[SerializeField] private PlayerData _playerData;
		[SerializeField] private GameEvent _onPlayerDeath;
		[SerializeField] private GameEvent _onPlayerRespawn;
		
		private PlayerCollision _playerCollision;
		private Vector3 _spawnPosition;

		private void Awake() {
			_playerCollision = GetComponent<PlayerCollision>();
			_spawnPosition = transform.position;
			_playerData.IsDead = false;
		}

		private void OnEnable() {
			_playerCollision.OnSideCollision += KillPlayer;
			_playerCollision.OnUpCollision += KillPlayer;
			_playerCollision.OnHazardCollision += KillPlayer;
		}

		private void OnDisable() {
			_playerCollision.OnSideCollision -= KillPlayer;
			_playerCollision.OnUpCollision -= KillPlayer;
			_playerCollision.OnHazardCollision -= KillPlayer;
		}

		internal void KillPlayer() {
			_onPlayerDeath.Raise();
			_playerData.IsDead = true;

			Invoke(nameof(RespawnPlayer), time: 1.0f);
		}

		// FIXME - Debug, remember to remove this lol
		private void Update() {
			if (Input.GetKeyDown(KeyCode.R)) {
				RespawnPlayer();
			}
		}

		private void RespawnPlayer() {
			_playerData.Transform.position = _spawnPosition;
			_playerData.IsDead = false;
			_onPlayerRespawn.Raise();
		}

		public void CompleteLevel() {
			print("Level completed");
		}

	}
}
