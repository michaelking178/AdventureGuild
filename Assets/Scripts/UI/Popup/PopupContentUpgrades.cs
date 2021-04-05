using TMPro;
using UnityEngine;

public class PopupContentUpgrades : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI goldText;

    [SerializeField]
    private TextMeshProUGUI woodText;

    [SerializeField]
    private TextMeshProUGUI ironText;

    [SerializeField]
    private TextMeshProUGUI artisansText;

    private Guildhall guildhall;
    private PopulationManager populationManager;
    private int goldCost, woodCost, ironCost, artisanCost;

    private Color affordable = new Color(0, 0.34f, 0);
    private Color unaffordable = new Color(0.5f, 0, 0);

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
        populationManager = FindObjectOfType<PopulationManager>();
    }

    public void Init(string _description, int _goldCost, int _woodCost, int _ironCost, int _artisanCost)
    {
        goldCost = _goldCost;
        woodCost = _woodCost;
        ironCost = _ironCost;
        artisanCost = _artisanCost;
        description.text = _description;
    }

    private void FixedUpdate()
    {
        goldText.text = $"Gold: {guildhall.Gold} / {goldCost}";
        woodText.text = $"Wood: {guildhall.Wood} / {woodCost}";
        ironText.text = $"Iron: {guildhall.Iron} / {ironCost}";
        if (artisanCost == 0)
        {
            artisansText.text = "";
        }
        else
        {
            artisansText.text = $"Artisans required: {artisanCost}";
        }

        if (goldCost <= guildhall.Gold) goldText.color = affordable;
        else goldText.color = unaffordable;

        if (woodCost <= guildhall.Wood) woodText.color = affordable;
        else woodText.color = unaffordable;

        if (ironCost <= guildhall.Iron) ironText.color = affordable;
        else ironText.color = unaffordable;

        if (artisanCost <= populationManager.Artisans().Count) artisansText.color = affordable;
        else artisansText.color = unaffordable;
    }
}
