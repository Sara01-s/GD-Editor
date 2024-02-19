using System.Collections.Generic;
using UnityEngine;

namespace LSystem {

	internal sealed class Turtle {

		private Vector3 _position;
		private Quaternion _rotation;
		private readonly Stack<(Vector3, Quaternion)> _savedStates = new();

		internal Turtle(Vector3 startPosition) {
			_position = startPosition;
		}

		internal void Move(Vector3 velocity) {
			// Quaternion * Vector3 rota el vector lol
			// https://discussions.unity.com/t/multiply-quaternion-by-vector/31759/4
			var rotatedVelocity = _rotation * velocity;
			Debug.DrawLine(_position, _position + rotatedVelocity, Color.white, duration: 100.0f);
			_position += rotatedVelocity;
		}

		internal void Rotate(float angle) {
			// es como _rotation += rotation;
			var rotation = new Vector3(0.0f, 0.0f, angle);
			_rotation = Quaternion.Euler(_rotation.eulerAngles + rotation);

			// no funcionó xd
			//_rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}

		internal void SaveState() {
			_savedStates.Push((_position, _rotation));
		}

		internal void LoadState() {
			var loadedState = _savedStates.Pop();

			_position = loadedState.Item1;
			_rotation = loadedState.Item2;
		}

	}
}
