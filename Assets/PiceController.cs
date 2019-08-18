using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PiceController : MonoBehaviour
{
    [SerializeField]
    private Slider timeSlider;

    private void Start()
    {
        //  最初は非表示に
        timeSlider.gameObject.SetActive(false);
    }
    public void Selected(FieldAttribute.Type type)
    {
        var defoMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
        var shader = defoMaterial.shader;
        var newMaterial = new Material(shader);
        newMaterial = defoMaterial;
        newMaterial.color = FieldAttribute.FieldColor[(int)type];
        // newMaterial.SetTexture("_MainTex", piceTexture);
        // newMaterial.SetInt("_SmoothnessTextureChannel", 1);

        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;

    }
}
