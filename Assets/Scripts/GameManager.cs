using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    public GameObject LeaderboardEntries;

    public Text NameText;
    public GameObject CoinText;
    public Text MaxScoreText;
    private YandexSDK SDK;

    public GameObject ScrollView;
    public Transform SV_content;
    public GameObject _fab;

    private PlayerStats playerStats;

    public int coins;
    int maxScore;
    bool offline;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        SDK = YandexSDK.Instance;
        SDK.AuthSuccess += SettingMame;
        SDK.RewardGet += Rewarded;
        SDK.DataGet += SettingData;
        SDK.LeaderBoardReady += AddEntri;
        //UpdateCoinsText();

        Auth();
        GetLeaderBoardEntries();
    }
    public void OnDeath()
    {
        if (coins != 0 && coins > maxScore)
        {
            maxScore = coins;
            SetData();
            SetLeaderBoard();
            OfflineMaxScore();
        }

        ShowCommon();
        GetLeaderBoardEntries();
    }
    public void UpdateCoinsText()
    {
        CoinText.SetActive(true);
        CoinText.GetComponent<Text>().text = coins.ToString();
    }
    public void Auth()
    {
        SDK.Authenticate();
    }
    public void GetData()
    {
        SDK.GettingData();
    }
    public void SetData()
    {
        if (!offline)
            MaxScoreText.text = $"MAX: {maxScore}";

        UserGameData UD = new UserGameData(maxScore);
        SDK.SettingData(JsonUtility.ToJson(UD));
    }
    public void ShowCommon()
    {
        SDK.ShowCommonAdvertisment();
    }
    public void ShowReward(PlayerStats _playerStats)
    {
        playerStats = _playerStats;

        SDK.ShowRewardAdvertisment();
    }
    private IEnumerator ShowLeaderboard()
    {
        LeaderboardEntries.SetActive(true);
        yield return new WaitForSeconds(10);
        LeaderboardEntries.SetActive(false);
    }

    public void GetLeaderBoardEntries() => SDK.getLeaderEntries();
    public void SetLeaderBoard() => SDK.setLeaderScore(maxScore);
    
    public void AddCoin(int chg)
    {
        coins += chg;
        UpdateCoinsText();
    }
    private void OfflineMaxScore()
    {
        if (offline)
        {
            maxScore = coins;
            NameText.text = $"MAX: {maxScore}";
            PlayerPrefs.SetInt("MAX", maxScore);
            PlayerPrefs.Save();
        }
    }
    private void SettingData()
    {
        maxScore = SDK.GetUserGameData.Coin;
        MaxScoreText.text = $"MAX: {maxScore}";
    }
    private void SettingMame()
    {
        if (SDK.GetUserData.Name != "")
        {
            NameText.text = SDK.GetUserData.Name;
            GetData();
        }
        else
        {
            maxScore = PlayerPrefs.GetInt("MAX");
            NameText.text = $"MAX: {maxScore}";
            offline = true;
        }
    }
    private void Rewarded()
    {
        StartCoroutine(playerStats.Continue());
    }
    
    private void AddEntri(string _json)
    {
        ClearEntri();
        var json = JSON.Parse(_json);
        var _count = (int)json["entries"].Count;
        //string url = "https://api.icons8.com/download/52b8de2a7f8ac69966499fad5fd564202f9b130f/iOS7/PNG/512/Logos/desura_filled-512.png";

        if (_count > 10)
            _count = 10;

        for (int i = 0; i < _count; i++)
        {
            var _entries = Instantiate(_fab, SV_content);
            //var raw = _entries.transform.GetChild(0).GetComponent<RawImage>();
            //StartCoroutine(LoadIMG(url, raw));

            var name = json["entries"][i]["player"]["publicName"];
            var score = json["entries"][i]["score"];

            _entries.transform.GetChild(0).GetComponent<Text>().text = name;
            _entries.transform.GetChild(1).GetComponent<Text>().text = score.ToString();

            /*if (NameText.text == name && maxScore < score)
                maxScore = score;*/
        }

        StartCoroutine(ShowLeaderboard());
    }
    private void ClearEntri()
    {
        if (SV_content.childCount > 0)
            foreach (Transform child in SV_content)
                Destroy(child.gameObject);
    }
    /*
    private IEnumerator LoadIMG(string _url,RawImage _img)
    {
        WWW www = new WWW(_url);
        yield return www;
        _img.texture = www.texture;
    }*/
}
