using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiceController : MonoBehaviour
{
    public void Selected()
    {
        var defoMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
        var shader = defoMaterial.shader;
        this.gameObject.GetComponent<MeshRenderer>().material = new Material(shader);
        this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 1, 1);

    }
}
