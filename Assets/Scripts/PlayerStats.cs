using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GameObject explosion;


    [Header("UI")]
    public GameObject deathScreen;
    public GameObject muted;
    public GameObject controls;
    public GameObject controlsOff;
    [Header("Fuel")]
    public float maxFuel;
    public Text fuelText;
    float fuel;
    [Header("Health")]
    public int maxHealth;
    public Text healthText;
    int health;

    bool isDead;

    [HideInInspector] public SoundManager sm;
    [HideInInspector] public GameManager gm;

    void Start()
    {
        fuel = maxFuel;
        health = maxHealth;

        sm = GetComponent<SoundManager>();
        gm = FindObjectOfType<GameManager>();

        LoadSaveData();
    }
    void Update()
    {
        if (!isDead)
            FuelChange(-Time.deltaTime * 2);

        fuelText.text = $"{Mathf.RoundToInt(fuel)}";
        healthText.text = $"{health}";

        if ((fuel <= 0 || health <= 0) && !isDead)
            Death();
    }
    public IEnumerator Continue()
    {
        yield return new WaitForSeconds(1);
        FuelChange(80);
        HealthChange(50);
        deathScreen.SetActive(false);

        isDead = false;
        PlayerController controller = GetComponent<PlayerController>();
        controller.playerObject.GetComponent<SpriteRenderer>().color = Color.white;
        controller.isDead = isDead;
    }
    public void ShowRewarded()
    {
        gm.ShowReward(GetComponent<PlayerStats>());
    }
    public void Restart()
    {
        gm.coins = 0;
        gm.UpdateCoinsText();
        SceneManager.LoadScene("Game");
    }
    private void Death()
    {
        gm.OnDeath();

        sm.PlaySound(2);

        isDead = true;
        PlayerController controller = GetComponent<PlayerController>();
        /*
        controller.playerObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        controller.playerObject.GetComponent<Rigidbody2D>().freezeRotation = false;*/
        controller.playerObject.GetComponent<SpriteRenderer>().color = new Color(.2f, .2f, .2f);
        controller.isDead = isDead;

        Instantiate(explosion, controller.playerObject.transform);

        deathScreen.SetActive(true);
    }
    public void ScoreChange(int chg)
    {
        gm.AddCoin(chg);
    }
    public void FuelChange(float chg)
    {
        if (chg > 0)
        {
            sm.PlaySound(0);
            ScoreChange(1);
        }

        fuel += chg;
        if (fuel > maxFuel)
            fuel = maxFuel;
        else if (fuel < 0)
            fuel = 0;
    }
    public void HealthChange(int chg)
    {
        if (chg > 0)
        {
            sm.PlaySound(0);
            ScoreChange(1);
        }
        else
            sm.PlaySound(1);

        health += chg;
        if (health > maxHealth)
            health = maxHealth;
        else if (health < 0)
            health = 0;
    }
    private void LoadSaveData()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");

        if (AudioListener.volume == 0)
            muted.SetActive(true);

        if (PlayerPrefs.GetInt("ShowControls") == 1)
        {
            controlsOff.SetActive(false);
            controls.SetActive(true);
        }
    }
    public void Mute()
    {
        if (AudioListener.volume != 0)
        {
            AudioListener.volume = 0;
            muted.SetActive(true);
        }
        else
        {
            AudioListener.volume = 1f;
            muted.SetActive(false);
        }

        PlayerPrefs.SetFloat("Volume", AudioListener.volume);

        PlayerPrefs.Save();
    }
    public void ControlsShow()
    {
        if (controls.activeSelf)
        {
            controls.SetActive(false);
            controlsOff.SetActive(true);
            PlayerPrefs.SetInt("ShowControls", 0);
        }
        else 
        {
            controls.SetActive(true);
            controlsOff.SetActive(false);
            PlayerPrefs.SetInt("ShowControls", 1);
        }

        PlayerPrefs.Save();
    }
}
