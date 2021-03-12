using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Menu startingMenu;

    [SerializeField]
    private GameObject clickBlocker;

    public Menu CurrentMenu;

    private void Start()
    {
        if (SaveSystem.SaveFileExists() && FindObjectOfType<LevelManager>().CurrentLevel() == "Main")
        {
            OpenMenu(FindObjectOfType<Menu_Hub>());
            return;
        }
        if (startingMenu == null)
            Debug.Log("No starting menu selected!");
        else
            OpenMenu(startingMenu);
    }

    public void OpenMenu(Menu menu)
    {
        StartCoroutine(MenuTransition(menu));
    }

    public void EnableMenu(Menu menu)
    {
        menu.gameObject.SetActive(true);
    }

    public void DisableMenu(Menu menu)
    {
        menu.gameObject.SetActive(false);
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
}
