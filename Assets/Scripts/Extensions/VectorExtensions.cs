using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class VectorExtensions{
	public static Vector2 XY(this Vector3 pSelf){
		return new Vector2(pSelf.x,pSelf.y);
	}
	public static Vector2 XZ(this Vector3 pSelf){
		return new Vector2(pSelf.x,pSelf.z);
	}
	public static Vector2 YZ(this Vector3 pSelf){
		return new Vector2(pSelf.y,pSelf.z);
	}

	public static Vector3 WriteX(this Vector3 pSelf, float pChange ){
		return new Vector3(pChange, pSelf.y, pSelf.z);
	}
	public static Vector3 WriteY(this Vector3 pSelf, float pChange ){
		return new Vector3(pSelf.x, pChange, pSelf.z);
	}
	public static Vector3 WriteZ(this Vector3 pSelf, float pChange ){
		return new Vector3(pSelf.x, pSelf.y, pChange);
	}

	public static Vector3[] ToVector3Array( this float[] pSelf ){
		List<Vector3> result = new List<Vector3>();
		if(pSelf.Length >= 3 && ((pSelf.Length % 3) == 0)){
			for(int i = 0; i < pSelf.Length; i+=3){
				result.Add(new Vector3(pSelf[i], pSelf[i+1], pSelf[i+2]));
			}
		}else{
			Debug.LogError("Can't create vector 3 array from this");
		}
		return result.ToArray();
	}

	public static Vector2[] ToVector2Array( this float[] pSelf ){
		List<Vector2> result = new List<Vector2>();
		if(pSelf.Length >= 2 && ((pSelf.Length % 2) == 0)){
			for(int i = 0; i < pSelf.Length; i+=2){
				result.Add(new Vector2(pSelf[i], pSelf[i+1]));
			}
		}else{
			Debug.LogError("Can't create vector 2 array from this");
		}
		return result.ToArray();
	}
}
