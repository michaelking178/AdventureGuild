using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionDemo : MonoBehaviour
{

    public GameObject demoSample;
    public Color BackgroundColor;
    public Color ShadowColor;
    public Vector2 ShadowSize;
    public Vector2 ShadowPos;
    public float AssetSize;
    public Sprite[] demoSprites;

    // Use this for initialization
    void Start()
    {
        Camera.main.backgroundColor = BackgroundColor;
        demoSample.GetComponent<Transform>().parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(AssetSize, AssetSize);
        for (int i = 0; i < demoSprites.Length; i++)
        {
            GameObject NewObject = Instantiate(demoSample);
            NewObject.transform.SetParent(demoSample.transform.parent);
            NewObject.GetComponent<RectTransform>().localScale = Vector3.one;
            NewObject.transform.GetChild(0).GetComponent<Image>().sprite = demoSprites[i];
            NewObject.transform.GetChild(0).GetComponent<Image>().color = ShadowColor;
            NewObject.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
            NewObject.transform.GetChild(0).GetComponent<RectTransform>().localScale = ShadowSize;
            NewObject.transform.GetChild(0).GetComponent<RectTransform>().localPosition = ShadowPos;
            NewObject.transform.GetChild(1).GetComponent<Image>().sprite = demoSprites[i];
            NewObject.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
            NewObject.SetActive(true);
        }
    }

}
