using System.Collections.Generic;
using UnityEngine;

public class ComboBox
{
    private static bool forceToUnShow = false;
    private static int useControlID = -1;
    public bool isShowingComboBox = false;

    private int selectedItemIndex = 0;
    
    public int List(Rect rect, string buttonText, string[] listContent, GUIStyle listStyle)
    {

        GUIContent[] lc = new GUIContent[listContent.Length];
        for (int i = 0; i < listContent.Length; i++)
        {
            lc[i] = new GUIContent(listContent[i]);
        }
        return List(rect, new GUIContent(buttonText), lc, "button", "box", listStyle);
    }

    public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle listStyle)
    {
        return List(rect, new GUIContent(buttonText), listContent, "button", "box", listStyle);
    }

    public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle)
    {
        return List(rect, buttonContent, listContent, "button", "box", listStyle);
    }

    public int List(Rect rect, string buttonText, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
    {
        return List(rect, new GUIContent(buttonText), listContent, buttonStyle, boxStyle, listStyle);
    }

    public int List(Rect rect, GUIContent buttonContent, GUIContent[] listContent,
                                    GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
    {
        if (forceToUnShow)
        {
            forceToUnShow = false;
            isShowingComboBox = false;
        }

        bool done = false;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.mouseUp:
                {
                    if (isShowingComboBox)
                    {
                        done = true;
                    }
                }
                break;
        }

        if (GUI.Button(rect, buttonContent, buttonStyle))
        {
            if (useControlID == -1)
            {
                useControlID = controlID;
                isShowingComboBox = false;
            }

            if (useControlID != controlID)
            {
                forceToUnShow = true;
                useControlID = controlID;
            }
            isShowingComboBox = true;
        }

        if (isShowingComboBox)
        {
            Rect listRect = new Rect(rect.x, rect.y + rect.height,
                      rect.width, listStyle.CalcHeight(listContent[0], 1.0f) * listContent.Length);

            GUI.Box(listRect, "", boxStyle);
            int newSelectedItemIndex = GUI.SelectionGrid(listRect, selectedItemIndex, listContent, 1, listStyle);
            if (newSelectedItemIndex != selectedItemIndex)
                selectedItemIndex = newSelectedItemIndex;
        }

        if (done)
            isShowingComboBox = false;

        return GetSelectedItemIndex();
    }

    public int GetSelectedItemIndex()
    {
        return selectedItemIndex;
    }
}