using UnityEngine;

namespace Game {

	public static class Vector2Extensions {

		public static void SetX(this Vector2 vector, float value) {
			vector.x = value;
		}

		public static void SetY(this Vector2 vector, float value) {
			vector.y = value;
		}

	}
}