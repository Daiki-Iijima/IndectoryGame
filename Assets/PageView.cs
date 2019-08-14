using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PageView : MonoBehaviour
{
#if UIボタン関係
    [SerializeField]
    private Button nextBtn;

    [SerializeField]
    private Button backBtn;
#endif

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Button pageViewContentPrefab;

    /// <summary>
    /// １ページあたりの要素数
    /// </summary>
    /// <value></value>
    public int PageContentNum { get; set; }

    public List<int> ContentList { get; set; }

    public int PageCount { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        ContentList = new List<int>()
        {
            1,
            5,
            3,
            99,
            6,
        };
        PageContentNum = 3;

        for (int add = 0; add < PageContentNum; add++)
        {
            //  ボタンの配置
            //  todo : メソッド化して使えるはず
            var rect = Instantiate(pageViewContentPrefab, content.transform).gameObject.GetComponent<RectTransform>();
            rect.gameObject.name = add.ToString();
            rect.localPosition = new Vector3(
                content.GetComponent<RectTransform>().sizeDelta.x / PageContentNum * add,
                0,
                0
                );

            //  最初に表示する情報を設定
            rect.transform.Find("Text").GetComponent<Text>().text = (ContentList[add].ToString());
        }



        for (int i = 0; i < ContentList.Count; i++)
        {
            if (i % PageContentNum == 0)
            {
                PageCount++;
            }
        }

        Debug.Log($"ページ数{PageCount}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
