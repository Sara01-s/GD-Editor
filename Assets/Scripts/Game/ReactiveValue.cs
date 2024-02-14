using System.Collections.Generic;
using System;

namespace Game {

	internal sealed class ReactiveValue<T> : IDisposable {
		
		internal T Value {
			get => _value;
			set {
				_value = value;
				OnValueChanged?.Invoke(_value);
			}
		}

		private event Action<T> OnValueChanged;
		private T _value;
		private readonly List<Action<T>> _callbacks = new();

		internal void Suscribe(Action<T> valueChangedCallback) {
			OnValueChanged += valueChangedCallback;
			_callbacks.Add(valueChangedCallback);
		}

		public void Dispose() {
			foreach (var action in _callbacks) {
				OnValueChanged -= action;
			}

			_callbacks.Clear();
			OnValueChanged = null;
		}

	}
}