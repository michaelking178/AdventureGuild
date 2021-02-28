using UnityEngine;

public class Menu_WatchAds : Menu
{
    #region Data

    [SerializeField]
    private GameObject contentPanel;

    [SerializeField]
    private GameObject boostFramePrefab;

    #endregion

    protected override void Start()
    {
        base.Start();
        PopulateBoosts();
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this)
        {
            // CheckForActiveBoosts
        }
    }

    private void PopulateBoosts()
    {
        foreach (GameObject child in FindObjectOfType<BoostManager>().gameObject.GetChildren())
        {
            GameObject boostFrame = Instantiate(boostFramePrefab, contentPanel.transform);
            boostFrame.GetComponent<BoostItemFrame>().boost = child.GetComponent<Boost>();
            boostFrame.name = "BoostFrame_" + child.name;
        }
    }
}
