using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    //DataManagerの部分は、シングルトンにするクラス名を指定して下さい。
    static public ObjectManager instance;
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

    }

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

    #region  Attribute

    public FieldAttribute.Type FieldType { get; private set; } = FieldAttribute.Type.Farmland;
    public PlayerStateAttribute.PlayerStateType playerState { get; private set; } = PlayerStateAttribute.PlayerStateType.None;

    #endregion

    #region ボタン
    // ============= ブロック変更フラグの変更 ==============
    [SerializeField] private Button shopButton;
    [SerializeField] private Button farmalandButton;
    [SerializeField] private Button plowed_FarmalandButton;
    [SerializeField] private Button wastelandButton;
    [SerializeField] private Button RoadButton;

    // ============= Playerの変更 ==============
    [SerializeField] private Button PlantButton;
    [SerializeField] private Button NoneButton;

    // ========================================
    #endregion

    public int CreateCountX;
    public int CreateCountY;

    public float ScrollSpeed;
    public int GetItemCount = 0;

    public int Money = 0;

    public int HaveSeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        createBtn.onClick.AddListener(() =>
        {

        });

        shopButton.onClick.AddListener(() =>
        {
            if (!shopPanel.activeSelf) { shopPanel.SetActive(true); return; }
        });

        //  フィールドの生成
        for (int countX = -CreateCountX / 2; countX < CreateCountX / 2; countX++)
        {
            for (int countY = -CreateCountY / 2; countY < CreateCountY / 2; countY++)
            {
                var obj = Instantiate(picePrefab, new Vector3(countX * placeField.transform.lossyScale.x, 0, countY * placeField.transform.lossyScale.z), Quaternion.identity, placeField.transform);
            }
        }

        //  ブロックを更新する属性の変更
        farmalandButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Farmland);
        plowed_FarmalandButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Plowed_farmland);
        wastelandButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Wasteland);
        RoadButton.onClick.AddListener(() => FieldType = FieldAttribute.Type.Road);

        //  プレイヤーの状態を変更
        PlantButton.onClick.AddListener(() => playerState = PlayerStateAttribute.PlayerStateType.Plant);
        NoneButton.onClick.AddListener(() => playerState = PlayerStateAttribute.PlayerStateType.None);
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

                //  作物の回収を優先
                if (piceController.FieldType == FieldAttribute.Type.Planted)
                {
                    GetItemCount += piceController.Harvest();
                    return;
                }

                //  UIの当たりがあったら処理をしないように
                if (IsExist()) { return; }



                //  クリックしたブロックが耕されている
                //  PlayerStateが植えるモード
                //  タネを持っていた場合
                if (piceController.FieldType == FieldAttribute.Type.Plowed_farmland &&
                    playerState == PlayerStateAttribute.PlayerStateType.Plant &&
                    HaveSeed > 0)
                {
                    Debug.Log("タネを植えます");
                    HaveSeed--;
                    piceController.PlantSeed();
                    return;
                }

                //  植えるモードだった場合
                if (playerState == PlayerStateAttribute.PlayerStateType.Plant) return;

                //  変更しているブロックの状態が変更しようとしている状態と同じだった場合戻す
                if (piceController.FieldType == FieldType) return;


                switch (piceController.FieldType)
                {
                    case FieldAttribute.Type.Farmland:
                        Debug.Log("農地");
                        break;
                    case FieldAttribute.Type.Plowed_farmland:
                        Debug.Log("耕した土地");
                        break;
                    case FieldAttribute.Type.Road:
                        Debug.Log("道");
                        break;
                    case FieldAttribute.Type.Wasteland:
                        Debug.Log("荒地");
                        break;
                }

                piceController.Selected(FieldType);

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

    /// <summary>
    /// UIがクリックされたか判定
    /// </summary>
    /// <returns></returns>
    public bool IsExist()
    {
        var current = EventSystem.current;
        var eventData = new PointerEventData(current)
        {
            position = Input.mousePosition
        };
        var raycastResults = new List<RaycastResult>();
        current.RaycastAll(eventData, raycastResults);
        var isExist = 0 < raycastResults.Count;
        return isExist;
    }
}
