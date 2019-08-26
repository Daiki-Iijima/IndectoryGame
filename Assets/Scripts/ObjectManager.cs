using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject placeField;
    [SerializeField]
    private Button createBtn;

    [SerializeField]
    private GameObject picePrefab;

    [SerializeField]
    private Text MoneyText;

    [SerializeField]
    private GameObject shopPanel;
    #region ボタン

    [SerializeField]
    private Button sellButton;

    private FieldAttribute.Type FieldType = FieldAttribute.Type.Farmland;

    [SerializeField]
    private Button farmalandButton;
    [SerializeField]
    private Button plowed_FarmalandButton;
    [SerializeField]
    private Button wastelandButton;
    [SerializeField]
    private Button RoadButton;

    #endregion

    public int CreateCountX;
    public int CreateCountY;

    public float ScrollSpeed;
    public int GetItemCount = 0;

    public int Money = 0;

    // Start is called before the first frame update
    void Start()
    {
        createBtn.onClick.AddListener(() =>
        {

        });

        sellButton.onClick.AddListener(() =>
        {
            if (!shopPanel.activeSelf) { shopPanel.SetActive(true); return; }

            if (GetItemCount == 0)
            {
                Debug.Log("売るアイテムがないよ");
                return;
            }
            Money += GetItemCount * 8;
            GetItemCount = 0;
        });

        //  フィールドの生成
        for (int countX = -CreateCountX / 2; countX < CreateCountX / 2; countX++)
        {
            for (int countY = -CreateCountY / 2; countY < CreateCountY / 2; countY++)
            {
                var obj = Instantiate(picePrefab, new Vector3(countX * placeField.transform.lossyScale.x, 0, countY * placeField.transform.lossyScale.z), Quaternion.identity, placeField.transform);
            }
        }

        farmalandButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Farmland);
        plowed_FarmalandButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Plowed_farmland);
        wastelandButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Wasteland);
        RoadButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Road);

    }

    public Vector3 RandomVector3(float start, float end)
    {
        return new Vector3(Random.Range(start, end), Random.Range(start, end), Random.Range(start, end));
    }
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "$ : " + Money.ToString();
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(GetTouchRay(isEnableDrawRay: true), out hitInfo))
        {
            if (Input.GetMouseButtonDown(0))
            {
                var piceController = hitInfo.collider.gameObject.GetComponent<PiceController>();


                //  TODO : Canvas状のUIの当たり判定の取り方を検討
                //Debug.Log(hitInfo.transform.gameObject.layer);

                ////  UIとの当たり判定を先に計算
                ////  UIが存在していた場合、ブロックのクリックへ判定がいかないように
                //if(hitInfo.collider.gameObject.layer ==5)
                //{
                //    return;
                //}

                //  作物の回収を優先
                if (piceController.FieldType == FieldAttribute.Type.Planted)
                {
                    GetItemCount += piceController.Harvest();
                    return;
                }

                piceController.Selected(FieldType);

                switch (piceController.FieldType)
                {
                    case FieldAttribute.Type.Farmland:
                        Debug.Log("農地");
                        break;
                    case FieldAttribute.Type.Plowed_farmland:
                        Debug.Log("耕した土地");
                        piceController.PlantSeed();
                        break;
                    case FieldAttribute.Type.Road:
                        Debug.Log("道");
                        break;
                    case FieldAttribute.Type.Wasteland:
                        Debug.Log("荒地");
                        break;
                }
            }

            if (Input.GetMouseButton(1))
            {
                Debug.Log(Input.mousePosition);
                var pos = placeField.transform.position;
                if (Input.mousePosition.x < 300)
                {
                    pos = new Vector3(pos.x + ScrollSpeed, pos.y, pos.z);
                }
                else
                {
                    pos = new Vector3(pos.x - ScrollSpeed, pos.y, pos.z);
                }

                if (Input.mousePosition.y < 150)
                {
                    pos = new Vector3(pos.x, pos.y, pos.z + ScrollSpeed);
                }
                else
                {
                    pos = new Vector3(pos.x, pos.y, pos.z - ScrollSpeed);
                }

                placeField.transform.position = pos;
            }
        }
    }

    /// <summary>
    /// スクリーン座標でタッチされた位置から飛ばしたRayを返す
    /// </summary>
    /// <param name="isEnableDrawRay">デバッグRayを表示するか</param>
    /// <returns>スクリーン座標系に変換されたRay</returns>
    public Ray GetTouchRay(bool isEnableDrawRay = false)
    {
        Vector2 touchScreenPosition = Input.mousePosition;

        touchScreenPosition.x = Mathf.Clamp(touchScreenPosition.x, 0.0f, Screen.width);
        touchScreenPosition.y = Mathf.Clamp(touchScreenPosition.y, 0.0f, Screen.height);

        Camera gameCamera = Camera.main;
        Ray touchPointToRay = gameCamera.ScreenPointToRay(touchScreenPosition);

        if (isEnableDrawRay)
        {
            // デバッグ用のRay表示
            Debug.DrawRay(touchPointToRay.origin, touchPointToRay.direction * 1000.0f);
        }

        return touchPointToRay;
    }
}
