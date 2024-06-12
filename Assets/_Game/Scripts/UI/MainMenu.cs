using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void StartButton()
    {
        Time.timeScale = 1f;
        LevelManager.instance.SetActiveChar(true);
        UIManager.Ins.setJsPanel(true);
        Close(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Set>();
        UIManager.Ins.SetLastCanvas(this);
        Close(0);
    }

    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
