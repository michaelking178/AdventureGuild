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
            FindObjectOfType<Menu_Hub>().Open();
            return;
        }
        if (startingMenu == null)
            Debug.Log("No starting menu selected!");
        else
            startingMenu.Open();
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
            yield return new WaitForSeconds(0.5f);
        }
        CurrentMenu = menu;
        CurrentMenu.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.35f);
        clickBlocker.SetActive(false);
    }
}
