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
                 Name = "aaa",
                 ItemType = ItemTypeAttribute.Type.Food
            },
            new ItemAttribute{
                 Name = "ddd",
                 ItemType = ItemTypeAttribute.Type.Gear
            },
            new ItemAttribute{
                 Name = "ccc",
                 ItemType = ItemTypeAttribute.Type.Wepon
            },
            new ItemAttribute{
                 Name = "ddd",
                 ItemType = ItemTypeAttribute.Type.Wepon
            },
            new ItemAttribute{
                 Name = "eee",
                 ItemType = ItemTypeAttribute.Type.Wepon
            },
            new ItemAttribute{
                 Name = "fff",
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

        #region ボタンイベント設定
        nextBtn.onClick.AddListener(() => ChangePage(list, 3, isNext: true));
        backBtn.onClick.AddListener(() => ChangePage(list, 3, isNext: false));
        #endregion
    }

    private void OnPressEvent(ItemAttribute item)
    {
        Debug.Log("クリック : " + item.Name);
    }

    /// <summary>
    /// ページ切り替え
    /// </summary>
    /// <param name="contentList"></param>
    /// <param name="skipPageCount"></param>
    /// <param name="isNext"></param>
    private void ChangePage(List<ViewContent> contentList, int skipPageCount, bool isNext)
    {
        //  todo : ここで処理ブロック入れてはいけない気がする
        //  みるのは、counter?
        if (PageCount < skipPageCount * NowPageCount)
        { return; }

        if (isNext) NowPageCount++; else NowPageCount--;

        int counter = skipPageCount * NowPageCount;

        //  内容の初期化
        foreach (var item in contentList)
        {
            item.Clean();
        }

        foreach (var item in contentList)
        {
            item.ItemInfo = ContentList[counter];
            item.OnPress += OnPressEvent;

            counter++;
        }
    }

    /// <summary>
    /// ボタンを作成し、ViewのContent下に生成する
    /// </summary>
    /// <param name="count">生成するボタン数</param>
    /// <returns>生成したボタン(ViewContent)のリスト</returns>
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
