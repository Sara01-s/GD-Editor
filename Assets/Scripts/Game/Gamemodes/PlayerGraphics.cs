using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class CubeGraphics : MonoBehaviour {

		[SerializeField] private ParticleSystem _groundParticles;

		private PlayerData _player;
		private Vector3 _spriteRotation;

		private void Update() {
			
		}

		private void CubeFeedback() {
			if (_player.IsGrounded) {
				_spriteRotation = transform.rotation.eulerAngles;
				_spriteRotation.z = round(_spriteRotation.z / 90.0f) * 90.0f;

				transform.rotation = Quaternion.Euler(_spriteRotation);

				if (!_groundParticles.isPlaying) {
					_groundParticles.Play();
				}
				
				return;
			}

			// Rotate cube when not grounded
			//transform.Rotate(_data.CubeConfig.RotationFactor * Time.deltaTime * Vector3.back);

			if (_groundParticles.isPlaying) {
				_groundParticles.Clear();
				_groundParticles.Stop();
			}
		}

	}
}