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

    private List<ColorEnum> listColor = new List<ColorEnum>();
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
        SetListColor();
        maps[index].SetActiveMap(true);
        maps[index].SetPlatform();
        SetColorForCharacter();
        SetActiveChar(false);
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
                iPlayer.OnInit(startPoint[i-1].transform.position, js, GetRandomCL());
                setPlayer = false;
                listCharacter.Add(iPlayer);
            }
            else
            {
                Bot ibot = prefabBot;
                ibot = Instantiate(prefabBot, startPoint[i-1].transform.position, Quaternion.identity);
                ibot.OnInit(GetRandomCL(), maps[index], finalGoal.transform.position);
                ibot.SetActive(false);
                listCharacter.Add(ibot);
            }
        }

    }
    private ColorEnum GetRandomCL()
    {
        int rand = Random.Range(0, listColor.Count);
        ColorEnum selectedCl = listColor[rand];
        listColor.RemoveAt(rand);
        return selectedCl;
    }
    public Platform[] DefinePlatform()
    {
        return maps[index].getPlatform();
    }
    private void SetListColor()
    {
        foreach(ColorEnum item in System.Enum.GetValues(typeof(ColorEnum)))
        {
            if(item != ColorEnum.None)
            {
                listColor.Add(item);
            }
        }
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
