using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : Singleton<gameManager>
{
    [SerializeField] private Finish finalGoal;
    public static gameManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UIManager.Ins.setJsPanel(false);
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void Defeat()
    {
        UIManager.Ins.OpenUI<EndGame>();
        UIManager.Ins.OpenUI<Defeat>();
        UIManager.Ins.setJsPanel(false);
        //LevelManager.Ins.SetActiveChar(false);
        Time.timeScale = 0f;
    }
    public void Victory()
    {
        UIManager.Ins.OpenUI<EndGame>();
        UIManager.Ins.OpenUI<Victory>();
        UIManager.Ins.setJsPanel(false);
        //LevelManager.Ins.SetActiveChar(false);
        Time.timeScale = 0f;
    }
}
