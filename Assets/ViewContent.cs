using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewContent : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Button button;

    public delegate void onPress(string a);
    public event onPress OnPress;
    public ItemAttribute objInfo;

    // Start is called before the first frame update
    void Start()
    {
        objInfo = new ItemAttribute();

        button.onClick.AddListener(() =>
        {
            OnPress?.Invoke("きた");
        });
    }
    public void SetInfoData(ItemAttribute setData)
    {
        objInfo = setData;
        nameText.text = $"{objInfo.Name}:{objInfo.ItemType.ToString()}";
    }
}
