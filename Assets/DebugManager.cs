using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    private Text outputText;

    [SerializeField]
    private GameObject field;

    [SerializeField]
    private ObjectManager manager;

    private List<PiceController> pices = new List<PiceController>();

    private int farmlandCount = 0;
    private int Plowed_FarmlandCount = 0;
    private int plantCount = 0;
    private int RoadCount = 0;
    private int WastelandCount = 0;
    private int SeedCount = 0;
    private int WheatCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        outputText = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pices.Count == 0)
        {
            foreach (Transform obj in field.transform)
            {
                pices.Add(obj.transform.GetComponent<PiceController>());
            }
        }

        farmlandCount = 0;
        Plowed_FarmlandCount = 0;
        RoadCount = 0;
        WastelandCount = 0;
        SeedCount = 0;
        WheatCount = 0;
        plantCount = 0;

        foreach (var item in pices)
        {
            switch (item.FieldType)
            {
                case FieldAttribute.Type.Farmland:
                    {
                        farmlandCount++;
                    }
                    break;
                case FieldAttribute.Type.Plowed_farmland:
                    {
                        Plowed_FarmlandCount++;
                    }
                    break;
                case FieldAttribute.Type.Planted:
                    {
                        plantCount++;
                    }
                    break;
                case FieldAttribute.Type.Road:
                    {
                        RoadCount++;
                    }
                    break;
                case FieldAttribute.Type.Wasteland:
                    {
                        WastelandCount++;
                    }
                    break;

            }

            switch (item.PlantType)
            {
                case PlantTypeAttribute.PlantType.Seed:
                    {
                        SeedCount++;
                    }
                    break;
                case PlantTypeAttribute.PlantType.Wheat:
                    {
                        WheatCount++;
                    }
                    break;

            }
        }


        outputText.text = $@"[==DebugText==]
農地   : {farmlandCount}
耕農地  : { Plowed_FarmlandCount}
タネ土地  : { plantCount}
道     : { RoadCount}
荒地   : { WastelandCount}
==============
タネ : { SeedCount}
小麦 : { WheatCount}
==============
所持小麦 : {manager.GetItemCount}
所持金 : {manager.Money}
";
    }
}
