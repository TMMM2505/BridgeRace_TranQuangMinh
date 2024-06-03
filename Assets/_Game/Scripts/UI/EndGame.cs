using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : UICanvas
{
    public void ReplayButton()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ResetLV();
        UIManager.Ins.OpenUI<MainMenu>();
    }
    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
