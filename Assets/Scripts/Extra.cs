using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public static class Extra {
	public static Scene GetActiveScene() {
		return SceneManager.GetActiveScene();
	}
	
	public static void ReloadScene() {
		SceneManager.LoadScene(GetActiveScene().buildIndex);
	}
	
	public static GameObject[] GetRootObjects() {
		return GetActiveScene().GetRootGameObjects();
	}
	
	public static IEnumerable<GameObject> GetRootObjects(string name) {
		foreach (var rootObject in GetRootObjects()) {
			if (rootObject.name == name) {
				yield return rootObject;
			}
		}
	}
	
	public static GameObject GetRootObject(string name) {
		return AssertX.One(
			GetRootObjects(name),
			$"Expected exactly one root game object named '{name}'."
		);
	}
	
	public static GameObject GetObject(string rootName, params string[] namePath) {
		return GetRootObject(rootName).GetDescendant(namePath);
	}
	
	public static T GetOnlyComponent<T>(this GameObject source) {
		var sourceName = source.name;
		var typeName = typeof(T).FullName;
		
		return AssertX.One(
			source.GetComponents<T>(),
			$"Expected exactly one {typeName} component in game object named '{sourceName}'"
		);
	}
	
	public static GameObject GetParent(this GameObject source) {
		var sourceName = source.name;
		
		var parentTransform = AssertX.NotNull(
			source.transform.parent,
			$"Expected a non-null parent of game object named '{sourceName}'"
		);
		
		return parentTransform.gameObject;
	}
	
	public static GameObject TryGetParent(this GameObject source) {
		var parentTransform = source.transform.parent;
		
		if (parentTransform == null) {
			return null;
		} else {
			return parentTransform.gameObject;
		}
	}
	
	public static GameObject GetAncestor(this GameObject source, uint levels) {
		var sourceName = source.name;
		var transform = source.transform;
		
		for (uint i = 0; i < levels; i++) {
			transform = AssertX.NotNull(
				transform.parent,
				$"Expected a non-null ancestor at level {i + 1} of game object named '{sourceName}'"
			);
		}
		
		return transform.gameObject;
	}
	
	public static int GetChildCount(this GameObject source) {
		return source.transform.childCount;
	}
	
	public static IEnumerable<GameObject> GetChildren(this GameObject source) {
		foreach (Transform childTransform in source.transform) {
			yield return childTransform.gameObject;
		}
	}
	
	public static IEnumerable<GameObject> GetChildren(this GameObject source, string name) {
		foreach (var child in source.GetChildren()) {
			if (name == child.name) {
				yield return child;
			}
		}
	}
	
	public static GameObject GetOnlyChild(this GameObject source) {
		var sourceName = source.name;
		
		return AssertX.One<GameObject>(
			source.GetChildren(),
			$"Expected exactly one child in game object named '{sourceName}'."
		);
	}
	
	public static GameObject GetChild(this GameObject source, string name) {
		var sourceName = source.name;
		
		return AssertX.One<GameObject>(
			source.GetChildren(name),
			$"Expected exactly one child named '{name}' in game object named '{sourceName}'."
		);
	}
	
	public static GameObject TryGetChild(this GameObject source, string name) {
		var enumerator = GetChildren(source, name).GetEnumerator();
		if (!enumerator.MoveNext()) return null;
		var value = enumerator.Current;
		if (enumerator.MoveNext()) return null;
		return value;
	}
	
	public static GameObject GetDescendant(this GameObject gameObject, params string[] namePath) {
		foreach (var name in namePath) {
			gameObject = gameObject.GetChild(name);
		}
		
		return gameObject;
	}
	
	public static bool HasParentEqualTo(this GameObject source, GameObject target) {
		var parentTransform = source.transform.parent;
		
		return !(
			parentTransform == null ||
			GameObject.ReferenceEquals(parentTransform.gameObject, target)
		);
	}
}
