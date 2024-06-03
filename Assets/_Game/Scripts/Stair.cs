using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public MeshRenderer rd;

    public void ChangeColor(Material material)
    {
        rd.material = material;
    }

    public Material GetMaterial() { return rd.material; }
}
