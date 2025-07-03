using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] BlinkerController blinkerController;
    private AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            AudioSource.Play();
            blinkerController.SetInterval(0.05f,0.05f);
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene() 
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("FieldScene");
    }
}
