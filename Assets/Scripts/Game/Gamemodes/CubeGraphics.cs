using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class CubeGraphics : MonoBehaviour {

		[SerializeField] private PlayerData _player;
		[SerializeField] private float _rotationFactor;

		private Vector3 _spriteRotation;

		public void Configure(PlayerData playerData) {
			_player = playerData;
		}

		public void UpdateGraphics() {

			if (abs(_player.Body.velocity.y) > 0.1f) {
				_player.Icon.HideParticles(detachFromIcon: true);
			}
			else {
				_player.Icon.ShowParticles(attachToIcon: true);
			}


			if (_player.IsGrounded) {
				_spriteRotation = _player.Icon.transform.rotation.eulerAngles;
				_spriteRotation.z = round(_spriteRotation.z / 90.0f) * 90.0f;

				_player.Icon.transform.rotation = Quaternion.Euler(_spriteRotation);
				return;
			}

			// Always rotate if not grounded.
			_player.Icon.transform.Rotate(_rotationFactor * Time.deltaTime * Vector3.back);
		}

	}
}