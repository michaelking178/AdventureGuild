﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu_ManagePeople : Menu
{
    #region Data

    [SerializeField]
    private Scrollbar scrollbar;

    private PersonUIScrollView scrollView;

    #endregion

    private void Start()
    {
        scrollView = GetComponentInChildren<PersonUIScrollView>();
    }

    public override void Open()
    {
        base.Open();
        scrollView.LoadPersonUIs();
        scrollView.SetPersonUIButtons(false, true, true);
        scrollbar.value = 1;
        foreach(GuildmemberGroup gmGroup in GetComponentsInChildren<GuildmemberGroup>())
        {
            gmGroup.Expand();
        }
    }

    public override void Close()
    {
        base.Close();
        StartCoroutine(ClearPersonUIs());
    }

    private IEnumerator ClearPersonUIs()
    {
        yield return new WaitForSeconds(1);
        scrollView.ClearPersonUIs();
        foreach (GuildmemberGroup gmGroup in GetComponentsInChildren<GuildmemberGroup>())
        {
            gmGroup.Collapse();
        }
    }
}
