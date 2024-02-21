using System.Collections;
using UnityEngine;

namespace Game {

	internal sealed class CameraShake : MonoBehaviour {

		[SerializeField] private AnimationCurve _shakeCurve;
		[SerializeField] private float _shakeDuration;

		public void Shake() {
			StartCoroutine(_Shake());
		}

		private IEnumerator _Shake() {
			var startPosition = transform.position;
			float elapsedTime = 0.0f;

			while (elapsedTime < _shakeDuration) {

				float shakeStrength = _shakeCurve.Evaluate(elapsedTime / _shakeDuration); // [0.0 -> 1.0]
				transform.position = startPosition + Random.insideUnitSphere * shakeStrength;

				elapsedTime += Time.deltaTime;
				yield return null;
			}

			transform.position = startPosition;
		}

	}
}
