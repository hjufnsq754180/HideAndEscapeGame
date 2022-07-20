using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FinishGame : MonoBehaviour
{
    private GameManager _gameManager;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _gameManager.GameFinished();
        }
    }
}
