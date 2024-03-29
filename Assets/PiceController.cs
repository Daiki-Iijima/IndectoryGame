﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiceController : MonoBehaviour
{
    [SerializeField]
    private Slider timeSlider;

    [SerializeField]
    private Image image;

    private bool isStartChange;

    public FieldAttribute.Type FieldType { get; private set; }

    public PlantTypeAttribute.PlantType PlantType { get; private set; }


    private void Start()
    {
        FieldType = FieldAttribute.Type.Wasteland;
        PlantType = PlantTypeAttribute.PlantType.None;
        //  最初は非表示に
        timeSlider.gameObject.SetActive(false);
        image.enabled = false;
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
        newMaterial.color = FieldAttribute.FieldColor[(int)FieldType];

        this.gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    public void PlantSeed()
    {
        if (PlantType != PlantTypeAttribute.PlantType.None) { return; }

        FieldType = FieldAttribute.Type.Planted;
        image.enabled = true;
        StartCoroutine("SeedTimer");
    }

    private IEnumerator SeedTimer()
    {

        image.sprite = Resources.Load<Sprite>("Images/Plants/SeedImg");
        PlantType = PlantTypeAttribute.PlantType.Seed;
        // ログ出力  
        Debug.Log("植えられました");

        // 10秒待つ  
        yield return new WaitForSeconds(10.0f);

        image.sprite = Resources.Load<Sprite>("Images/Plants/komugiImg");
        PlantType = PlantTypeAttribute.PlantType.Wheat;

        Debug.Log("育ちました");
    }


    public int Harvest()
    {
        if (PlantType == PlantTypeAttribute.PlantType.None ||
            PlantType == PlantTypeAttribute.PlantType.Seed)
        {
            return 0;
        }

        PlantType = PlantTypeAttribute.PlantType.None;
        FieldType = FieldAttribute.Type.Farmland;

        image.enabled = false;
        var count = Random.Range(1, 3);
        return count;
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
