using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum State
    {
        Ready,
        Play
    }

    State state;

    public GameObject player;
    public GameObject enemyGenerator;
    public GameObject ClearMessage;

    public GameObject[] Panels;//firstP,startP,endP
    public Text[] texts;//Message,Count

    const int ClearScore = 100;//生成するenemyの最大数(この数を倒したらクリア)
    int score;

    string currentState;

    // Start is called before the first frame update
    void Start()
    {
        Ready();
    }

    // Update is called once per frame
    void Update()
    {
        //スコア取得
        score = enemyGenerator.GetComponent<EnemyGenerator>().GetAttackEnemyCount();
        //ハイスコア更新
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    void LateUpdate()
    {
        //State切り替え
        switch (state)
        {
            case State.Ready:
                if (currentState == "GameStart")
                {
                    GameStart();
                }
                break;

            case State.Play:
                if(player.GetComponent<PlayerController>().GetIsStan() == true)
                {
                    currentState = "GameOver";
                    GameOver();
                }
                else if (score >= ClearScore)
                {
                    currentState = "GameClear";
                    GameClear();
                }
                break;
        }
    }

    void Ready()
    {
        state = State.Ready;
        Panels[0].SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponentInChildren<Shooter>().enabled = false;
        enemyGenerator.SetActive(false);
    }

    void GameStart()
    {
        state = State.Play;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponentInChildren<Shooter>().enabled = true;
        enemyGenerator.SetActive(true);
    }

    void GameOver()
    {
        ClearMessage.SetActive(false);
        Panels[2].SetActive(true);
        Panels[2].GetComponent<Image>().color = Color.red;
        texts[0].text = "GAME OVER...";
        texts[1].text = "倒した数 : " + score + "体\n< ハイスコア : " + PlayerPrefs.GetInt("HighScore") + "体 >\n"; 
    }

    void GameClear()
    {
        ClearMessage.SetActive(true);
        Panels[2].SetActive(true);
        Panels[2].GetComponent<Image>().color = Color.white;
        texts[0].text = "GAME CLEAR!";
        texts[1].text = "倒した数 : " + score + "体\n< ハイスコア : " + PlayerPrefs.GetInt("HighScore") + "体 >\n";
    }

    public int GetClearScore()
    {
        return ClearScore;
    }

    public void CurrentState(string message)
    {
        this.currentState = message;
    }

    public string GetCurrentState()
    {
        return this.currentState;
    }
}
