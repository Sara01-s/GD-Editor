using UnityEngine;

namespace Game {

	internal sealed class Parallax : MonoBehaviour {

		[SerializeField] private PlayerData _playerData;
		[SerializeField] private Material _material;
		[SerializeField, Range(0.0f, 1.0f)] private float _speedMultiplier;

		private void Update() {
			if (_playerData.IsDead) return;

			var parallaxOffset = new Vector3(Time.time * _playerData.Speed * _speedMultiplier, 0.0f);
			_material.mainTextureOffset = parallaxOffset;
		}

	}
}
