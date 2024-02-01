using UnityEngine;

namespace Game {

	internal sealed class NaiveAudioMovement : MonoBehaviour {
		
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private float _playerSpeed;

		private void Update() {
			transform.position = new Vector2(_audioSource.time * _playerSpeed, transform.position.y);
		}

	}
}

