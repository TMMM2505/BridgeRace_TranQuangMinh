using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private int win = 2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Constants.tagBot))
        {
            win = 0;
        }
        if (other.gameObject.CompareTag(Constants.tagPlayer))
        {
            win = 1;
        }
    }
    public int Win()
    {
        return win;
    }
}
