using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject createPrefab;

    [SerializeField]
    private GameObject infoPrefab;

    [SerializeField]
    private GameObject placeField;
    [SerializeField]
    private Button createBtn;

    [SerializeField]
    private GameObject scrollContaint;

    [SerializeField]
    private GameObject picePrefab;
    public List<GameObject> gameObjects;

    #region モードボタン
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
    public int CreatedField { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        createBtn.onClick.AddListener(() =>
        {
            CreatedField++;
            gameObjects.Add(Instantiate(createPrefab, RandomVector3(0, 10f), Quaternion.identity));
            GameObject obj = Instantiate(infoPrefab);

            var data = new ItemAttribute
            {
                Name = "",
                ItemType = ItemTypeAttribute.Type.Food
            };

            obj.GetComponent<ViewContent>().ItemInfo = data;
            obj.transform.SetParent(scrollContaint.transform, false);
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

        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(GetTouchRay(isEnableDrawRay: true), out hitInfo))
        {
            if (Input.GetMouseButtonDown(0))
            {
                var piceController = hitInfo.collider.gameObject.GetComponent<PiceController>();

                piceController.Selected(FieldType);

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


            }

            if (Input.GetMouseButton(1))
            {
                Debug.Log(Input.mousePosition);
                var pos = placeField.transform.position;
                if (Input.mousePosition.x < 300)
                {
                    pos = new Vector3(pos.x + 0.4f, pos.y, pos.z);
                }
                else
                {
                    pos = new Vector3(pos.x - 0.4f, pos.y, pos.z);
                }

                if (Input.mousePosition.y < 150)
                {
                    pos = new Vector3(pos.x, pos.y, pos.z + 0.4f);
                }
                else
                {
                    pos = new Vector3(pos.x, pos.y, pos.z - 0.4f);
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
