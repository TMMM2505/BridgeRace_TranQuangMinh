using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public static LevelManager instance;
 
    [SerializeField] private Joystick js;
    [SerializeField] private Map[] maps = new Map[3];
    [SerializeField] private GameObject[] startPoint = new GameObject[4];

    [SerializeField] private Finish finalGoal;

    [SerializeField] private Material noneMaterial;

    [SerializeField] private Player prefabPlayer;
    [SerializeField] private Bot prefabBot;

    public ColorData colorData;

    private List<Character> listCharacter = new List<Character>();
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
    }
    public void SetColorForCharacter()
    {
        bool setPlayer = true;
        for (int i = 1; i <= 4; i++)
        {
            if(setPlayer)
            {
                Player iPlayer = prefabPlayer;
                iPlayer = Instantiate(prefabPlayer, startPoint[i-1].transform.position, Quaternion.identity);
                iPlayer.OnInit(startPoint[i-1].transform.position, js, (ColorEnum)i);
                setPlayer = false;
                listCharacter.Add(iPlayer);
            }
            else
            {
                Bot ibot = prefabBot;
                ibot = Instantiate(prefabBot, startPoint[i-1].transform.position, Quaternion.identity);
                ibot.OnInit((ColorEnum)i, maps[index], finalGoal.transform.position);
                ibot.SetActive(false);
                listCharacter.Add(ibot);
            }
        }
    }

    public Platform[] DefinePlatform()
    {
        return maps[index].getPlatform();
    }
    private void ClearCharacter()
    {
        for (int i = 0; i < listCharacter.Count; i++)
        {
            Destroy(listCharacter[i].gameObject);
        }
        listCharacter.Clear();
    }
    public void ResetLV()
    {
        ClearCharacter();
        maps[index].ClearMap();
        foreach (KeyValuePair<Collider, Stair> subDic in CacheDictionary.instance.listStair)
        {
            Stair item = subDic.Value;
            item.ChangeColor(noneMaterial);
        }
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
        prefabPlayer.SetActive(active);
        for(int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].SetActive(active);
        }
    }
}
