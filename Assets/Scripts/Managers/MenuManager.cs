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
    private GameObject clickBlocker;

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
        clickBlocker.SetActive(true);
        if (CurrentMenu != null)
        {
            CloseMenu();
            yield return new WaitForSeconds(0.4f);
        }
        CurrentMenu = GetMenu(menuName);
        CurrentMenu.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.35f);
        clickBlocker.SetActive(false);
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

    public void DisableMenu(string menuName)
    {
        foreach (GameObject _menu in menus)
        {
            if (_menu.name == menuName)
            {
                _menu.SetActive(false);
                return;
            }
        }
        Debug.Log("MenuManager cannot destroy menu " + menuName);
    }

    #region Disable Character Creator after character creation is finished

    public void DisableCharacterCreation()
    {
        StartCoroutine(DisableCharacterCreator());
    }

    // Wait 3 seconds to allow for transition to finish before destruction
    private IEnumerator DisableCharacterCreator()
    {
        yield return new WaitForSeconds(3);
        DisableMenu("Menu_Start");
        DisableMenu("Menu_Start_2");
        DisableMenu("Menu_CharacterCreator_1");
        DisableMenu("Menu_CharacterCreator_2");
        DisableMenu("Menu_CharacterCreator_3");
    }

    #endregion
}
