using UnityEngine;

public class Menu : MonoBehaviour
{
    protected MenuManager menuManager;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    protected virtual void Start()
    {
        
    }

    // Issues all commands necessary for the menu to load successfully. This intended to clean up the OnClick() button events
    public virtual void Open() {
        menuManager.OpenMenu(this);
    }

    public virtual void Close() {
        GetComponent<Animator>().SetTrigger("Close"); ;
    }
}
