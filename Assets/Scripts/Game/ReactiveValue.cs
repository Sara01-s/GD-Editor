using System;

namespace Game {

	internal sealed class ReactiveValue<T> {

		private event Action<T> OnValueChanged;

		internal T Value {
			get => _value;
			set {
				_value = value;
				OnValueChanged?.Invoke(_value);
			}
		}

		private T _value;

		internal void Suscribe(Action<T> action) {
			OnValueChanged += action;
		}

		internal void Dispose() {
			OnValueChanged = null;
		}

	}
}