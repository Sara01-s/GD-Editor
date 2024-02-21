using UnityEngine;

namespace Game {
	
	internal enum SpeedType {
		None = 0, Slow = 1, Normal = 2, Fast = 3, Faster = 4, Fastest = 5
	}

	[CreateAssetMenu(menuName = "Player/Config")]
	internal sealed class PlayerData : ScriptableObject {

		public float Speed {
			get => _speedValues[(int) SpeedType];
		}

		[Header("Gameplay")]
		public Transform Transform;
		public Gamemode CurrentGamemode;
		public Gamemode PreviousGamemode;
		public Vector2 Position;
		public SpeedType SpeedType;
		public bool IsGrounded;
		public float MaxYSpeed = -24.2f;
		public float LastPortalY;
		public bool IsDead;
		[HideInInspector] public Rigidbody2D Body;
		
		[Header("Graphics")]
		public Icon Icon;
		public Color PrimaryColor;
		public Color SecondaryColor;
		public bool UseGlow;
		public Color GlowColor;

		private readonly float[] _speedValues = { 0.0f, 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

	}
}