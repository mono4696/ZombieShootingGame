using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGMController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip BadBGM;
    public AudioClip GoodBGM;

    public GameManager gameManager;
    public AudioSource mainBGM;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetCurrentState() == "GameOver")
        {
            mainBGM.Stop();
            GetComponent<Camera>().depth = 1;
            audioSource.PlayOneShot(BadBGM);
        }
        else if (gameManager.GetCurrentState() == "GameClear")
        {
            mainBGM.Stop();
            GetComponent<Camera>().depth = 1;
            audioSource.PlayOneShot(GoodBGM);
        }
    }

}
