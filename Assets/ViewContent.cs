using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewContent : MonoBehaviour
{
    [SerializeField]
    private Text displayText;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Button button;

    #region  イベント
    public delegate void onPress(string a);
    public event onPress OnPress;
    #endregion

    private ItemAttribute itemInfo;
    public ItemAttribute ItemInfo
    {
        set
        {
            itemInfo = value;
            if (itemInfo != null)
                displayText.text = $"{ItemInfo.Name}:{ItemInfo.ItemType.ToString()}";
        }
        get
        {
            return itemInfo;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            OnPress?.Invoke("きた");
        });
    }

    public void Clean()
    {
        ItemInfo = null;
        displayText.text = "";
    }

}
