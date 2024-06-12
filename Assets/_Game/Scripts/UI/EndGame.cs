using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : UICanvas
{
    public void ReplayButton()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ResetLV();
        LevelManager.Ins.LVOnInit();
        Time.timeScale = 1f;
        UIManager.Ins.OpenUI<MainMenu>();
    }
    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void NextLVButton()
    {
        int index = LevelManager.instance.GetIndexMap();
        if(index <= 2)
        {
            UIManager.Ins.CloseAll();
            LevelManager.Ins.ResetLV();

            LevelManager.instance.TurnOffCurrentMap(index);
            LevelManager.instance.SetIndexMap(++index);

            LevelManager.Ins.LVOnInit();
            Time.timeScale = 1f;

            UIManager.Ins.OpenUI<MainMenu>();
        }
    }
    public void SettingButton()
    {
        Close(0);
        UIManager.Ins.OpenUI<Set>();
        UIManager.Ins.SetLastCanvas(this);
    }
}
