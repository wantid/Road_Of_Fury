using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Background Settings")]
    public float minY;
    public float snapLength;
    public GameObject[] roadObjects;
    public Transform scrollingParent;
    [Header("Starting Objects")]
    public GameObject nextBackround;
    public List<Transform> backround = new List<Transform>();

    [HideInInspector] public float carSpeed;
    private int currentRoad;

    private void Update()
    {
        Scroll();
    }
    private void Scroll()
    {
        scrollingParent.Translate(Vector2.down * Time.deltaTime * carSpeed);
        if (backround[0].position.y < minY)
        {
            Destroy(backround[0].gameObject);
            backround.Remove(backround[0]);
            Vector2 pos = new Vector2(0, backround[backround.Count - 1].position.y + snapLength);
            backround.Add(Instantiate(nextBackround, pos, Quaternion.identity, scrollingParent).GetComponent<Transform>());

            BackgroundChange();
        }
        carSpeed = GetComponent<PlayerController>().currentSpeed;
    }
    private void BackgroundChange()
    {
        switch (currentRoad)
        {
            case 1:
                currentRoad = 3;
                nextBackround = roadObjects[currentRoad];
                break;
            case 2:
                currentRoad = 5;
                nextBackround = roadObjects[currentRoad];
                break;
            case 3:
                if (Random.value > .8f) currentRoad = 4;
                nextBackround = roadObjects[currentRoad];
                break;
            case 4:
                currentRoad = 0;
                nextBackround = roadObjects[currentRoad];
                break;
            case 5:
                if (Random.value > .8f) currentRoad = 6;
                nextBackround = roadObjects[currentRoad];
                break;
            case 6:
                currentRoad = 0;
                nextBackround = roadObjects[currentRoad];
                break;
            default:
                if (Random.value > .5f) currentRoad = Random.Range(0, 3);
                nextBackround = roadObjects[currentRoad];
                break;
        }
    }
}
