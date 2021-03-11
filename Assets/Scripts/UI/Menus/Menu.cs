using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public List<Menu> adjacentMenus = new List<Menu>();

    protected MenuManager menuManager;

    protected virtual void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Issues all commands necessary for the menu to load successfully. This intended to clean up the OnClick() button events
    public virtual void Open() {
        menuManager.OpenMenu(this);
        foreach(Menu menu in adjacentMenus)
        {
            menu.gameObject.SetActive(true);
        }
    }

    public virtual void Close() {
        StartCoroutine(CloseMenu());
    }

    private IEnumerator CloseMenu() {
        GetComponent<Animator>().SetTrigger("Close");
        foreach (Menu menu in adjacentMenus)
        {
            menu.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(3);
        //gameObject.SetActive(false);
    }

    private void FixedUpdate() {
        if (menuManager.CurrentMenu == gameObject)
        {
            // Do active menu stuff
        }
    }
}
