using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Component to expand the tag system in unity, to make it so each gameObject can have multiple tags
///</summary>

public class CustomTags : MonoBehaviour
{
    public static List<string> availableTags = new List<string>() {
        "Playable",
        "MainPlayer",
        "Enemy"
    };
    public List<string> tags = new List<string>();

    private void Awake() {
        foreach(string tag in tags)
        {
            if(!IsTag(tag))
            {
                Debug.LogError("Tag: " + tag + " isnt in the available, check if the spelling is correct. GameObject: " + gameObject.name);
            }
        }
    }

    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }
    public static bool IsTag(string tag)
    {
        return availableTags.Contains(tag);
    }
}
