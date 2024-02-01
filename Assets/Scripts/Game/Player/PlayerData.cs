using UnityEngine;
using System;

namespace Game {
	
	internal enum PlayerSpeed {
		None = 0, Slow = 1, Normal = 2, Fast = 3, Faster = 4, Fastest = 5
	}

	internal enum Gamemodes {
		None = 0, Cube = 1, Ship = 2
	}

	[CreateAssetMenu(menuName = "Player/Config")]
	internal sealed class PlayerData : ScriptableObject {
		[SerializeField] private Gamemode[] _gamemodes;

		public ReactiveValue<Sprite> Sprite = new();
		public ReactiveValue<ParticleSystem> Particles = new();

		public float SpeedValue {
			get => _speedValues[(int) SpeedType];
		}

		[Header("Gameplay")]
		public Transform Transform;
		public Gamemode CurrentGamemode;
		public Vector2 Position;
		public PlayerSpeed SpeedType;
		public bool IsGrounded;
		public float MaxYSpeed = -24.2f;
		[HideInInspector] public Rigidbody2D Body;
		
		[Header("Graphics")]
		[HideInInspector] public Transform SpriteTransform;
		public Color Primary;
		public Color Secondary;

		private readonly float[] _speedValues = { 0.0f, 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };
		[NonSerialized] private Gamemodes _currentGamemodeType;
		[NonSerialized] private Gamemodes _previousGamemodeType;

		internal void SwitchGamemode(Gamemodes newGamemode) {
			SwitchGamemode(newGamemode, _previousGamemodeType);
		}

		internal void SwitchGamemode(Gamemodes newGamemode, Gamemodes previousGamemode) {
			
			if (newGamemode == Gamemodes.None) {
				Debug.LogError("Tried to switch to Invalid Gamemode.");
				return;
			}
			
			_previousGamemodeType = _currentGamemodeType;
			_currentGamemodeType = newGamemode;

			CurrentGamemode = _gamemodes[(int) _currentGamemodeType - 1]; // -1 because first gamamode is 1 and we want to access array index 0
			CurrentGamemode.Enable(playerData: this);
			Debug.Log($"{_currentGamemodeType} Enabled.");

			if (_previousGamemodeType != Gamemodes.None) { // The first time, previousGamemode will be None
				CurrentGamemode.Disable();
				Debug.Log($"{_previousGamemodeType} Disabled.");
			}
		}

	}
}