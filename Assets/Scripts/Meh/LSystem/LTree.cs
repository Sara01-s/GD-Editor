using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LSystem {

	internal sealed class LTree : MonoBehaviour {

		[SerializeField] private uint _iterations = 2;

		private readonly string _axiom = "F";
		private readonly string _productionRules = "FF+[+F-F-F]-[+F-F-F]";

		private readonly Dictionary<string, Action<Turtle>> _commands = new() {
			{"F", turtle => turtle.Move(Vector3.up)},
			{"+", turtle => turtle.Rotate(Random.Range(10.0f, 25.0f))},
			{"-", turtle => turtle.Rotate(-Random.Range(10.0f, 25.0f))},
			{"[", turtle => turtle.SaveState()},
			{"]", turtle => turtle.LoadState()},
		};

		private void Awake() {
			var lSystem = new LSystem(_axiom, _productionRules, transform.position, _commands);

			Debug.Log(_axiom);

			for (int i = 0; i < _iterations; i++) {
				Debug.Log(lSystem.GenerateSymbols());
			}

			lSystem.Draw();
		}
		
	}
}
