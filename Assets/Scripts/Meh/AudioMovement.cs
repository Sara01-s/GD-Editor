using UnityEngine;

namespace Game {

	internal sealed class AudioMovement : MonoBehaviour {

		[SerializeField] private AudioTime _audioTime;
		[SerializeField] private float _speed;

		private void Update() {

			double speed = _audioTime.SmoothedDspTime * _speed;
			var velocity = new Vector2((float)speed, transform.position.y);

			transform.position = velocity;
		}

	}
}