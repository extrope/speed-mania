using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class AssertX {
	public static T NotNull<T>(T value, string message) where T : class {
		Assert.IsNotNull<T>(value, message);
		return value;
	}
	
	public static T One<T>(T[] array, string message) {
		Assert.AreEqual(array.Length, 1, message);
		return array[0];
	}
	
	public static T One<T>(IEnumerator<T> enumerator, string message) {
		var hasFirst = enumerator.MoveNext();
		Assert.IsTrue(hasFirst, message);
		var value = enumerator.Current;
		Assert.IsFalse(enumerator.MoveNext(), message);
		return value;
	}
	
	public static T One<T>(IEnumerable<T> enumerable, string message) {
		return One<T>(enumerable.GetEnumerator(), message);
	}
	
	public static T AllEqual<T>(T first, IEnumerator<T> rest, string message) {
		Utils.ForEach(rest, other => Assert.AreEqual(first, other, message));
		return first;
	}
	
	public static T AllEqual<T>(T first, IEnumerable<T> rest, string message) {
		return AssertX.AllEqual(first, rest.GetEnumerator(), message);
	}
	
	public static T AllEqual<T>(IEnumerator<T> values, string message) {
		(var first, var rest) = Utils.FirstRest(values);
		AssertX.AllEqual(first, rest, message);
		return first;
	}
	
	public static T AllEqual<T>(IEnumerable<T> values, string message) {
		return AssertX.AllEqual(values.GetEnumerator(), message);
	}
}
