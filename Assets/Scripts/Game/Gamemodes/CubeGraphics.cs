using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class CubeGraphics : MonoBehaviour, IGamemodeGraphics {

		[SerializeField] private PlayerData _player;
		[SerializeField] private Sprite _cubeSprite;
		[SerializeField] private float _rotationFactor;

		private Vector3 _spriteRotation;

		public void Configure(PlayerData playerData) {
			_player = playerData;
			_player.Sprite.Value = _cubeSprite;
		}

		public void UpdateGraphics() {
			if (_player.IsGrounded) {
				_spriteRotation = _player.CubeIcon.transform.rotation.eulerAngles;
				_spriteRotation.z = round(_spriteRotation.z / 90.0f) * 90.0f;

				_player.CubeIcon.transform.rotation = Quaternion.Euler(_spriteRotation);
				_player.CubeIcon.ShowParticles();
				return;
			}

			_player.CubeIcon.transform.Rotate(_rotationFactor * Time.deltaTime * Vector3.back);
			_player.CubeIcon.HideParticles();
		}

	}
}