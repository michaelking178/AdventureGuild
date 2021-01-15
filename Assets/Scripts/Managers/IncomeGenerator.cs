using System;
using System.Collections;
using UnityEngine;

public class IncomeGenerator : MonoBehaviour
{
    private Guildhall guildhall;
    int goldToAdd = 0;
    int ironToAdd = 0;
    int woodToAdd = 0;

    private void Start()
    {
        guildhall = GetComponent<Guildhall>();
        StartCoroutine(BeginGenerateIncome());
    }

    private void GenerateIncome()
    {
        int interval = (int)(DateTime.Now - guildhall.StartTime).TotalSeconds;
        if (interval / 3600 >= 1)
        {
            int totalIntervalsPassed = Mathf.FloorToInt(interval / 3600);
            goldToAdd = guildhall.GoldIncome * totalIntervalsPassed;
            ironToAdd = guildhall.IronIncome * totalIntervalsPassed;
            woodToAdd = guildhall.WoodIncome * totalIntervalsPassed;

            if (goldToAdd > guildhall.MaxGoldIncome) goldToAdd = guildhall.MaxGoldIncome;
            if (ironToAdd > guildhall.MaxIronIncome) ironToAdd = guildhall.MaxIronIncome;
            if (woodToAdd > guildhall.MaxWoodIncome) woodToAdd = guildhall.MaxWoodIncome;

            guildhall.AdjustGold(goldToAdd);
            guildhall.AdjustIron(ironToAdd);
            guildhall.AdjustWood(woodToAdd);
            guildhall.StartTime = DateTime.Now;
        }
    }

    private IEnumerator BeginGenerateIncome()
    {
        yield return new WaitForSeconds(1);
        GenerateIncome();
        if (goldToAdd > 0 || woodToAdd > 0 || ironToAdd > 0)
        {
            FindObjectOfType<NotificationManager>().CreateNotification(string.Format("Peasants earned {0} GOLD, {1} WOOD, and {2} IRON while you were away!", goldToAdd, woodToAdd, ironToAdd), Notification.Spirit.Good);
        }
        StartCoroutine(DelayedGenerateIncome());
    }

    private IEnumerator DelayedGenerateIncome()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            GenerateIncome();
        }
    }
}
