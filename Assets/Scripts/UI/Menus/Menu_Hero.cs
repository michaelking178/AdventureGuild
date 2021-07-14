using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Hero : Menu
{
    #region Data

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
    private Slider expSlider;
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Button editBioBtn;

    [SerializeField]
    private TMP_InputField heroBioInput;

    [SerializeField]
    private Button heroBioDone;

    [SerializeField]
    private Button heroBioCancel;

    private GuildMember hero;

    #endregion

    private void FixedUpdate()
    {
        if (menuManager.CurrentMenu == this && hero != null)
        {
            heroVocation.text = $"Level {hero.Level} {hero.Vocation.Title()}";
            heroExperience.text = $"{hero.Experience} / {Levelling.GuildMemberLevel[hero.Level]}";
            heroHealth.text = $"{hero.Hitpoints}/{hero.MaxHitpoints}";
            heroBioText.text = hero.Quip;
            UpdateExpSlider();
            UpdateHealthSlider();
        }
    }

    public override void Open()
    {
        base.Open();
        UpdateHeroPanel();
    }

    public override void Close()
    {
        if (heroBioInput.gameObject.activeInHierarchy == true)
            CancelUpdateBio();
        base.Close();
    }

    public void OpenBioEditor()
    {
        editBioBtn.gameObject.SetActive(false);
        heroBioText.gameObject.SetActive(false);
        heroBioInput.gameObject.SetActive(true);
        heroBioDone.gameObject.SetActive(true);
        heroBioCancel.gameObject.SetActive(true);
        heroBioInput.text = hero.Quip;
    }

    public void UpdateBio()
    {
        hero.Quip = heroBioInput.text;
        //hero.Bio = heroBioInput.text;
        CloseBioEditor();
    }

    public void CancelUpdateBio()
    {
        heroBioInput.text = "";
        CloseBioEditor();
    }

    private void UpdateHeroPanel()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<GuildMember>();
        heroImage.sprite = hero.Avatar;
        heroName.text = hero.person.name;
    }

    private void CloseBioEditor()
    {
        editBioBtn.gameObject.SetActive(true);
        heroBioText.gameObject.SetActive(true);
        heroBioInput.gameObject.SetActive(false);
        heroBioDone.gameObject.SetActive(false);
        heroBioCancel.gameObject.SetActive(false);
    }

    private void UpdateExpSlider()
    {
        expSlider.minValue = Levelling.GuildMemberLevel[hero.Level - 1];
        expSlider.maxValue = Levelling.GuildMemberLevel[hero.Level];
        expSlider.value = hero.Experience;
    }

    private void UpdateHealthSlider()
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = hero.MaxHitpoints;
        healthSlider.value = hero.Hitpoints;
    }
}
