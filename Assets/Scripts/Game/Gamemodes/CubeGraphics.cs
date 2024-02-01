using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class CubeGraphics : MonoBehaviour, IGamemodeGraphics {

		[SerializeField] private PlayerData _player;
		[SerializeField] private Sprite _cubeSprite;
		[SerializeField] private ParticleSystem _groundParticles;
		[SerializeField] private float _rotationFactor;

		private Vector3 _spriteRotation;

		public void Configure(PlayerData playerData) {
			_player = playerData;
			_player.Particles.Value.transform.SetParent(_player.Transform);
			_player.Particles.Value = _groundParticles;
			_player.Sprite.Value = _cubeSprite;

		}

		public void UpdateGraphics() {
			if (_player.IsGrounded) {
				_spriteRotation = _player.SpriteTransform.rotation.eulerAngles;
				_spriteRotation.z = round(_spriteRotation.z / 90.0f) * 90.0f;

				_player.SpriteTransform.rotation = Quaternion.Euler(_spriteRotation);

				if (!_groundParticles.isPlaying) {
					_player.Particles.Value.Play();
				}
				
				return;
			}

			_player.SpriteTransform.Rotate(_rotationFactor * Time.deltaTime * Vector3.back);
			
			if (_player.Particles.Value.isPlaying) {
				_player.Particles.Value.Clear();
				_player.Particles.Value.Stop();
			}
		}

	}
}