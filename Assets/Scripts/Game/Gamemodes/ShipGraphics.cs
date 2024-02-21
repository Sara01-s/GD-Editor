using UnityEngine;

namespace Game {

	internal sealed class ShipGraphics : MonoBehaviour, IGamemodeGraphics {

		[SerializeField] private PlayerData _player;
		[SerializeField] private float _rotationFactor;

		public void Configure(PlayerData playerData) {
			_player = playerData;
		}

		public void UpdateGraphics() {
			var shipRotation = Quaternion.AngleAxis(_player.Body.velocity.y * _rotationFactor, Vector3.forward);
			_player.Icon.transform.rotation = shipRotation;
		}

	}
}