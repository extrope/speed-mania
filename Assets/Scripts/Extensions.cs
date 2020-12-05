using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class Extensions {
	public static T GetOnlyComponent<T>(this GameObject gameObject) {
		return AssertX.One(gameObject.GetComponents<T>());
	}
	
	public static GameObject GetParent(this GameObject gameObject) {
		return AssertX.NotNull(gameObject.transform.parent).gameObject;
	}
	
	public static GameObject GetAncestor(this GameObject gameObject, uint levels) {
		while (levels-- > 0)
			gameObject = gameObject.GetParent();
		return gameObject;
	}
	
	public static int GetChildCount(this GameObject gameObject) {
		return gameObject.transform.childCount;
	}
	
	public static IEnumerable<GameObject> GetChildren(this GameObject gameObject) {
		foreach (Transform childTransform in gameObject.transform) {
			yield return childTransform.gameObject;
		}
	}
	
	public static IEnumerable<GameObject> GetChildren(this GameObject gameObject, string name) {
		foreach (var child in gameObject.GetChildren()) {
			if (name == child.name) {
				yield return child;
			}
		}
	}
	
	public static GameObject GetOnlyChild(this GameObject gameObject) {
		return AssertX.One<GameObject>(gameObject.GetChildren());
	}
	
	public static GameObject GetChild(this GameObject gameObject, string name) {
		return AssertX.One<GameObject>(gameObject.GetChildren(name));
	}
	
	public static GameObject GetDescendant(this GameObject gameObject, params string[] path) {
		foreach (var name in path)
			gameObject = gameObject.GetChild(name);
		return gameObject;
	}
}
