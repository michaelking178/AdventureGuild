using UnityEngine;

public class Menu_WatchAds : Menu
{
    #region Data

    [SerializeField]
    private GameObject contentPanel;

    [SerializeField]
    private GameObject boostFramePrefab;

    #endregion

    private void Start()
    {
        PopulateBoosts();
    }

    private void PopulateBoosts()
    {
        foreach (GameObject child in FindObjectOfType<BoostManager>().gameObject.GetChildren())
        {
            GameObject boostFrame = Instantiate(boostFramePrefab, contentPanel.transform);
            boostFrame.GetComponent<BoostItemFrame>().Boost = child.GetComponent<Boost>();
            boostFrame.name = "BoostFrame_" + child.name;
        }
    }
}
