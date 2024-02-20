using UnityEngine;

namespace Game {

	internal sealed class PlayerGraphics : MonoBehaviour {

		[SerializeField] private PlayerData _player;
		[SerializeField] private Transform _iconPivot;

		private Icon _serializedCubeIcon;

		private void Awake() {
			SpawnIcon();
		}

		private void OnEnable() {
			_player.OnDeath += DisableGraphics;
		}

		private void OnDisable() {
			_player.OnDeath -= DisableGraphics;
			_player.CubeIcon = _serializedCubeIcon;
		}

		private void SpawnIcon() {
			_serializedCubeIcon = _player.CubeIcon;
			_player.CubeIcon = Instantiate(_player.CubeIcon, _iconPivot);
			_player.CubeIcon.SetColors(_player.PrimaryColor, _player.SecondaryColor, _player.UseGlow, _player.GlowColor);
		}

		private void DisableGraphics() {
			_player.CubeIcon.PlayDeathEffect();
			_player.CubeIcon.HideIcon();
			_player.CubeIcon.HideParticles();
		}

	}
}