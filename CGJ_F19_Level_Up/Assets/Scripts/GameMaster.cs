using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private AiController aiController;
	[SerializeField] private UpgradeScreenManager upgradeManager;
	public Player player;
    
    public int PlayerScore { get; set; }

    private UIMaster _uiMaster;

    #region Singleton
    public static GameMaster Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        _uiMaster = GetComponent<UIMaster>();
    }
    #endregion

    private void FixedUpdate()
    {
        if (aiController.AllEnemiesKilled())
            aiController.StartWave(.1f, 1000);
    }

    public void PlayerDead()
    {
        _uiMaster.GameOverUI();
    }
}