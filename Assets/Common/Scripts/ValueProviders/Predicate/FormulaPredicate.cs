using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Calculates bool value by formula
/// Formula sample: 
/// </summary>
public class FormulaPredicate<T> : AggregatePredicate<T> {
	/// <summary>
	/// Formula to calculate
	/// Sample: 0|-1&2
	/// This means "Zeroth argument OR BOTH (NOT first) and second arguments
	/// More samples: 
	/// 0&1&2
	/// 0
	/// </summary>
	public string formula;

	public Func<T, bool> Calculate(string formula) {
		if (formula.Contains('|')) {
			return x => formula.Split('|').Any(part => Calculate(part)(x));
		}
		if (formula.Contains('&')) {
			return x => formula.Split('&').All(part => Calculate(part)(x));
		}
		if (formula.Contains('-') || formula.Contains('!')) {
			return x => !Calculate(formula.Substring(1))(x);
		}
		return arguments[int.Parse(formula)]?.Value ?? (x => false);
	}

	public override Func<T, bool> CalculateValue {
		get {
			return Calculate(formula);
		}
	}
}
