using UnityEngine;

namespace Game {

	[RequireComponent(typeof(Collider2D))]
	internal sealed class PlayerInteraction : MonoBehaviour {

		[SerializeField] private PlayerData _data;

		private void OnTriggerEnter2D(Collider2D other) {

			if (other.TryGetComponent<IInteractable>(out var interactable)) {
				interactable.Interact(_data);
			}
		}

	}
}
