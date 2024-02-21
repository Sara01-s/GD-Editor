using UnityEngine;

namespace Game {

	internal sealed class PlayerGraphics : MonoBehaviour {

		[SerializeField] private PlayerData _player;
		[SerializeField] private Transform _iconPivot;

		private Icon _serializedCubeIcon;

		private void Awake() {
			SpawnIcon();
		}

		private void OnDisable() {
			_player.Icon = _serializedCubeIcon;
		}

		private void SpawnIcon() {
			_serializedCubeIcon = _player.Icon;

			_player.Icon = Instantiate(_player.Icon, _iconPivot);
			_player.Icon.Blink();
			_player.Icon.PlaySpawnEffect();
			_player.Icon.SetColors(_player.PrimaryColor, _player.SecondaryColor, _player.UseGlow, _player.GlowColor);
		}

		public void RespawnIcon() {
			_player.Icon.transform.rotation = Quaternion.identity;
			_player.Icon.Blink();
			_player.Icon.PlaySpawnEffect();
		}

		public void DisableIcon() {
			_player.Icon.HideParticles();
			_player.Icon.PlayDeathEffect();
			_player.Icon.HideIcon();
		}

	}
}