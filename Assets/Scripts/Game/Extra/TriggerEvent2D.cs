using UnityEngine.Events;
using UnityEngine;

namespace Game {

	[RequireComponent(typeof(Collider2D))]
	internal sealed class TriggerEvent2D : MonoBehaviour {
		
		[SerializeField] private string _tagToDetect;

		[SerializeField] private UnityEvent<Collider2D> _onTriggerEnter2D;
		[SerializeField] private UnityEvent<Collider2D> _onTriggerStay2D;
		[SerializeField] private UnityEvent<Collider2D> _onTriggerExit2D;

		private void Awake() {
			GetComponent<Collider2D>().isTrigger = true;
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.CompareTag(_tagToDetect)) {
				_onTriggerEnter2D?.Invoke(other);
			}
		}

		private void OnTriggerStay2D(Collider2D other) {
			if (other.CompareTag(_tagToDetect)) {
				_onTriggerStay2D?.Invoke(other);
			}
		}

		private void OnTriggerExit2D(Collider2D other) {
			if (other.CompareTag(_tagToDetect)) {
				_onTriggerExit2D?.Invoke(other);
			}
		}

		
	}
}