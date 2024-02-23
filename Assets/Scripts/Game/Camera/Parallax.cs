using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class Parallax : MonoBehaviour {

		[SerializeField] private Transform _referenceTarget;
		[SerializeField] private Material _material;
		[SerializeField] private Vector3 _velocityMultiplier;
		[SerializeField] private bool _parallaxEnabled;

		public void EnableParallax() {
			_parallaxEnabled = true;
		}

		public void DisableParallax() {
			_parallaxEnabled = false;
		}

		private void Update() {
			if (!_parallaxEnabled) return;
			_material.mainTextureOffset = Vector3.Scale(_referenceTarget.position, _velocityMultiplier);
		}

	}
}
