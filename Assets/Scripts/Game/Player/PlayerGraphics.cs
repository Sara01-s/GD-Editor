using UnityEngine;

namespace Game {

	internal sealed class PlayerGraphics : MonoBehaviour {

		[SerializeField] private PlayerData _player;
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private ParticleSystem _particles;

		private void Awake() {
			_player.SpriteTransform = _spriteRenderer.transform;
			_player.Particles.Value = _particles;
		}

		private void OnEnable() {
			_player.Sprite.Suscribe(UpdateSprite);
			_player.Particles.Suscribe(UpdateParticles);
			_player.OnDeath += DisableGraphics;
		}

		private void OnDisable() {
			_player.Sprite.Dispose();
			_player.Particles.Dispose();
			_player.OnDeath -= DisableGraphics;
		}

		private void DisableGraphics() {
			_spriteRenderer.enabled = false;
			_particles.Clear();
			_particles.Stop();
		}

		private void UpdateSprite(Sprite sprite) {
			_spriteRenderer.sprite = sprite;
			_spriteRenderer.color = _player.Primary;
		}

		private void UpdateParticles(ParticleSystem particles) {
			_particles = particles;
		}

	}
}