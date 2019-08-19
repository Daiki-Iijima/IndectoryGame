using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PiceController : MonoBehaviour
{
    [SerializeField]
    private Slider timeSlider;

    private bool isStartChange;

    public FieldAttribute.Type FieldType { get; set; }
    private void Start()
    {
        //  最初は非表示に
        timeSlider.gameObject.SetActive(false);
    }
    public void Selected(FieldAttribute.Type type)
    {
        isStartChange = true;
        FieldType = type;
        timeSlider.gameObject.SetActive(isStartChange);

        var defoMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
        var shader = defoMaterial.shader;
        var newMaterial = new Material(shader);
        newMaterial = defoMaterial;
        newMaterial.color = FieldAttribute.FieldColor[(int)type];
        // newMaterial.SetTexture("_MainTex", piceTexture);
        // newMaterial.SetInt("_SmoothnessTextureChannel", 1);

        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }
    private void Update()
    {
        if (timeSlider.value <= 1 && isStartChange)
        {
            timeSlider.value += 0.017f;

        }

        if (timeSlider.value >= 1)
        {
            timeSlider.value = 0;
            isStartChange = false;
            timeSlider.gameObject.SetActive(isStartChange);
        }
    }
}
