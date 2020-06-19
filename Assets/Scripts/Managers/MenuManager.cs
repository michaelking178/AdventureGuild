using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> menus = new List<GameObject>();

    [SerializeField]
    private GameObject startingMenu;

    private GameObject currentMenu;
    private GameObject hero;

    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");

        if (hero.GetComponent<Person>())
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

    private IEnumerator MenuTransition(string menuName)
    {
        if (currentMenu != null)
        {
            currentMenu.GetComponent<Animator>().SetTrigger("Close");
            yield return new WaitForSeconds(0.5f);

        }
        GetMenu(menuName).GetComponent<Animator>().SetTrigger("Open");
        currentMenu = GetMenu(menuName);
        yield return new WaitForSeconds(0.5f);
    }

    private GameObject GetMenu(string menuName)
    {
        foreach (GameObject _menu in menus)
        {
            if (_menu.name == menuName)
                return _menu;
        }
        return null;
    }
}
