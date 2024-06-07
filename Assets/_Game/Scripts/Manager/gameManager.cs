using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : Singleton<gameManager>
{
    [SerializeField] private Finish finalGoal;
    public static gameManager instance;

    private bool showEG = true;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UIManager.Ins.setJsPanel(false);
        UIManager.Ins.OpenUI<MainMenu>();
    }
    private void Update()
    {
        if(showEG)
        {
            int index = LevelManager.instance.GetIndexMap();
            if(index < 2)
            {
                if (finalGoal.Win() == 1)
                {
                    showEG = false;
                    Victory();
                    LevelManager.instance.SetActiveChar(false);
                    finalGoal.ResetGoal();
                }
                if (finalGoal.Win() == 0)
                {
                    showEG = false;
                    Defeat();
                    LevelManager.instance.SetActiveChar(false);
                    finalGoal.ResetGoal();
                }
                finalGoal.ResetCheckWin(2);
                if(index < 2)
                {
                    LevelManager.instance.SetIndexMap(index++);
                }
            }
            else
            {
                UIManager.Ins.OpenUI<TextEG>();
                LevelManager.instance.SetActiveChar(false);
            }
        }
    }

    private void Defeat()
    {
        UIManager.Ins.OpenUI<EndGame>();
        UIManager.Ins.OpenUI<Defeat>();
    }
    private void Victory()
    {
        UIManager.Ins.OpenUI<EndGame>();
        UIManager.Ins.OpenUI<Victory>();
    }

    public void SetCheckFinish(bool check)
    {
        showEG = check;
    }

}
