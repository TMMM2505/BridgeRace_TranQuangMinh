using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : UICanvas
{
    public void ReplayButton()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ResetLV();
        gameManager.instance.SetCheckFinish(true);
        LevelManager.Ins.LVOnInit();
        UIManager.Ins.OpenUI<MainMenu>();
    }
    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void NextLVButton()
    {
        int index = LevelManager.instance.GetIndexMap();
        if(index < 2)
        {
            UIManager.Ins.CloseAll();
            LevelManager.Ins.ResetLV();

            LevelManager.instance.TurnOffCurrentMap(index);
            LevelManager.instance.SetIndexMap(++index);
            LevelManager.Ins.LVOnInit();

            gameManager.instance.SetCheckFinish(true);

            UIManager.Ins.OpenUI<MainMenu>();
        }
    }
}
