using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class CustomEditorStyles
{
    public static GUIStyle ButtonDelete()
    {
        GUIStyle buttonDelete = new GUIStyle("button");
        buttonDelete.normal.textColor = new Color(0.9f, 0.35f, 0.5f);
        buttonDelete.hover.textColor = new Color(1.0f, 0.7f, 0.9f);

        return buttonDelete;
    }

    public static GUIStyle ButtonNew()
    {
        GUIStyle buttonNew = new GUIStyle("button");
        buttonNew.normal.textColor = new Color(0.75f, 0.9f, 0.55f);
        buttonNew.hover.textColor = new Color(0.9f, 1.0f, 0.7f);

        return buttonNew;
    }

}
