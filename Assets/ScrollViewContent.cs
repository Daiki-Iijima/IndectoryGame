using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewContent : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Image image;

    public CreateObjectAttribute objInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        objInfo = new CreateObjectAttribute();
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = $"{objInfo.Name}:{objInfo.ItemType.ToString()}";
    }

    public void SetInfoData(CreateObjectAttribute setData)
    {
        objInfo = setData;
    }
}
