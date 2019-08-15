using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageView : MonoBehaviour
{
    #region  UIボタン関係
    [SerializeField]
    private Button nextBtn;

    [SerializeField]
    private Button backBtn;
    #endregion

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject pageViewContentPrefab;

    /// <summary>
    /// １ページあたりの要素数
    /// </summary>
    /// <value></value>
    public int PageContentNum { get; set; } = 3;

    public List<ItemAttribute> ContentList { get; set; }

    private List<Button> CreateButtonList = new List<Button>();
    public int PageCount { get; set; }
    public int NowPageCount { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ContentList = new List<ItemAttribute>()
        {
            new ItemAttribute{
                 Name = "これはテストデータ",
                 ItemType = ItemTypeAttribute.Type.Food
            },
            new ItemAttribute{
                 Name = "ddd",
                 ItemType = ItemTypeAttribute.Type.Gear
            },
            new ItemAttribute{
                 Name = "あなたはホモ",
                 ItemType = ItemTypeAttribute.Type.Wepon
            },
            new ItemAttribute{
                 Name = "レズビアン",
                 ItemType = ItemTypeAttribute.Type.Wepon
            },
            new ItemAttribute{
                 Name = "aaa",
                 ItemType = ItemTypeAttribute.Type.Wepon
            },
            new ItemAttribute{
                 Name = "ccc",
                 ItemType = ItemTypeAttribute.Type.Wepon
            },
        };

        var list = CreateViewButton(PageContentNum);

        int count = 0;
        foreach (var item in list)
        {
            item.ItemInfo = ContentList[count];
            item.OnPress += OnPressEvent;

            count++;
        }


        for (int i = 0; i < ContentList.Count; i++)
        {
            if (i % PageContentNum == 0)
            {
                PageCount++;
            }

        }


        Debug.Log($"ページ数{PageCount}");

        nextBtn.onClick.AddListener(() =>
        {
            NowPageCount++;
            //  内容の初期化
            foreach (var item in list)
            {
                item.Clean();
            }

            int counter = 1 * NowPageCount;
            foreach (var item in list)
            {
                item.ItemInfo = ContentList[counter];
                item.OnPress += OnPressEvent;

                counter++;
            }
        });
    }

    private void OnPressEvent(string st)
    {
        Debug.Log("クリック");
    }

    //  todo : ボタンである必要性はない？GameObjectである意味もない？
    private List<ViewContent> CreateViewButton(int count)
    {
        List<ViewContent> viewContents = new List<ViewContent>();

        for (int i = 0; i < count; i++)
        {
            //  ボタンの配置
            var obj = Instantiate(pageViewContentPrefab, content.transform);
            var rect = obj.GetComponent<RectTransform>();
            var viewCont = obj.GetComponent<ViewContent>();

            rect.gameObject.name = i.ToString();

            //  位置調整
            rect.localPosition = new Vector3(
                content.GetComponent<RectTransform>().sizeDelta.x / count * i,
                0,
                0
                );

            viewContents.Add(viewCont);
        }

        return viewContents;
    }
}
