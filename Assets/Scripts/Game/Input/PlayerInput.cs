using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace Game {

	internal sealed class PlayerInput : MonoBehaviour, InputMap.IGameplayActions {

		internal static Action OnMainInputHeld;
		internal static Action OnMainInputPressed;
		internal static Action OnMainInputReleased;
		internal static Action OnEscInputPressed;

		internal static bool IsMainInputHeld;

		private InputMap _inputMap;

		private void Awake() {
			_inputMap = new();
			_inputMap.Gameplay.SetCallbacks(this);

			ChangeActionMap(_inputMap.Gameplay);
		}

		private void OnDisable() {
			_inputMap.Gameplay.RemoveCallbacks(this);
			_inputMap = null;
		}

		private void Update() {
			IsMainInputHeld = _inputMap.Gameplay.MainInput.IsPressed();

			if (IsMainInputHeld) {
				OnMainInputHeld?.Invoke();
			}
		}

		internal static void ChangeActionMap(InputActionMap newActionMap) {
			if (!newActionMap.enabled) {
				newActionMap.Enable();
			}
		}

		public void OnMainInput(InputAction.CallbackContext context) {
			if (context.started) {
				OnMainInputPressed?.Invoke();
			}
			
			if (context.canceled) {
				OnMainInputReleased?.Invoke();
			}
		}

		public void OnEscInput(InputAction.CallbackContext context) {
			if (context.phase == InputActionPhase.Started) {
				OnEscInputPressed?.Invoke();
				print("Escape input pressed!");
			}
		}
	}
}


