using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private AiController aiController;
    [SerializeField] private Player player;

    private void FixedUpdate()
    {
        if (aiController.AllEnemiesKilled())
            aiController.StartWave(.5f, 1000);
    }
}
