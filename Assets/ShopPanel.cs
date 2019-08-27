using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private Button closeBtn;
    [SerializeField]
    private Button sellBtn;
    [SerializeField]
    private ObjectManager manager;



    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });


        sellBtn.onClick.AddListener(() =>
        {
            if (manager.GetItemCount == 0)
            {
                Debug.Log("売るアイテムがないよ");
                return;
            }
            manager.Money += manager.GetItemCount * 8;
            manager.GetItemCount = 0;
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
