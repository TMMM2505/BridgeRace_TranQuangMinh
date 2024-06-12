using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlaying : UICanvas
{
    public void PauseButton()
    {
        LevelManager.instance.SetActiveChar(false);
        Time.timeScale = 0f;
        UIManager.Ins.OpenUI<Pause>();
        Close(0);
    }
    public void SettingButton()
    {
        Close(0);
        Time.timeScale = 0f;
        UIManager.Ins.SetLastCanvas(this);
        UIManager.Ins.OpenUI<Set>();
    }
}
