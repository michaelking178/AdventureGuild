using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> menus = new List<GameObject>();

    [SerializeField]
    private GameObject startingMenu;

    [SerializeField]
    private GameObject clickBlockerPanel;

    public GameObject CurrentMenu;

    private void Start()
    {
        if (SaveSystem.SaveFileExists() && FindObjectOfType<LevelManager>().CurrentLevel() == "Main")
        {
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

    public void CloseMenu()
    {
        CurrentMenu.GetComponent<Animator>().SetTrigger("Close");
    }

    private IEnumerator MenuTransition(string menuName)
    {
        clickBlockerPanel.SetActive(true);
        if (CurrentMenu != null)
        {
            CloseMenu();
            yield return new WaitForSeconds(0.4f);
        }
        CurrentMenu = GetMenu(menuName);
        CurrentMenu.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.35f);
        clickBlockerPanel.SetActive(false);
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
