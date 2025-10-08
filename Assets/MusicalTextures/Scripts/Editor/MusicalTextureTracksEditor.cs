using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MusicalTextureTracksScriptableObject))]
public class MusicalTextureTracksEditor : Editor
{
    private MusicalTextureTracksScriptableObject mtTracksScrObj;
    private string[] mtNames = null;

    GUIStyle itemNumber;
    GUIStyle noMusicalTextures;

    GUIContent rectBox;
    GUIStyle[] colorStyles;

    public class DeleteTrackSection
    {
        public int indexTrack;
        public int indexSection;
        public DeleteTrackSection(int iTrack, int iSection)
        {
            indexTrack = iTrack;
            indexSection = iSection;
        }
    }

    public void OnEnable()
    {
        mtTracksScrObj = (MusicalTextureTracksScriptableObject)target;
        rectBox = new GUIContent();

        if (mtTracksScrObj.mtTracks.Count == 0)
        {
            mtTracksScrObj.mtTracks.Add(new MusicalTextureTrack());
        }

        if (mtTracksScrObj.musicalTexturesData == null)
        {
            mtNames = mtTracksScrObj.musicalTexturesData.GetMusicalTexturesNames();
        }
        else
        {
            colorStyles = new GUIStyle[mtTracksScrObj.musicalTexturesData.musicalTextures.Count];

            for (int i = 0; i < colorStyles.Length; i++)
            {
                GUIStyle colorStyle = new GUIStyle(GUIStyle.none);
                //colorStyle.normal.textColor = mtTracksScrObj.musicalTexturesData.musicalTextures[i].color;
                colorStyle.alignment = TextAnchor.MiddleCenter; 
                colorStyle.normal.background = ColorBox(mtTracksScrObj.musicalTexturesData.musicalTextures[i].color, 2, 2);

                colorStyles[i] = colorStyle;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        InitStyles();
        DeleteTrackSection deleteTrackSection = null;

        EditorGUI.BeginChangeCheck();
        mtTracksScrObj.musicalTexturesData = (MusicalTextureScriptableObject)EditorGUILayout.ObjectField("Musical textures data", mtTracksScrObj.musicalTexturesData, typeof(MusicalTextureScriptableObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(mtTracksScrObj);
        }

        GUILayout.Space(20);

        if (mtTracksScrObj.musicalTexturesData == null)
        {
            EditorGUILayout.LabelField("Please set musical textures data", noMusicalTextures);
            return;
        }

        if (mtNames == null)
        {
            mtNames = mtTracksScrObj.musicalTexturesData.GetMusicalTexturesNames();
        }

        int trackCount = 0;
        int deleteTrack = -1;

        for (int i = 0; i < mtTracksScrObj.mtTracks.Count; i++)
        {
            EditorGUI.BeginChangeCheck();

            MusicalTextureTrack mtTrack = mtTracksScrObj.mtTracks[i];

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(i.ToString("00"), itemNumber, GUILayout.Width(25));

            EditorGUILayout.BeginVertical();

            if (!mtTrack.isSilent)
            {
                trackCount++;
                EditorGUILayout.LabelField("Track " + trackCount, GUILayout.Width(80));
            }
            else
            {
                EditorGUILayout.LabelField("-", GUILayout.Width(80));
            }

            mtTrack.comment = EditorGUILayout.TextField(mtTrack.comment);

            EditorGUILayout.BeginHorizontal();
            mtTrack.isSilent = EditorGUILayout.ToggleLeft("Is silent", mtTrack.isSilent, GUILayout.Width(150));

            if (mtTrack.isSilent)
            {
                EditorGUILayout.LabelField("Silence duration", GUILayout.Width(100));
                mtTrack.silenceDuration = EditorGUILayout.FloatField(mtTrack.silenceDuration);
            }
            EditorGUILayout.EndHorizontal();

            if (mtTrack.isSilent)
            {
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20);
                continue;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Sketch", GUILayout.Width(60));
            mtTrack.sketch = (Texture2D)EditorGUILayout.ObjectField(mtTrack.sketch, typeof(Texture2D), false);
            EditorGUILayout.EndHorizontal();

            if (mtTrack.trackSections == null)
            {
                mtTrack.trackSections = new List<MusicalTextureTrackSection>();
            }

            if (mtTrack.trackSections.Count == 0)
            {
                mtTrack.trackSections.Add(new MusicalTextureTrackSection());
            }

            for (int j = 0; j < mtTrack.trackSections.Count; j++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Section " + (j + 1), GUILayout.Width(60));

                mtTrack.trackSections[j].trackSectionType = (TrackSectionType)EditorGUILayout.EnumPopup(mtTrack.trackSections[j].trackSectionType, GUILayout.Width(60));

                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginHorizontal();
                DrawRectangle(mtTrack.trackSections[j].mtMainIndex);
                mtTrack.trackSections[j].mtMainIndex = EditorGUILayout.Popup(mtTrack.trackSections[j].mtMainIndex, mtNames);
                EditorGUILayout.EndHorizontal();

                if (mtTrack.trackSections[j].trackSectionType == TrackSectionType.Dual)
                {
                    EditorGUILayout.BeginHorizontal();
                    DrawRectangle(mtTrack.trackSections[j].mtSecondaryIndex);
                    mtTrack.trackSections[j].mtSecondaryIndex = EditorGUILayout.Popup(mtTrack.trackSections[j].mtSecondaryIndex, mtNames);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                if (j > 0)
                {
                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        deleteTrackSection = new DeleteTrackSection(i, j);
                    }
                }
                else
                {
                    GUILayout.Space(23);
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+", GUILayout.Width(60)))
            {
                mtTrack.trackSections.Add(new MusicalTextureTrackSection());
            }

            GUILayout.Space(20);

            EditorGUILayout.EndVertical();

            if (i > 0)
            {
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    deleteTrack = i;
                }
            }
            else
            {
                GUILayout.Space(20);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(mtTracksScrObj);
            }

            EditorGUILayout.EndHorizontal();
        }

        if (deleteTrack != -1)
        {
            mtTracksScrObj.mtTracks.RemoveAt(deleteTrack);
        }

        if(deleteTrackSection != null)
        {
            mtTracksScrObj.mtTracks[deleteTrackSection.indexTrack].trackSections.RemoveAt(deleteTrackSection.indexSection);
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Add track"))
        {
            mtTracksScrObj.mtTracks.Add(new MusicalTextureTrack());
        }
    }

    void DrawRectangle(int index)
    {
        Rect rect = GUILayoutUtility.GetRect(rectBox, colorStyles[index], GUILayout.Width(20), GUILayout.Height(20));
        GUI.Box(rect, rectBox, colorStyles[index]);
    }

    Texture2D ColorBox(Color color, int width, int height)
    {
        Texture2D colorBox = new Texture2D(width, height, TextureFormat.RGB24, true);
        //colorBox.wrapMode = TextureWrapMode.Clamp;
        colorBox.filterMode = FilterMode.Point;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color fade = color * 0.5f + color * y * (1f / (height * 2)) + color * 0.25f*(-x * (1f / (width * 2)));
                colorBox.SetPixel(x, y, fade);
            }
        }
        colorBox.Apply();

        return colorBox;
    }
    void InitStyles()
    {
        itemNumber = new GUIStyle("Label");
        itemNumber.alignment = TextAnchor.MiddleCenter;
        itemNumber.fontSize = 16;
        itemNumber.fontStyle = FontStyle.Bold;

        noMusicalTextures = new GUIStyle("Label");
        noMusicalTextures.alignment = TextAnchor.MiddleCenter;
        noMusicalTextures.fontStyle = FontStyle.Bold;
        noMusicalTextures.normal.textColor = Color.red;
    }
}
