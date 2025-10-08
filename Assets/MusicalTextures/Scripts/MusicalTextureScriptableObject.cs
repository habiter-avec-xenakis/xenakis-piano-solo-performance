using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicalTextures", menuName = "Musical Textures", order = 1)]
public class MusicalTextureScriptableObject : ScriptableObject
{
    public List<MusicalTexture> musicalTextures;

    public string[] GetMusicalTexturesNames()
    {
        string[] names = new string[musicalTextures.Count];

        for(int i = 0; i < musicalTextures.Count; i++)
        {
            names[i] = musicalTextures[i].textureName;
        }

        return names;
    }
}
