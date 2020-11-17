using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject Popup;
    public GameObject ContentPanel;
    public GameObject DefaultContent;
    public GameObject UpgradeContent;

    public void CallPopup()
    {
        Popup.gameObject.SetActive(true);
    }

    public void CreateUpgradeContent(string _description, int _goldCost, int _woodCost, int _ironCost, int _artisanCost)
    {
        ClearContent();
        GameObject popupContent = Instantiate(UpgradeContent, ContentPanel.transform);
        popupContent.GetComponent<PopupContentUpgrades>().Init(_description, _goldCost, _woodCost, _ironCost, _artisanCost);
    }

    public void CreateDefaultContent(string _description)
    {
        ClearContent();
        GameObject popupContent = Instantiate(DefaultContent, ContentPanel.transform);
        popupContent.GetComponentInChildren<TextMeshProUGUI>().text = _description;
    }

    public void SetSingleButton(string btnText)
    {
        Popup.GetComponent<PopupMenu>().SetSingleButton(btnText);
    }

    public void SetDoubleButton(string btnText1, string btnText2)
    {
        Popup.GetComponent<PopupMenu>().SetDoubleButton(btnText1, btnText2);
    }

    public void Populate(string _title, Sprite _sprite)
    {
        Popup.GetComponent<PopupMenu>().Populate(_title, _sprite);
    }

    private void ClearContent()
    {
        List<GameObject> children = Helpers.GetChildren(ContentPanel);
        foreach(GameObject obj in children)
        {
            Destroy(obj);
        }
    }
}
