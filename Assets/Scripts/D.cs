#define UNITY_3D
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
#if UNITY_3D
using UnityEngine;
#endif

public class D
{
    public static bool isNull(object pObject) { return isNull(pObject, ""); }
    public static bool isNull(object pObject, string pMessage)
    {
        if (pObject == null)
        {
#if UNITY_3D
            messages.Add("Reference is null, " + pMessage);
            UnityEngine.Debug.LogError("Reference is null, " + pMessage);
            return true;
#else
            Console.WriteLine(pMessage);
            throw new Exception("Reference is null, " + pMessage);
#endif
            //UnityEngine.Debug.LogError("object of type is null, " + pMessage);
            //Console.WriteLine("object of type is null, " + pMessage);


            /*
            StackTrace st = new StackTrace(1, true);
            StackFrame[] stFrames = st.GetFrames();
            string stackTrace = "";
            foreach (StackFrame sf in stFrames)
            {
                stackTrace += String.Format(" -- Method: {0}\n", sf.GetMethod());
            }
            UnityEngine.Debug.Log(stackTrace);*/
            //return true;
        }
        else
        {
            return false;
        }
    }
    public static void Log(string pMessage)
    {
#if UNITY_3D
        messages.Add(pMessage);
        UnityEngine.Debug.Log(pMessage);
#else
            Console.WriteLine(pMessage);
#endif
    }
    public static void LogError(string pMessage)
    {
#if UNITY_3D
        messages.Add("!" + pMessage);
        UnityEngine.Debug.LogError(pMessage);
#else
            throw new Exception(pMessage);
#endif
    }
	
#if UNITY_3D
    private static Vector2 scrollposition = Vector2.zero;
    private static List<string> messages = new List<string>();
#endif   
	
	
	public static void DrawGUI()
    {
#if UNITY_3D        
        scrollposition = GUI.BeginScrollView(new Rect(Screen.width - 400f, 0f, 400f, 400f), scrollposition, new Rect(0,0,400f,400f));
        GUILayout.BeginVertical();
        int j = 0;
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            GUI.Label(new Rect(0, j++ * 18f, 400f, 22f), messages[i]);
        }
        GUILayout.EndVertical();
        GUI.EndScrollView();
#endif
    }


}