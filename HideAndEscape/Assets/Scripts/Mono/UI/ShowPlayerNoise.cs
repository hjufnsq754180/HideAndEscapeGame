using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPlayerNoise : MonoBehaviour
{
    private TextMeshProUGUI _noiseText;

    private void Start()
    {
        _noiseText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerNoise.OnPlayerChangedNoise += SetNoiseText;
    }

    private void OnDisable()
    {
        PlayerNoise.OnPlayerChangedNoise -= SetNoiseText;
    }

    public void SetNoiseText(float noise)
    {
        _noiseText.text = $"Ўум: {noise}";
    }
}
