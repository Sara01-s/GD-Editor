using UnityEngine;
using System;

namespace Game {
	
	internal enum PlayerSpeed {
		None = 0, Slow = 1, Normal = 2, Fast = 3, Faster = 4, Fastest = 5
	}

	[CreateAssetMenu(menuName = "Player/Config")]
	internal sealed class PlayerData : ScriptableObject {

		internal Action OnDeath;

		public ReactiveValue<Sprite> Sprite = new();
		public ReactiveValue<ParticleSystem> Particles = new();

		public float SpeedValue {
			get => _speedValues[(int) SpeedType];
		}

		[Header("Gameplay")]
		public Transform Transform;
		public Gamemode CurrentGamemode;
		public Gamemode PreviousGamemode;
		public Vector2 Position;
		public PlayerSpeed SpeedType;
		public bool IsGrounded;
		public float MaxYSpeed = -24.2f;
		public float LastPortalY;
		[HideInInspector] public Rigidbody2D Body;
		
		[Header("Graphics")]
		[HideInInspector] public Transform SpriteTransform;
		public Color Primary;
		public Color Secondary;

		private readonly float[] _speedValues = { 0.0f, 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

	}
}