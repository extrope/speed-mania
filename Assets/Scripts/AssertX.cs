using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class AssertX {
	public static T NotNull<T>(T value) where T : class {
		Assert.IsNotNull(value);
		return value;
	}
	
	public static T One<T>(T[] array) {
		Assert.AreEqual(array.Length, 1);
		return array[0];
	}
	
	public static T One<T>(IEnumerator<T> enumerator) {
		var hasFirst = enumerator.MoveNext();
		Assert.IsTrue(hasFirst);
		var value = enumerator.Current;
		Assert.IsFalse(enumerator.MoveNext());
		return value;
	}
	
	public static T One<T>(IEnumerable<T> enumerable) {
		return One<T>(enumerable.GetEnumerator());
	}
	
	public static T AllEqual<T>(T first, IEnumerator<T> rest) {
		Utils.ForEach(rest, other => Assert.AreEqual(first, other));
		return first;
	}
	
	public static T AllEqual<T>(T first, IEnumerable<T> rest) {
		return AssertX.AllEqual(first, rest.GetEnumerator());
	}
	
	public static T AllEqual<T>(IEnumerator<T> values) {
		(var first, var rest) = Utils.FirstRest(values);
		AssertX.AllEqual(first, rest);
		return first;
	}
	
	public static T AllEqual<T>(IEnumerable<T> values) {
		return AssertX.AllEqual(values.GetEnumerator());
	}
}
