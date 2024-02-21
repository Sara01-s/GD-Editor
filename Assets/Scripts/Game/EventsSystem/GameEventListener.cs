using UnityEngine.Events;
using UnityEngine;

namespace Game {

	internal sealed class GameEventListener : MonoBehaviour {
		
		[SerializeField] private GameEvent _gameEvent;
		[SerializeField] private UnityEvent _callback;

		private void OnEnable() {
			_gameEvent.RegisterListener(this);
		}

		private void OnDisable() {
			_gameEvent.UnregisterLister(this);
		}

		internal void OnEventRaised() {
			_callback?.Invoke();
		}

	}
}