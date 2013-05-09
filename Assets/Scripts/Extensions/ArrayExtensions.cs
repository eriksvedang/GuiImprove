using System.Collections.Generic;
using System;

public static class ArrayExtensions
{	

	/// <summary>
	/// Extends Arrays(T[]) with a ForEach function, very much like List<T> ForEach
	/// Runs an Action<T> on every element in an array
	/// </summary>
	public static void ForEach<T>(this T[] pSelf, Action<T> pAction)
	{
		for (int i = 0; i < pSelf.Length; i++)
		{
			pAction(pSelf[i]);
		}
	}

	/// <summary>
	/// Extends Arrays(T[]) with shorthand for List<T>.FindAll()
	/// </summary>
	public static T[] FindAll<T>(this T[] pSelf, Predicate<T> pMatch)
	{
		var result = new List<T>(pSelf);
		return result.FindAll(pMatch).ToArray();
	}

	/// <summary>
	/// Extends Arrays(T[]) with shorthand for List<T>.Find()
	/// </summary>
	public static T Find<T>(this T[] pSelf, Predicate<T> pMatch)
	{
		var result = new List<T>(pSelf);
		return result.Find(pMatch);
	}

	/// <summary>
	/// Extends Array(T[] with a shorthand for List<T>.Sort())
	/// does not change the original array
	/// </summary>
	/// <returns>
	/// a new sorted list, shallow copy
	/// </returns>
	public static T[] Sort<T>(this T[] pSelf, Comparison<T> pComparer = null)
	{
		var l = new List<T>(pSelf);
		if (pComparer != null)
		{
			l.Sort(pComparer);
		} else
		{
			l.Sort();
		}
		return l.ToArray();
	}

	/// <summary>
	/// Extends (T[]) with IndexOf returns index of first match or -1 if no match found
	/// </summary>
	public static int IndexOf<T>( this T[] pSelf, Predicate<T> pMatch ){
		for( int i = 0; i < pSelf.Length; i++){
			if(pMatch(pSelf[i])){
				return i;
			}
		}
		return -1;
	}

	/// <summary>
	/// Extends (T[]) with a map function that replaces each element in an array
	/// </summary>
	public static void Map<T>( this T[] pSelf, Func<T,T> pFunction ){
		for( int i= 0; i < pSelf.Length; i++){
			pSelf[i] = pFunction(pSelf[i]);
		}
	}

	/// <summary>
	/// Extends (T[]) with a Linq like select function
	/// </summary>
	public static ResultType[] Select<T, ResultType>( this T[] pSelf, Func<T, ResultType> pConverter){
		ResultType[] result = new ResultType[pSelf.Length];
		for( int i = 0; i < pSelf.Length; i++){
			result[i] = pConverter(pSelf[i]);
		}
		return result;
	}

	/// <summary>
	/// Create a new array with elements sorted in reversed order
	/// </summary>
	public static T[] Reversed<T>(this T[] pSelf){
		var v = new List<T>(pSelf);
		v.Reverse();
		return v.ToArray();
	}
}

