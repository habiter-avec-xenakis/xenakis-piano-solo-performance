using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;
using TMPro;

public class VolumeOverridesParameters
{
    public string name = "Component";
    public List<VolumeParameter> volumeParameters = new List<VolumeParameter>();
    public List<string> volumeParametersNames = new List<string>();
}

public class VolumeOverridesUIControlsCreator : EditorWindow
{
    // Volume
    public Volume volume;
    // UI
    public Transform uiParent;
    public GameObject prefabUiTitle;
    public GameObject prefabUiSlider;

    private List<VolumeOverridesParameters> volumeOverridesParameters = new List<VolumeOverridesParameters>();
    private Vector2 scrollPos;

    [MenuItem("Window/Volume Overrides UI Creator")]
    static void Init()
    {
        VolumeOverridesUIControlsCreator window = (VolumeOverridesUIControlsCreator)EditorWindow.GetWindow(typeof(VolumeOverridesUIControlsCreator));
        window.Show();
    }
    private void OnGUI()
    {
        volumeOverridesParameters.Clear();

        EditorGUILayout.LabelField("Volume");
        volume = (Volume)EditorGUILayout.ObjectField(volume, typeof(Volume), true);
        if (!volume)
        {
            GUILayout.Label("Please fill the Volume field.");
            return;
        }

        GUILayout.Space(20);

        EditorGUILayout.LabelField("UI");
        uiParent = (Transform)EditorGUILayout.ObjectField("Parent layout", uiParent, typeof(Transform), true);
        prefabUiTitle = (GameObject)EditorGUILayout.ObjectField("Title prefab", prefabUiTitle, typeof(GameObject), false);
        prefabUiSlider = (GameObject)EditorGUILayout.ObjectField("Slider prefab", prefabUiSlider, typeof(GameObject), false);

        GUILayout.Space(20);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        for (int i = 0; i < volume.profile.components.Count; i++)
        {
            var component = volume.profile.components[i];
            var componentName = component.name.Replace("(Clone)", "");
            EditorGUILayout.LabelField("Component " + (i + 1) + ": " + componentName);
            var fields = component.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            var vop = new VolumeOverridesParameters();
            vop.name = componentName;

            for (int j = 0; j < component.parameters.Count; j++)
            {
                var parameter = component.parameters[j];
                var parameterName = UppercaseFirst(fields[j].Name);
                vop.volumeParameters.Add(parameter);
                vop.volumeParametersNames.Add(parameterName);

                EditorGUILayout.LabelField(" - " + "Parameter " + (j + 1) + ": " + parameterName);
            }

            volumeOverridesParameters.Add(vop);
        }

        EditorGUILayout.EndScrollView();

        GUILayout.Space(20);

        if (GUILayout.Button("Create UI Controls"))
        {
            CreateUIControls();
        }
    }

    private void CreateUIControls()
    {
        foreach(var vop in volumeOverridesParameters)
        {
            //var title = Instantiate(prefabUiTitle, uiParent);
            var title = PrefabUtility.InstantiatePrefab(prefabUiTitle, uiParent) as GameObject;
            title.name = vop.name;
            var tmproText = title.GetComponent<TextMeshProUGUI>();
            if(tmproText)
            {
                tmproText.text = vop.name;
            }

            for(int i = 0; i < vop.volumeParameters.Count; i++)
            {
                var parameter = vop.volumeParameters[i];

                if (parameter.GetType() == typeof(ClampedFloatParameter))
                {
                    var cfp = parameter as ClampedFloatParameter;

                    var slider = PrefabUtility.InstantiatePrefab(prefabUiSlider, uiParent) as GameObject;
                    slider.name = "Slider_" + vop.volumeParametersNames[i];
                    var textComponents = slider.GetComponentsInChildren<TextMeshProUGUI>();

                    foreach(var textComponent in textComponents)
                    {
                        if(textComponent.name == "Slider_Text")
                        {
                            textComponent.text = vop.volumeParametersNames[i];
                        }
                    }

                    var sliderComponent = slider.GetComponentInChildren<Slider>();
                    if(slider)
                    {
                        sliderComponent.onValueChanged.AddListener(delegate { parameter.SetValue(new ClampedFloatParameter(sliderComponent.value, 0f, 1f, true)); });
                        //UnityEditor.Events.UnityEventTools.AddPersistentListener(sliderComponent.onValueChanged, delegate { parameter.SetValue(new ClampedFloatParameter(sliderComponent.value, 0f, 1f, true)); });
                    }
                }
                else if (parameter.GetType() == typeof(ColorParameter))
                {

                }
                else
                {
                    // When no prefab related to parameter exists
                }
            }
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}
