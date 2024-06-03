using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void StartButton()
    {
        UIManager.Ins.setJsPanel(true);
        //Character.instance.setActive(true);
        LevelManager.instance.LVOnInit();
        Close(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Set>();
    }

    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
