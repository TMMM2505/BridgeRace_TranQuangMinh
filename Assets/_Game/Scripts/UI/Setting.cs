using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : UICanvas
{
    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ReplayButton()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ResetLV();
        UIManager.Ins.OpenUI<MainMenu>();
    }
}
