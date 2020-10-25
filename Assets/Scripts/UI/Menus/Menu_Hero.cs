using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Hero : MonoBehaviour
{
    [SerializeField]
    private Image heroImage;

    [SerializeField]
    private TextMeshProUGUI heroName;

    [SerializeField]
    private TextMeshProUGUI heroVocation;

    [SerializeField]
    private TextMeshProUGUI heroHealth;

    [SerializeField]
    private TextMeshProUGUI heroExperience;

    [SerializeField]
    private TextMeshProUGUI heroBioText;

    [SerializeField]
    private Button editBioBtn;

    [SerializeField]
    private TMP_InputField heroBioInput;

    [SerializeField]
    private Button heroBioDone;

    [SerializeField]
    private Button heroBioCancel;

    private GuildMember hero;
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void UpdateHeroPanel()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        heroImage.sprite = hero.Avatar;
        heroName.text = hero.person.name;
    }

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == gameObject && hero != null)
        {
            heroVocation.text = string.Format("Level {0} {1}", hero.Level, hero.Vocation.Title());
            heroExperience.text = string.Format("Experience: {0} / {1}", hero.Experience, CharacterLevel.LevelValues[hero.Level]);
            heroHealth.text = string.Format("Health: {0}/{1}", hero.Hitpoints.ToString(), hero.MaxHitpoints.ToString());
            heroBioText.text = hero.bio;
        }
    }

    public void OpenBioEditor()
    {
        editBioBtn.gameObject.SetActive(false);
        heroBioText.gameObject.SetActive(false);
        heroBioInput.gameObject.SetActive(true);
        heroBioDone.gameObject.SetActive(true);
        heroBioCancel.gameObject.SetActive(true);
        heroBioInput.text = hero.bio;
    }

    public void UpdateBio()
    {
        hero.bio = heroBioInput.text;
        CloseBioEditor();
    }

    public void CancelUpdateBio()
    {
        heroBioInput.text = "";
        CloseBioEditor();
    }

    private void CloseBioEditor()
    {
        editBioBtn.gameObject.SetActive(true);
        heroBioText.gameObject.SetActive(true);
        heroBioInput.gameObject.SetActive(false);
        heroBioDone.gameObject.SetActive(false);
        heroBioCancel.gameObject.SetActive(false);
    }
}
