using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : Singleton<gameManager>
{
    public static gameManager instance;

    private List<UICanvas> listUI = new List<UICanvas>();
    private bool finish = true;

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
        if (LevelManager.instance.Result() == 1 && finish)
        {
            finish = false;
            Victory();
        }
        if (LevelManager.instance.Result() == 0 && finish)
        {
            finish = false;
            Defeat();
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
/*    public void addListUI(UICanvas newUI)
    {
        listUI.Add(newUI);
    }

    public void CloseAllUI()
    {
        for(int i = 0; i < listUI.Count; i++)
        {
            Debug.Log("xoa UI");
            Destroy(listUI[i]);
        }
    }*/
}
