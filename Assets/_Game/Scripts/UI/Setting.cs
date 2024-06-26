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
        LevelManager.Ins.LVOnInit();
        Time.timeScale = 1f;
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void BackButton()
    {
        Close(0);
        UIManager.Ins.LastCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }
}
