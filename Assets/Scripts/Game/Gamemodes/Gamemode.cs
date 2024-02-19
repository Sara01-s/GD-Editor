using UnityEngine;

namespace Game {

	internal enum GamemodeType {
		None = 0, Cube = 1, Ship = 2
	}

	internal abstract class Gamemode : MonoBehaviour {

		[Tooltip("-1 means Freecam")]
		[SerializeField] internal float MaxScreenHeight;
		[SerializeField] private bool _showScreenHeightGizmo;

		internal abstract void Enable(PlayerData playerData);
		internal abstract void Disable();

		
		private void OnDrawGizmos() {
			if (_showScreenHeightGizmo && MaxScreenHeight > 0) {
				var lineHeight = new Vector3(0.0f, MaxScreenHeight / 2.0f);

				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(transform.position + lineHeight, transform.position + -lineHeight);
			}
		}
	}

	internal interface IUpdatable {
		void Update();
	}

	internal interface ILateUpdatable {
		void LateUpdate();
	}

	internal interface IPhysicUpdatable {
		void PhysicsUpdate();
	}

}
