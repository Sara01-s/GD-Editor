using System.Collections;
using UnityEngine;

namespace Game {

	internal sealed class CameraShake : MonoBehaviour {

		[SerializeField] private AnimationCurve _shakeCurve;
		[SerializeField] private PlayerData _playerData;
		[SerializeField] private float _shakeDuration;

		private void OnEnable() {
			_playerData.OnDeath += Shake;
		}

		private void OnDisable() {
			_playerData.OnDeath -= Shake;
		}

		private void Shake() {
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
