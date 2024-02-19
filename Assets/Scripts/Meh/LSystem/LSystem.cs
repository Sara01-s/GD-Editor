using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

namespace LSystem {

	internal sealed class LSystem {

		private readonly Dictionary<string, Action<Turtle>> _symbolToCommand;
		private readonly string _productionRules;
		private readonly Turtle _turtle;

		private string _symbols;

		internal LSystem(string axiom, string productionRules, Vector3 startPosition, 
						 Dictionary<string, Action<Turtle>> symbolToCommand) 
		{
			_symbols = axiom; // axiom = initial state
			_productionRules = productionRules;
			_turtle = new Turtle(startPosition);
			_symbolToCommand = symbolToCommand;
		}

		internal void Draw() {
			foreach (char symbol in _symbols) {
				if (_symbolToCommand.TryGetValue(symbol.ToString(), out var command)) {
					command?.Invoke(_turtle);
				}
			}
		}

		internal string GenerateSymbols() {
			var newSymbols = new StringBuilder();

			foreach (char c in _symbols) {
				if (c == 'F') {
					// F se reemplaza por FF+[+F-F-F]-[-F+F+F] (en este caso)
					newSymbols.Append(_productionRules);
				}
				else {
					newSymbols.Append(c);
				}
			}

			_symbols = newSymbols.ToString();
			return newSymbols.ToString();
		}

	}
}
