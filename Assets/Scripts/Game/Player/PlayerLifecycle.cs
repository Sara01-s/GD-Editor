using UnityEngine.SceneManagement;
using UnityEngine;

namespace Game {

	[RequireComponent(typeof(PlayerCollision))]
	internal sealed class PlayerLifecycle : MonoBehaviour {

		[SerializeField] private PlayerData _playerData;
		[SerializeField] private GameObject _vfxDeath;
		
		private PlayerCollision _playerCollision;

		private void Awake() {
			_playerCollision = GetComponent<PlayerCollision>();
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
			print("Player dead");
			_playerData.OnDeath?.Invoke();
			
			var vfx = Instantiate(_vfxDeath, transform);
			float vfxDuration = vfx.GetComponent<ParticleSystem>().main.duration;

			Invoke(nameof(ReloadScene), vfxDuration);
		}

		private void ReloadScene() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void CompleteLevel() {
			print("Level completed");
		}

	}
}
