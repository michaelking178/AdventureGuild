using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> menus = new List<GameObject>();

    [SerializeField]
    private Menu startingMenu;

    [SerializeField]
    private GameObject clickBlocker;

    public Menu CurrentMenu;

    private void Start()
    {
        if (SaveSystem.SaveFileExists() && FindObjectOfType<LevelManager>().CurrentLevel() == "Main")
        {
            OpenMenu(GameObject.Find("Menu_Hub").GetComponent<Menu>());
            return;
        }
        if (startingMenu == null)
        {
            Debug.Log("No starting menu selected!");
        }
        else
        {
            OpenMenu(startingMenu);
        }
    }

    public void OpenMenu(Menu menu)
    {
        StartCoroutine(MenuTransition(menu));
    }

    private IEnumerator MenuTransition(Menu menu)
    {
        clickBlocker.SetActive(true);
        if (CurrentMenu != null)
        {
            CurrentMenu.Close();
            yield return new WaitForSeconds(0.4f);
        }
        CurrentMenu = menu;
        CurrentMenu.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.35f);
        clickBlocker.SetActive(false);
    }

    //public GameObject GetMenu(Menu menu)
    //{
    //    foreach (GameObject _menu in menus)
    //    {
    //        if (_menu.name == menuName)
    //            return _menu;
    //    }
    //    return null;
    //}

    public void DisableMenu(Menu menu)
    {
        foreach (GameObject _menu in menus)
        {
            if (_menu == menu)
            {
                _menu.SetActive(false);
                return;
            }
        }
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
        DisableMenu(GameObject.Find("Menu_Start").GetComponent<Menu>());
        DisableMenu(GameObject.Find("Menu_Start_2").GetComponent<Menu>());
        DisableMenu(GameObject.Find("Menu_CharacterCreator_1").GetComponent<Menu>());
        DisableMenu(GameObject.Find("Menu_CharacterCreator_2").GetComponent<Menu>());
        DisableMenu(GameObject.Find("Menu_CharacterCreator_3").GetComponent<Menu>());
    }

    #endregion
}
