using UnityEngine;

namespace Game {

	internal enum GamemodeType {
		None = 0, Cube = 1, Ship = 2
	}

	internal abstract class Gamemode : MonoBehaviour {

		[Tooltip("-1 means Freecam")]
		[SerializeField] internal float MaxScreenHeight;

		internal abstract void Enable(PlayerData playerData);
		internal abstract void Disable();

	}

	internal interface IUpdatable {
		void Update();
	}

	internal interface IPhysicUpdatable {
		void UpdatePhysics();
	}

}
