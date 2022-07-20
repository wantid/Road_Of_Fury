using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject muted;
    public GameObject controlsOff;

    public void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");

        if (AudioListener.volume == 0)
            muted.SetActive(true);

        if (PlayerPrefs.GetInt("ShowControls") == 1)
            controlsOff.SetActive(false);
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
        if (!controlsOff.activeSelf)
        {
            controlsOff.SetActive(true);
            PlayerPrefs.SetInt("ShowControls", 0);
        }
        else
        {
            controlsOff.SetActive(false);
            PlayerPrefs.SetInt("ShowControls", 1);
        }

        PlayerPrefs.Save();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
