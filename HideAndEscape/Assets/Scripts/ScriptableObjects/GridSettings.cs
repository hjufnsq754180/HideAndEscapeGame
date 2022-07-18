using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Grid Preset", menuName = "Grid/Grid Settings")]
public class GridSettings : ScriptableObject
{
    public int width;
    public int offset;
    public GameObject cellPref;
}
