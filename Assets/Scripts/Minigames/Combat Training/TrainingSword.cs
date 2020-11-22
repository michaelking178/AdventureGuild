using System.Collections;
using UnityEngine;

public class TrainingSword : MonoBehaviour
{
    [SerializeField]
    private AudioClip swordWoosh;

    [SerializeField]
    private AudioClip[] swordClangs;

    public int Swings = 0;
    public int Hits = 0;

    private AudioSource audioSource;
    private Vector2 startPos;
    private Vector2 movementPos;
    private float distance;
    private int speed = 25;
    private float startTime = 0f;
    private float swingResistance = 0.1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
        movementPos = startPos;
    }

    private void FixedUpdate()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fraction = 0;
        if (distCovered > 0)
        {
            fraction = distCovered / distance;
        }
        transform.position = Vector2.Lerp(startPos, movementPos, fraction);
    }

    public void Swing(Vector2 position)
    {
        if (!FindObjectOfType<TrainingManager>().GameOver)
        {
            Swings++;
            StartCoroutine(SwingMovement(position));
        }
    }

    private IEnumerator SwingMovement(Vector2 position)
    {
        startPos = transform.position;
        movementPos = position;
        startTime = Time.time;
        distance = Vector2.Distance(startPos, movementPos);
        yield return new WaitForSeconds(swingResistance);

        startPos = transform.position;
        movementPos = Vector2.zero;
        startTime = Time.time;
        distance = Vector2.Distance(startPos, movementPos);
        yield return new WaitForSeconds(swingResistance);
    }

    public void ClangSound()
    {
        audioSource.clip = swordClangs[Random.Range(0, swordClangs.Length)];
        audioSource.Play();
    }

    public void WooshSound()
    {
        audioSource.clip = swordWoosh;
        audioSource.Play();
    }
}
