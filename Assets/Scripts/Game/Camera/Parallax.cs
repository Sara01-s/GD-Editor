using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class Parallax : MonoBehaviour {

		[SerializeField] private Transform _referenceTarget;
		[SerializeField] private Material _material;
		[SerializeField] private Vector3 _velocityMultiplier;
		[SerializeField] private bool _parallaxEnabled;

		private Vector3 _lastReferenceTargetPosition;
		private Vector3 _referenceVelocity;

		private void Awake() {
			_lastReferenceTargetPosition = transform.position;
		}

		public void EnableParallax() {
			_parallaxEnabled = true;
		}

		public void DisableParallax() {
			_parallaxEnabled = false;
		}

		private void Update() {
			if (_parallaxEnabled) return;
			
			Vector3 parallaxOffset = _referenceTarget == null
				? _velocityMultiplier
				: _velocityMultiplier;

			_material.mainTextureOffset = new Vector3(parallaxOffset.x, 0.0f);
		}

		private Vector3 CalculateReferenceVelocity() {
			_referenceVelocity = _referenceTarget.position - _lastReferenceTargetPosition / Time.deltaTime;
			_lastReferenceTargetPosition = _referenceTarget.position;

			var rightVelocity = dot(Vector3.right, _referenceVelocity);
			var upVelocity = dot(Vector3.up, _referenceVelocity);

			return new Vector3(rightVelocity, upVelocity);
		}

	}
}
