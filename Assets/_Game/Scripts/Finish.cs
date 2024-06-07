using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private BoxCollider bCol;
    private int win = 2;
    private void Start()
    {
        bCol = GetComponent<BoxCollider>();
    }
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

    public void ResetCheckWin(int win)
    {
        this.win = win;
    }
    public void ResetGoal()
    {
        bCol.isTrigger = true;
    }
}
