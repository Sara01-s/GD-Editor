using UnityEngine;
using System;

namespace Game {

	internal sealed class GamemodePortal : MonoBehaviour, IInteractable {

		[SerializeField] private Gamemodes _gamemode;

		internal event Action OnInteraction;

		public void Interact(PlayerData playerData) {
			playerData.SwitchGamemode(_gamemode);
			OnInteraction?.Invoke();
		}
		
	}
}