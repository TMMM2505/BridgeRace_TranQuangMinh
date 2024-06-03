using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Platform[] listPlatform = new Platform[2];
    
    public void SetPlatform()
    {
        for(int i = 0; i < listPlatform.Length; i++)
        {
            listPlatform[i].SetupBrick();
        }
    }
    public Platform[] getPlatform() 
    {
        return listPlatform; 
    }

    public void SetActiveMap(bool setActive)
    {
        transform.gameObject.SetActive(setActive);
    }

    public void ClearMap()
    {
        for(int i = 0; i < listPlatform.Length;i++)
        {
            listPlatform[i].ClearPlatform();
        }
    }
}
