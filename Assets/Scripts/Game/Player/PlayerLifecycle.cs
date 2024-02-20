using UnityEngine.SceneManagement;
using UnityEngine;

namespace Game {

	[RequireComponent(typeof(PlayerCollision))]
	internal sealed class PlayerLifecycle : MonoBehaviour {

		[SerializeField] private PlayerData _playerData;
		
		private PlayerCollision _playerCollision;

		private void Awake() {
			_playerCollision = GetComponent<PlayerCollision>();
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
			_playerData.OnDeath?.Invoke();
			_playerData.IsDead = true;

			Invoke(nameof(ReloadScene), time: 1.0f);
		}

		// FIXME - Debug, remember to remove this lol
		private void Update() {
			if (Input.GetKeyDown(KeyCode.R)) {
				ReloadScene();
			}
		}

		private void ReloadScene() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void CompleteLevel() {
			print("Level completed");
		}

	}
}
