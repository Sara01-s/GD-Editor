using UnityEngine;
using System;

namespace Game {

	internal sealed class GamemodePortal : MonoBehaviour, IInteractable {

		[SerializeField] private GamemodeType _gamemode;

		internal event Action OnInteraction;

		public void Interact(PlayerData playerData) {
			var playerGamemode = playerData.Transform.GetComponent<PlayerGamemode>();

			playerGamemode.ChangeGamemode(_gamemode);
			playerData.LastPortalY = transform.position.y;

			OnInteraction?.Invoke();
		}
		
	}
}