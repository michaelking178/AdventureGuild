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

    private Guildhall guildhall;
    private int goldCost, woodCost, ironCost;

    private Color affordable = new Color(0, 0.34f, 0);
    private Color unaffordable = new Color(0.5f, 0, 0);

    private void Start()
    {
        guildhall = FindObjectOfType<Guildhall>();
    }

    public void Init(string _description, int _goldCost, int _woodCost, int _ironCost)
    {
        goldCost = _goldCost;
        woodCost = _woodCost;
        ironCost = _ironCost;
        description.text = _description;
    }

    private void FixedUpdate()
    {
        goldText.text = string.Format("Gold: {0} / {1}", guildhall.Gold.ToString(), goldCost.ToString());
        woodText.text = string.Format("Wood: {0} / {1}", guildhall.Wood.ToString(), woodCost.ToString());
        ironText.text = string.Format("Iron: {0} / {1}", guildhall.Iron.ToString(), ironCost.ToString());

        if (goldCost <= guildhall.Gold) goldText.color = affordable;
        else goldText.color = unaffordable;

        if (woodCost <= guildhall.Wood) woodText.color = affordable;
        else woodText.color = unaffordable;

        if (ironCost <= guildhall.Iron) ironText.color = affordable;
        else ironText.color = unaffordable;
    }
}
