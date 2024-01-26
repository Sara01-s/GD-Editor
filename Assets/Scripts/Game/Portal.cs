using UnityEngine;

namespace Game {

	[RequireComponent(typeof(Collider2D))]
	internal sealed class Portal : MonoBehaviour, IInteractable {

		[SerializeField] private Gamemode _gamemode;

		public void Interact(object data) {

			var playerData = data as PlayerData;

		}

	}
}
