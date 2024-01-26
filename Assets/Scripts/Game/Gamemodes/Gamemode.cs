namespace Game {

	internal abstract class Gamemode : UnityEngine.MonoBehaviour {

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
