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
    private Button createBtn;

    [SerializeField]
    private GameObject scrollContaint;

    [SerializeField]
    private GameObject picePrefab;
    public List<GameObject> gameObjects;

    // Start is called before the first frame update
    void Start()
    {
        createBtn.onClick.AddListener(() =>
        {
            gameObjects.Add(Instantiate(createPrefab, RandomVector3(0, 10f), Quaternion.identity));
            GameObject obj = Instantiate(infoPrefab);

            var data = new CreateObjectAttribute
            {
                Name = "",
                ItemType = ObjectTypeAttribute.Type.Gear
            };

            obj.GetComponent<ScrollViewContent>().SetInfoData(data);
            obj.transform.SetParent(scrollContaint.transform, false);
        });

        //  フィールドの生成
        for (int countX = -5; countX <= 5; countX++)
        {
            for (int countY = -5; countY <= 5; countY++)
            {
                var obj = Instantiate(picePrefab, new Vector3(countX, 0, countY), Quaternion.identity);

                if (countX == 0)
                {
                    obj.GetComponent<PiceController>().Selected();
                }
            }
        }
    }


    public Vector3 RandomVector3(float start, float end)
    {
        return new Vector3(Random.Range(start, end), Random.Range(start, end), Random.Range(start, end));
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 touchScreenPosition = Input.mousePosition;

        touchScreenPosition.x = Mathf.Clamp(touchScreenPosition.x, 0.0f, Screen.width);
        touchScreenPosition.y = Mathf.Clamp(touchScreenPosition.y, 0.0f, Screen.height);

        Camera gameCamera = Camera.main;
        Ray touchPointToRay = gameCamera.ScreenPointToRay(touchScreenPosition);

        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(touchPointToRay, out hitInfo))
        {
            if (Input.GetMouseButton(0))
                hitInfo.collider.gameObject.GetComponent<PiceController>().Selected();
        }

        // デバッグ機能を利用して、スクリーンビューでレイが出ているか見てみよう。
        Debug.DrawRay(touchPointToRay.origin, touchPointToRay.direction * 1000.0f);

    }
}
