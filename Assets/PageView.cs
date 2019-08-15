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
    private Button pageViewContentPrefab;

    /// <summary>
    /// １ページあたりの要素数
    /// </summary>
    /// <value></value>
    public int PageContentNum { get; set; }

    public List<ViewContent> ContentList { get; set; }

    private List<Button> CreateButtonList = new List<Button>();
    public int PageCount { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        ContentList = new List<ViewContent>()
        {
            new ViewContent(),
            new ViewContent(),
            new ViewContent(),
        };
        PageContentNum = 3;

        var list = CreateViewButton(PageContentNum);

        int count = 0;
        foreach (var item in list)
        {
            var data = new ItemAttribute
            {
                Name = count.ToString(),
                ItemType = ItemTypeAttribute.Type.Food
            };

            item.transform.GetComponent<ViewContent>().SetInfoData(data);
            item.transform.GetComponent<ViewContent>().OnPress += OnPressEvent;
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

        });
    }

    private void OnPressEvent(string st)
    {
        Debug.Log("クリック");
    }

    //  todo : ボタンである必要性はない？
    private List<Button> CreateViewButton(int count)
    {
        List<Button> viewButtons = new List<Button>();

        for (int i = 0; i < count; i++)
        {
            //  ボタンの配置
            var rect = Instantiate(pageViewContentPrefab, content.transform).gameObject.GetComponent<RectTransform>();

            rect.gameObject.name = i.ToString();

            //  位置調整S
            rect.localPosition = new Vector3(
                content.GetComponent<RectTransform>().sizeDelta.x / count * i,
                0,
                0
                );

            viewButtons.Add(rect.gameObject.GetComponent<Button>());
        }

        return viewButtons;
    }
}
