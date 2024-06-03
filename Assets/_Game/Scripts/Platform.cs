using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static Platform instance;
    
    private Brick brick;
    private List<Brick> listBrick = new List<Brick>();
    private void Awake()
    {
        instance = this;
    }
    public void SetupBrick()
    {
        listBrick.Clear();

        Vector3 mapPos = transform.position;

        int red = 0, blue = 0, green = 0, yellow = 0;
        System.Random r = new System.Random();
        for (int i = 16; i >= -14; i -= 2)
            for (int j = 15; j >= -16; j -= 2)
            {
                ColorEnum random = (ColorEnum)Random.Range(1, System.Enum.GetValues(typeof(ColorEnum)).Length);
                if ((int)random == 1)
                {
                    if (red <= 40)
                    {
                        brick = SimplePool.Spawn<Brick>(PoolType.Brick, new Vector3(j, mapPos.y + 2.7f, mapPos.z + i), Quaternion.identity);
                        brick.ChangeColor(LevelManager.instance.colorData.GetColorData(random), random);
                        listBrick.Add(brick);
                        red++;
                    }

                }
                else if ((int)random == 2)
                {
                    if (blue <= 40)
                    {
                        brick = SimplePool.Spawn<Brick>(PoolType.Brick, new Vector3(j, mapPos.y + 2.7f, mapPos.z + i), Quaternion.identity);
                        brick.ChangeColor(LevelManager.instance.colorData.GetColorData(random), random);
                        listBrick.Add(brick);

                        blue++;
                    }
                }
                else if ((int)random == 4)
                {
                    if (yellow <= 40)
                    {
                        brick = SimplePool.Spawn<Brick>(PoolType.Brick, new Vector3(j, mapPos.y + 2.7f, mapPos.z + i), Quaternion.identity);
                        brick.ChangeColor(LevelManager.instance.colorData.GetColorData(random), random);
                        listBrick.Add(brick);

                        yellow++;
                    }
                }
                else if ((int)random == 3)
                {
                    if (green <= 40)
                    {
                        brick = SimplePool.Spawn<Brick>(PoolType.Brick, new Vector3(j, mapPos.y + 2.7f, mapPos.z + i), Quaternion.identity);
                        brick.ChangeColor(LevelManager.instance.colorData.GetColorData(random), random);
                        listBrick.Add(brick);

                        green++;
                    }
                }
            }
        Debug.Log("Red: " + red + ", Blue: " + blue + ", Green: " + green + ", Yellow: " + yellow);
    }
    public void ClearPlatform()
    {
        for(int i = 0; i < listBrick.Count; i++) 
        {
            Destroy(listBrick[i].gameObject);
        }
    }
    public List<Brick> getTarget()
    {
        return listBrick;
    }
}
