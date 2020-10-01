using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour
{
    public GameObject[] Panels;//firstP,startP,endP
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNextButton()
    {
        Panels[0].SetActive(false);
        Panels[1].SetActive(true);
    }

    public void OnClickStartButton()
    {
        gameManager.SendMessage("CurrentState", "GameStart");
        Panels[1].SetActive(false);
    }

    public void OnClickReTryButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
