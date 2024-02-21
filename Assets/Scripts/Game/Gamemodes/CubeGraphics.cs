using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class CubeGraphics : MonoBehaviour, IGamemodeGraphics {

		[SerializeField] private PlayerData _player;
		[SerializeField] private float _rotationFactor;

		private Vector3 _spriteRotation;

		public void Configure(PlayerData playerData) {
			_player = playerData;
		}

		public void UpdateGraphics() {
			if (_player.IsGrounded) {
				_spriteRotation = _player.Icon.transform.rotation.eulerAngles;
				_spriteRotation.z = round(_spriteRotation.z / 90.0f) * 90.0f;

				_player.Icon.transform.rotation = Quaternion.Euler(_spriteRotation);
				_player.Icon.ShowParticles();
				return;
			}

			_player.Icon.transform.Rotate(_rotationFactor * Time.deltaTime * Vector3.back);
		}

		internal void HideGroundParticles() {
			_player.Icon.HideParticles();
		}

	}
}