using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Finding Frost/Levels")]
public class Levels : ScriptableObject
{
    [TextArea]
    public string levelPoem;
}

