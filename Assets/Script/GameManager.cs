using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverText;
    public static Vector3 lastCheckpointPosition;
    //[SerializeField] AudioClip gameOverSE;
    AudioSource audioSource;
    private bool isReset = false;
    public static bool wJumpFlg;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReset && Input.anyKeyDown) 
        {
            Restart();
        }
    }

    public void GameOver() 
    {
        GameOverText.SetActive(true);
        //audioSource.PlayOneShot(gameOverSE);
        isReset = true;
    }

    public void GameClear() 
    {
        SceneManager.LoadScene("EndingScene");
    }

    void Restart() 
    {
        Debug.Log(lastCheckpointPosition);
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    public void DisableAudio()
    {
        if (audioSource != null)
        {
            audioSource.enabled = false;
        }
    }
}
