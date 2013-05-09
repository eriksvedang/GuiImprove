using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class PysicsExtensions
{
	/// <summary>
	/// Extends UnityEngine.RaycastHit[] with a function to get the closest component
	/// </summary>
	/// <returns>The closest component.</returns>
	/// <param name="pSearchMethod">Method to use on each hit.transform to search for a component</param>
	public static T GetClosestComponent<T>(this RaycastHit[] pRaycastHits, ComponentSearch pSearchMethod) where T : Component
	{
		var hits = pRaycastHits.SortByDistance();
		foreach (var hit in hits)
		{
			T result = hit.transform.GetComponent<T>(pSearchMethod);
			if (result != null)
			{
				return result;
			}
		}
		return null;
	}

	/// <summary>
	/// Extends UnityEngine.RaycastHit[] with a function to get an array with components of type T sorted closest first
	/// </summary>
	/// <returns>an array of T</returns>
	/// <param name="pSearchMethod">Method to use on each hit.transform to search for components</param>
	public static T[] GetComponentsByCloseness<T>(this RaycastHit[] pRaycastHits, ComponentSearch pSearchMethod) where T : Component
	{
		List<T> results = new List<T>();
		var hits = pRaycastHits.SortByDistance();
		foreach (var hit in hits)
		{
			T[] c = hit.transform.GetComponents<T>(pSearchMethod);
			if (c != null)
			{
				results.AddRange(c);
			}
		}
		return results.ToArray();
	}

	/// <summary>
	/// Extends UnityEngine.RaycastHit[] with a method for sorting hits by RaycastHit.distance
	/// </summary>
	/// <returns>a new array sorted by distance to hit, smallest first</returns>
	public static RaycastHit[] SortByDistance(this RaycastHit[] pSelf)
	{
		var hits = new List<RaycastHit>(pSelf);
		hits.Sort((l, r) => l.distance < r.distance ? -1 : 1);
		return hits.ToArray();
	}
}