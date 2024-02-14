using UnityEngine;
using System;

namespace Game {

	internal sealed class PlayerGamemode : MonoBehaviour {

		internal static event Action<PlayerData> OnGamemodeChanged;
		
		[SerializeField] private PlayerData _playerData;
		[SerializeField] private Gamemode[] _gamemodes;
		[SerializeField] private GamemodeType _initialGamemode;

		private GamemodeType _currentGamemodeType;
		private GamemodeType _previousGamemodeType;

		private void Awake() {
			ChangeGamemode(_initialGamemode);
		}

		internal void ChangeGamemode(GamemodeType newGamemode) {
			
			if (newGamemode == GamemodeType.None) {
				Debug.LogError("Tried to switch to Invalid Gamemode.");
				return;
			}
			
			_previousGamemodeType = _currentGamemodeType;
			_currentGamemodeType = newGamemode;

			_playerData.PreviousGamemode = _playerData.CurrentGamemode;
			_playerData.CurrentGamemode = _gamemodes[(int) _currentGamemodeType - 1]; // -1 because first gamamode is 1 and we want to access array index 0
			_playerData.CurrentGamemode.Enable(_playerData);
			Debug.Log($"{_currentGamemodeType} Enabled.");

			OnGamemodeChanged?.Invoke(_playerData);

			if (_previousGamemodeType != GamemodeType.None) { // at the level start, previousGamemode will be None
				_playerData.PreviousGamemode.Disable();
				Debug.Log($"{_previousGamemodeType} Disabled.");
			}
		}

	}
}