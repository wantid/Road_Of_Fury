using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject bonus;

    public Transform[] spawnPoints;
    public Transform[] bonusesPoints;

    void Start()
    {
        if (Random.value > .3f)
        {
            int k = Random.Range(0, spawnPoints.Length);
            if (spawnPoints[k].gameObject.activeSelf)
            {
                if (Random.value > .8f)
                    Instantiate(bonus, spawnPoints[k]).GetComponent<Bonus>().type = 0;
                else
                    Instantiate(bonus, spawnPoints[k]).GetComponent<Bonus>().type = (BonusType)Random.Range(0, System.Enum.GetValues(typeof(BonusType)).Length);
            }
        }

        int i = Random.Range(0, spawnPoints.Length);
        if (spawnPoints[i].gameObject.activeSelf)
        {
            Transform scrollingParent = FindObjectOfType<BackgroundScroll>().scrollingParent;

            if (Random.value > .9f)
                Instantiate(objects[Random.Range(1, objects.Length)], spawnPoints[i].position, Quaternion.identity, scrollingParent);
            else
            {
                CarController car = Instantiate(objects[0], spawnPoints[i].position, Quaternion.identity, scrollingParent).GetComponent<CarController>();
                car.direction = i;
                car.speed = Random.Range(5f, 8f);
            }
        }
    }
}
