using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : UICanvas
{
    public void ResumeButton()
    {
        LevelManager.Ins.SetActiveChar(true);
        Close(0);
        Time.timeScale = 1f;
        UIManager.Ins.setJsPanel(true);
    }
    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
