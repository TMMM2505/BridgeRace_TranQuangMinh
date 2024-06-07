using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public static LevelManager instance;

    [SerializeField] private Map[] maps = new Map[3];

    [SerializeField] private Finish finalGoal;

    [SerializeField] private Material noneMaterial;

    [SerializeField] private Player player;
    [SerializeField] private Bot bot;

    public ColorData colorData;

    private List<Bot> listBot = new List<Bot>();
    private int index = -1;
    private void Awake()
    {
        instance = this;
        index = 0;
    }
    private void Start()
    {
        LVOnInit();
    }
    public void LVOnInit()
    {
        maps[index].SetActiveMap(true);
        maps[index].SetPlatform();
        SetColorForCharacter();
        player.SetActive(false);
    }
    public void SetColorForCharacter()
    {
        player.setColor((ColorEnum)1);
        for (int i = 2; i <= 4; i++)
        {
            Bot ibot = bot;
            ibot = Instantiate(ibot, new Vector3(i * 2, -3f, -10f), Quaternion.identity);
            listBot.Add(ibot);
            ibot.OnInit((ColorEnum)i, maps[index], finalGoal.transform.position);
            bot.SetActive(false);
        }
    }

    public Platform[] DefinePlatform()
    {
        return maps[index].getPlatform();
    }
    private void ClearBot()
    {
        for (int i = 0; i < listBot.Count; i++)
        {
            Destroy(listBot[i].gameObject);
        }
        listBot.Clear();
    }
    public void ResetLV()
    {
        ClearBot();
        maps[index].ClearMap();
        foreach (KeyValuePair<Collider, Stair> subDic in Dictionary.instance.listStair)
        {
            Stair item = subDic.Value;
            item.ChangeColor(noneMaterial);
        }
        player.OnInit();
    }
    public int GetIndexMap()
    {
        return index;
    }
    public void SetIndexMap(int index)
    {
        this.index = index;
    }
    public void TurnOffCurrentMap(int index)
    {
        maps[index].SetActiveMap(false);
    }

    public void SetActiveChar(bool active)
    {
        player.SetActive(active);
        for(int i = 0; i < listBot.Count; i++)
        {
            listBot[i].SetActive(active);
        }
    }
}
