using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public BonusType type;
    public Sprite[] bonusSprites;
    public int change;
    void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case (BonusType)0:
                sprite.sprite = bonusSprites[0];
                change = Random.Range(20,50);
                break;
            case (BonusType)1:
                sprite.sprite = bonusSprites[1];
                change = Random.Range(20, 50);
                break;
            default:
                Debug.Log($"Type of bonus {gameObject.name} is undefined");
                break;
        }
    }
}
public enum BonusType
{
    Fuel, 
    health,
}
