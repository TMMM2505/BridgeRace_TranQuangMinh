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
    private int index = 0;
    private int result;
    private void Awake()
    {
        instance = this;
        index = 0;
    }
    private void Start()
    {
        result = 2;
    }
    public void LVOnInit()
    {
        maps[index].SetPlatform();
        maps[index].SetActiveMap(true);
        SetColorForCharacter();
    }
    public void SetColorForCharacter()
    {
        player.setColor((ColorEnum)1);
        player.OnInit();
        for (int i = 2; i <= 2; i++)
        {
            Bot ibot = bot;
            ibot = Instantiate(ibot, new Vector3(i*2, 3f, -10f), Quaternion.identity);
            listBot.Add(ibot);
            ibot.OnInit((ColorEnum)i, maps[index],finalGoal.transform.position);
        }
    }

    public Platform[] DefinePlatform()
    {
        return maps[index].getPlatform();
    }

    public int Result()
    {
        if(finalGoal.Win() == 1)
        {
            result = 1;
        }
        else if (finalGoal.Win() == 0)
        {
            result = 0;
        }
        return result;
    }
    private void ClearBot()
    {
        for(int i = 0; i < listBot.Count; i++)
        {
            Destroy(listBot[i].gameObject);
        }
    }
    public void ResetLV()
    {
        ClearBot();
        maps[index].ClearMap();
        foreach (KeyValuePair<Collider,Stair> subDic in Dictionary.instance.listStair)
        {
            Stair item = subDic.Value;
            item.ChangeColor(noneMaterial);
        }
    }
}
