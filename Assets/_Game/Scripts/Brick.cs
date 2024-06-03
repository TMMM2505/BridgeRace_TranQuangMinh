using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Brick : GameUnit
{
    public Collider col;
    public MeshRenderer rd;
    public ColorEnum color;
    public void TurnOff()
    {
        col.enabled = false;
        rd.enabled = false;
    }

    public void ReAppear()
    {
        StartCoroutine(Active(3f));
    }

    IEnumerator Active(float time)
    {
        yield return new WaitForSeconds(time);
        col.enabled = true;
        rd.enabled = true;
    }

    public void ChangeColor(Material material, ColorEnum color)
    {
        rd.material = material;
        this.color = color;
    }

    public void SetPos(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

    public void OnTaken(Transform trans)
    {
        transform.SetParent(trans);
        transform.forward = trans.forward;
    }
}
