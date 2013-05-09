using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
/// <summary>
/// Component search method for the GetComponents extension.
/// </summary>
public enum ComponentSearch
{
	SELF,
	PARENTS,
	CHILDREN,
	SCENE
}

public static class TransformExtensions
{
	public static T[] GetComponentsInScene<T>(this Transform pSelf) where T :  Component
	{
		return Component.FindObjectsOfType(typeof(T)) as T[];
	}
	public static T GetComponentInScene<T>(this Transform pSelf) where T :  Component
	{
		return Component.FindObjectOfType(typeof(T)) as T;
	}

	/// <summary>
	/// Extends UnityEngine.Transform with a function that collects components from self and parent transforms
	/// </summary>
	/// <returns>an array of components</returns>
	public static T[] GetComponentsInParents<T>(this Transform pSelf) where T : Component
	{
		Transform t = pSelf;
		var result = new List<T>();
		while (t != null)
		{
			result.AddRange(pSelf.GetComponents<T>());
			t = t.parent;
		}
		return result.ToArray();
	}

	/// <summary>
	/// Extends UnityEngine.Transform with a function that searches self and parents for a component
	/// </summary>
	/// <returns>A component or null</returns>
	public static T GetComponentInParents<T>(this Transform pSelf) where T : Component
	{
		Transform t = pSelf;
		while (t != null)
		{
			T result = t.GetComponent<T>();
			if (result != null)
				return result;
			t = t.parent;
		}
		return null;
	}

	/// <summary>
	/// Extends UnityEngine.Transform with a function to get components with a serach option
	/// </summary>
	/// <returns>A component or null</returns>
	/// <param name="pSearchMethod">search method to use</param>
	public static T GetComponent<T>(this Transform pSelf, ComponentSearch pSearchMethod) where T : Component
	{
		switch (pSearchMethod)
		{
			case ComponentSearch.CHILDREN:
				return pSelf.GetComponentInChildren<T>();
			case ComponentSearch.PARENTS:
				return pSelf.GetComponentInParents<T>();
			case ComponentSearch.SELF:
				return pSelf.GetComponent<T>();
			case ComponentSearch.SCENE:
				return pSelf.GetComponentInScene<T>();
			default:
				Debug.LogError("Unsupported search method " + pSearchMethod.ToString());
				return null;
		}
	}

	/// <summary>
	/// Extends UnityEngine.Transform with a method to get components with a search option
	/// </summary>
	/// <returns>an array of components, empty array if none</returns>
	/// <param name="pSearchMethod">search method to use</param>
	public static T[] GetComponents<T>(this Transform pSelf, ComponentSearch pSearchMethod) where T : Component
	{
		switch (pSearchMethod)
		{
			case ComponentSearch.CHILDREN:
				return pSelf.GetComponentsInChildren<T>();
			case ComponentSearch.PARENTS:
				return pSelf.GetComponentsInParents<T>();
			case ComponentSearch.SELF:
				return pSelf.GetComponents<T>();
			case ComponentSearch.SCENE:
				return pSelf.GetComponentsInScene<T>();
			default:
				Debug.LogError("Unsupported search method " + pSearchMethod.ToString());
				return null;
		}
	}
	/// <summary>
	/// Extends UnityEngine.Transoform with a functions that returns an array of all direct children ( non recursive )
	/// </summary>
	/// <returns>The children.</returns>
	/// <param name="pSelf">P self.</param>
	public static Transform[] GetChildren( this Transform pSelf ){
		int len = pSelf.childCount;
		var result = new Transform[len];
		for( int i = 0; i < len; i++){
			result[i] = pSelf.GetChild(i);
		}
		return result;
	}

	/// <summary>
	/// Extends UnityEngine.Transform with a function that Finds a transform by name recursively down the hirarchy
	/// </summary>
	/// <returns>Found Transform, returns null if not found</returns>
	public static Transform FindRecursive(this Transform pSelf, string pName)
	{
		foreach(Transform t in pSelf){
			Transform result = FindRecurisveInternal(t, pName);
			if(result != null){
				return result;
			}
		}
		return null;
	}

	/// <summary>
	/// Extends UnityEngine.Transform with a function that Finds a transform by name recursively down the hirarchy
	/// </summary>
	/// <returns>Found Transform, Logs an error on fail</returns>
	public static Transform FindRecursiveSafe(this Transform pSelf, string pName)
	{
		foreach(Transform t in pSelf){
			Transform result = FindRecurisveInternal(t, pName);
			if(result != null){
				return result;
			}
		}
		Debug.LogError("Could not recursively find a transform named " + pName + " inside " + pSelf.FullName());
		return null;
	}
	static Transform FindRecurisveInternal(Transform t, string pSearch){
		if(t.name == pSearch){
			return t;
		}
		foreach(Transform child in t){
			var result = FindRecurisveInternal(child, pSearch);
			if(result != null){
				return result;
			}
		}
		return null;
	}

	/// <summary>
	/// Extends UnityEngine.Transform with a function that Finds all transforms in children matching a predicate
	/// </summary>
	/// <returns>Found Transform, returns an empty array on fail</returns>
	public static Transform[] FindAllRecursive(this Transform pSelf, Predicate<Transform> pMatch )
	{
		List<Transform> result = new List<Transform>();
		foreach(Transform t in pSelf){
			FindRecurisveInternal(t, pMatch, result);
		}
		return result.ToArray();
	}

	static void FindRecurisveInternal(Transform t, Predicate<Transform> pMatch, List<Transform> pList ){
		if(pMatch(t)){
			pList.Add(t);
		}
		foreach(Transform child in t){
			FindRecurisveInternal(child, pMatch, pList);
		}
	}


	public static string FullName( this Transform pSelf ){
		string result = pSelf.name;
		pSelf = pSelf.parent;
		while( pSelf != null){
			result = pSelf.name + "/" + result;
			pSelf = pSelf.parent;
		}
		return result;
	}
	



}
