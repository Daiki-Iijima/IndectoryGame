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

    public List<GameObject> gameObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        createBtn.onClick.AddListener(() =>
        {
            gameObjects.Add(Instantiate(createPrefab));
            GameObject obj= Instantiate(infoPrefab);

            var data = new CreateObjectAttribute
            {
                Name = "",
                ItemType = ObjectTypeAttribute.Type.Gear
            };

            obj.GetComponent<ScrollViewContent>().SetInfoData(data);
            obj.transform.SetParent(scrollContaint.transform,false);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
