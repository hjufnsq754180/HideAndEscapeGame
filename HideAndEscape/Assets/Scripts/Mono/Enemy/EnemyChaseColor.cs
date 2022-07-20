using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseColor : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;

    private void OnEnable()
    {
        EnemyController.OnChangedColor += ChangeColor;
    }

    private void OnDisable()
    {
        EnemyController.OnChangedColor -= ChangeColor;
    }

    private void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void ChangeColor()
    {
        Material material = meshRenderer.material;
        material.color = Color.red;
    }
}
