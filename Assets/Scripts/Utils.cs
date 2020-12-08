using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
	public static T[] ToArray<T>(this IEnumerator<T> enumerator, int length) {
		var array = new T[length];
		for (int i = 0; i < length; i++) {
			enumerator.MoveNext();
			array[i] = enumerator.Current;
		}
		return array;
	}
	
	public static T[] ToArray<T>(this IEnumerable<T> enumerable, int length) {
		return enumerable.GetEnumerator().ToArray(length);
	}
	
	public static IEnumerable<T> Enumerate<T>(this IEnumerator<T> enumerator) {
		while (enumerator.MoveNext()) {
			yield return enumerator.Current;
		}
	}
	
	public static IEnumerable<O> Map<I, O>(this IEnumerable<I> enumerable, Func<I, O> mapper) {
		foreach (var value in enumerable) {
			yield return mapper(value);
		}
	}
	
	public static IEnumerable<O> Map<I, O>(Func<I, O> mapper, params I[] values) {
		return Utils.Map(values, mapper);
	}
	
	public static (T first, IEnumerator<T> rest) FirstRest<T>(this IEnumerator<T> enumerator) {
		enumerator.MoveNext();
		var first = enumerator.Current;
		return (first, enumerator);
	}
	
	public static (T first, IEnumerator<T> rest) FirstRest<T>(this IEnumerable<T> enumerable) {
		return FirstRest(enumerable.GetEnumerator());
	}
	
	public static void ForEach<T>(this IEnumerator<T> enumerator, Action<T> callback) {
		while (enumerator.MoveNext()) {
			callback(enumerator.Current);
		}
	}
	
	public static IEnumerable<T> Cat<T>(IEnumerable<IEnumerable<T>> enumerables) {
		foreach (var enumerable in enumerables) {
			foreach (var value in enumerable) {
				yield return value;
			}
		}
	}
	
	public static IEnumerable<T> Cat<T>(params IEnumerable<T>[] enumerables) {
		return Cat((IEnumerable<IEnumerable<T>>) enumerables);
	}
}
