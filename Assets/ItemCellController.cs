using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCellController : MonoBehaviour
{
    [SerializeField] private ItemDataEnum.ItemData itemData;

    private Text itemTitleText;
    private Button buyButton;

    private void Start()
    {
        //  初期設定
        itemTitleText = this.transform.Find("TitleText").GetComponent<Text>();
        buyButton = this.transform.Find("BuyButton").GetComponent<Button>();

        //  ボタンアクション設定
        buyButton.onClick.AddListener(OnClickBuyButton);

        itemTitleText.text = itemData.ToString();
    }

    #region イベント
    private void OnClickBuyButton()
    {
        if (itemData == ItemDataEnum.ItemData.Wheat_Seed)
            ObjectManager.instance.HaveSeed++;
    }
    #endregion

    public void SetCellData(ItemDataEnum.ItemData setItemData)
    {
        itemData = setItemData;
    }
}
