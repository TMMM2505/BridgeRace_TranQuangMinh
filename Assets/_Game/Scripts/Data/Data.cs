using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class ColorData : MonoBehaviour
{
    public List<Material> colors;
    public Material GetColorData(ColorEnum color)
    {
        return colors[(int)color];
    }
}
public enum ColorEnum
{
    None = 0,
    Red = 1,
    Blue = 2,
    Yellow = 3,
    Green = 4,
}

