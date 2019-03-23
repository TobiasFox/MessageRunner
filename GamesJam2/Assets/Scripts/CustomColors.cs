using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DefineCustomColors", order = 1)]
public class CustomColors : ScriptableObject
{
    [Tooltip("order blue, green, red, yellow")]
    public Color[] colors;

    public enum Colors
    {
        BLUE,
        GREEN,
        YELLOW,
        PURPLE
    }

}
