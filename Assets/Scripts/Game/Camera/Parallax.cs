using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class Parallax : MonoBehaviour {

		[SerializeField] private Transform _referenceTarget;
		[SerializeField] private Material _material;
		[SerializeField, Range(0.0f, 1.0f)] private float _speedMultiplier;
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
				? new Vector3(Time.time, _speedMultiplier, 0.0f)
				: new Vector3(Time.time, CalculateReferenceVelocity().x * _speedMultiplier, 0.0f);

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
