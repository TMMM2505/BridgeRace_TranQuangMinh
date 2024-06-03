using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    public static Dictionary instance;
    public Dictionary<Collider, Brick> listBrick = new Dictionary<Collider, Brick>();
    public Dictionary<Collider, Stair> listStair = new Dictionary<Collider, Stair>();

    private void Start()
    {
        instance = this;
    }

    public Brick vachamBrick(Collider item)
    {
        if (!listBrick.ContainsKey(item))
        {
            listBrick.Add(item, item.GetComponent<Brick>());
        }

        return listBrick[item];
    }
    public Stair vachamStair(Collider item)
    {
        if (!listStair.ContainsKey(item))
        {
            listStair.Add(item, item.GetComponent<Stair>());
        }

        return listStair[item];
    }
}
