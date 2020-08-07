﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> menus = new List<GameObject>();

    [SerializeField]
    private GameObject startingMenu;

    private GameObject currentMenu;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        if (SaveSystem.SaveFileExists())
        {
            SaveSystem.LoadGame();
            OpenMenu("Menu_Hub");
            return;
        }
        if (startingMenu == null)
        {
            Debug.Log("No starting menu selected!");
        }
        else
        {
            OpenMenu(startingMenu.name);
        }
    }

    public void OpenMenu(string menuName)
    {
        StartCoroutine(MenuTransition(menuName));
    }

    private IEnumerator MenuTransition(string menuName)
    {
        if (currentMenu != null)
        {
            currentMenu.GetComponent<Animator>().SetTrigger("Close");
            soundManager.PlaySound("CloseMenu");
            yield return new WaitForSeconds(0.6f);
        }
        currentMenu = GetMenu(menuName);
        foreach (GameObject thisMenu in menus)
        {
            if (thisMenu != currentMenu)
            {
                thisMenu.SetActive(false);
            }
            else
            {
                thisMenu.SetActive(true);
            }
        }
        currentMenu.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        soundManager.PlaySound("OpenMenu");
    }

    public GameObject GetMenu(string menuName)
    {
        foreach (GameObject _menu in menus)
        {
            if (_menu.name == menuName)
                return _menu;
        }
        return null;
    }
}
