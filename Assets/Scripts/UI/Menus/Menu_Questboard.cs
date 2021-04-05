using System.Collections.Generic;
using UnityEngine;

public class Menu_Questboard : Menu
{
    public GameObject postingPanel;
    public GameObject questPostPrefab;
    public int numberOfQuests;

    public int numCols = 5;
    public int numRows = 5;

    private float colWidth, rowHeight;
    private Vector2 randomPos;
    private QuestManager questManager;

    private List<Vector2> usedCoordinates = new List<Vector2>();

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        RefreshQuests();
    }

    public override void Open()
    {
        base.Open();
        RefreshQuests();
    }

    public void RefreshQuests()
    {
        ClearQuests();

        colWidth = postingPanel.GetComponent<RectTransform>().rect.width / numCols;
        rowHeight = postingPanel.GetComponent<RectTransform>().rect.height / numRows;

        foreach (Quest quest in questManager.GetQuestsByStatus(Quest.Status.New)) {
            GameObject questPost = Instantiate(questPostPrefab, postingPanel.transform);
            questPost.GetComponent<QuestPost>().SetQuest(quest);
            GetRandomPos();

            randomPos.x = randomPos.x * colWidth - 800.0f;
            randomPos.y = randomPos.y * rowHeight - 800.0f;
            questPost.transform.localPosition = randomPos;

            Quaternion rot = new Quaternion(0, 0, 0, 1) {
                eulerAngles = new Vector3(0, 0, Random.Range(-20.0f, 20.0f))
            };
            questPost.transform.rotation = rot;
        }
    }

    private void ClearQuests()
    {
        foreach(GameObject child in postingPanel.GetChildren()) {
            if (child != postingPanel) {
                Destroy(child);
            }
        }
        usedCoordinates.Clear();
    }

    private void GetRandomPos()
    {
        randomPos = new Vector2(Random.Range(0, numCols), Random.Range(0, numRows));
        if (!usedCoordinates.Contains(randomPos)) {
            usedCoordinates.Add(randomPos);
            return;
        }
        GetRandomPos();
    }
}
