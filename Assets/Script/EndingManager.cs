using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] CaptionManager captionManager;
    [SerializeField] CameraController cameraController;
    [SerializeField] ImpulseController impulseController;
    [SerializeField] FadeController fadeController;
    [SerializeField] ShaderController shaderController;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject illust;
    [SerializeField] GameObject topBar;
    [SerializeField] GameObject endroll;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fire;
    [SerializeField] GameObject wick;
    [SerializeField] GameObject cake;
    [SerializeField] GameObject background;
    [SerializeField] AudioClip chimeSE;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip quakeSE;
    [SerializeField] AudioClip fireSE;
    [SerializeField] AudioClip switchSE;
    [SerializeField] AudioClip cakeSE;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("Ending");
    }
    
    IEnumerator Ending() 
    {
        Debug.Log("エンディング開始");
        audioSource.Stop();
        PlayerManager.isBlocked = true;
        yield return new WaitForSeconds(3f);
        audioSource.PlayOneShot(chimeSE);
        yield return StartCoroutine(captionManager.ShowCaption("祝福されし者よ"));
        yield return StartCoroutine(cameraController.MoveCamera(new Vector3(13.5f, -1f, -10),3f));        
        audioSource.PlayOneShot(chimeSE);
        yield return StartCoroutine(captionManager.ShowCaption("かの赤石を踏み抜き"));
        yield return StartCoroutine(cameraController.MoveCamera(new Vector3(13.5f, 15f, -10), 3f));
        audioSource.PlayOneShot(chimeSE);
        yield return StartCoroutine(captionManager.ShowCaption("塔の頂に火を"));
        player.transform.position = new Vector3(9.5f, -1f, 0);
        yield return StartCoroutine(cameraController.MoveCamera(new Vector3(13.5f, -1f, -10), 3f));
        yield return new WaitForSeconds(3f);
        audioSource.PlayOneShot(jumpSE);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, 10);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(switchSE);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(quakeSE);
        impulseController.Shake(9,1);
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(cameraController.MoveCamera(new Vector3(13.5f, 15f, -10), 3f));
        yield return StartCoroutine(fadeController.FadeOut(3f));
        audioSource.PlayOneShot(fireSE);
        gameManager.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(2f);
        fire.SetActive(true);
        cake.SetActive(true);
        wick.GetComponent<SpriteRenderer>().color = Color.white;
        yield return StartCoroutine(fadeController.FadeIn(3f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(cameraController.MoveCamera(new Vector3(13.5f, -106f, -10), 15f));
        yield return StartCoroutine(captionManager.ShowCaption("火によって塔の本当の姿があらわになった"));
        yield return StartCoroutine(captionManager.ShowCaption("今まで登ってきた塔の正体"));
        background.SetActive(true);
        yield return StartCoroutine(captionManager.ShowCaption("それは…"));
        StartCoroutine(shaderController.MosaicOut(1f));
        yield return StartCoroutine(cameraController.ZoomOut(150f, 1f));
        topBar.SetActive(false);
        audioSource.PlayOneShot(cakeSE);
        yield return StartCoroutine(captionManager.ShowCaption("君の誕生日ケーキだったんだ！"));
        audioSource.Play();
        yield return StartCoroutine(captionManager.ShowCaption("誕生日おめでとう！"));
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(captionManager.ExtendBar(1100,1.5f));
        yield return StartCoroutine(endroll.GetComponent<EndrollController>().Endroll());
        yield return StartCoroutine(illust.GetComponent<FadeController>().FadeOut(5f));
        shaderController.ResetMosaic();
        PlayerManager.isBlocked = false;
        Debug.Log("エンディング終了");
    }
}
