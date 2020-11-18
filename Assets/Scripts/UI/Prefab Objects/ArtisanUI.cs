using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtisanUI : MonoBehaviour
{
    [SerializeField]
    private Image personUIPanel;

    [SerializeField]
    private GameObject extensionPanel;

    [SerializeField]
    private TextMeshProUGUI buttonText;

    private ConstructionManager constructionManager;
    private bool isSelected = false;
    private Color defaultColor = new Color(0.3f, 0.7f, 1f);
    private Color selectedColor = new Color(0.25f, 0.4f, 0.73f);

    private void Start()
    {
        constructionManager = FindObjectOfType<ConstructionManager>();
    }

    public void Select()
    {
        if (!isSelected)
        {
            isSelected = true;
            constructionManager.AddArtisan(GetComponent<PersonUI>().GuildMember);
            buttonText.text = "Deselect Artisan";
        }
        else
        {
            isSelected = false;
            constructionManager.RemoveArtisan(GetComponent<PersonUI>().GuildMember);
            buttonText.text = "Select Artisan";
        }
        SetColor();
        extensionPanel.SetActive(false);
    }

    private void SetColor()
    {
        if (isSelected) personUIPanel.color = selectedColor;
        else personUIPanel.color = defaultColor;
    }
}
