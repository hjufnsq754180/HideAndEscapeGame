using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    [SerializeField]
    private int _maxNoise;
    [SerializeField]
    private int _currentNoise;
    [SerializeField]
    private int _noisePerSecond;
    [SerializeField]
    private int _noiseReducePerHalfSec;

    private bool _isRunning = false;
    [SerializeField]
    private float timerMakeNoise;
    [SerializeField]
    private float timerReduceNoise;

    private PlayerController _controller;
    public static Action OnPlayerMaxNoise;
    public static Action<float> OnPlayerChangedNoise;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_controller.dir.magnitude >= 0.1f)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }

        if (_isRunning)
        {
            timerReduceNoise = 0;
            timerMakeNoise += Time.deltaTime;
            if (timerMakeNoise > 1)
            {
                timerMakeNoise = 0;
                SetNoise();
            }
        }

        if (!_isRunning)
        {
            timerMakeNoise = 0;
            timerReduceNoise += Time.deltaTime;
            if (timerReduceNoise > 0.5f)
            {
                timerReduceNoise = 0;
                ReduceNoise();
            }
        }
    }

    public void SetNoise()
    {
        _currentNoise += _noisePerSecond;
        if (_currentNoise > _maxNoise)
        {
            _currentNoise = _maxNoise;
            OnPlayerMaxNoise?.Invoke();
        }
        OnPlayerChangedNoise?.Invoke(_currentNoise);
    }

    public void ReduceNoise()
    {
        _currentNoise -= _noiseReducePerHalfSec;
        if (_currentNoise < 0)
        {
            _currentNoise = 0;
        }
        OnPlayerChangedNoise?.Invoke(_currentNoise);
    }
}
