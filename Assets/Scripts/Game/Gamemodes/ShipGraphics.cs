using UnityEngine;

namespace Game {

	internal sealed class ShipGraphics : MonoBehaviour, IGamemodeGraphics {

		[SerializeField] private PlayerData _player;
		[SerializeField] private float _rotationFactor;
		[SerializeField] private Sprite _shipSprite;
		[SerializeField] private ParticleSystem _shipParticles;

		public void Configure(PlayerData playerData) {
			_player = playerData;
			_player.Particles.Value.transform.SetParent(_player.SpriteTransform);
			_player.Particles.Value = _shipParticles;
			_player.Sprite.Value = _shipSprite;
		}

		public void UpdateGraphics() {
			var shipRotation = Quaternion.AngleAxis(_player.Body.velocity.y * _rotationFactor, Vector3.forward);
			_player.SpriteTransform.rotation = shipRotation;
		}

	}
}