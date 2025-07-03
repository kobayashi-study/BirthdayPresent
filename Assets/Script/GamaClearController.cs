using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaClearController : MonoBehaviour
{
    [SerializeField] private AudioClip stairSE;
    [SerializeField] private GameObject blackoutPanel;
    [SerializeField] private string endingSceneName = "EndingScene";
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Vector3 respawnPosition;

    private AudioSource audioSource;
    private PlayerManager playerManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerManager = collision.gameObject.GetComponent<PlayerManager>();
            GameManager.lastCheckpointPosition = respawnPosition;
            StartCoroutine(MoveSequence());
        }
    }

    private IEnumerator MoveSequence()
    {
        if (playerManager != null)
        {
            playerManager.LockInput(); // �v���C���[���ɑ����~���\�b�h���K�v
        }

        yield return new WaitForSeconds(1f);

        if (playerManager != null)
        {
            playerManager.HideSprite(); // �v���C���[���ɔ�\�����\�b�h���K�v
        }

        if (audioSource != null && stairSE != null)
        {
            audioSource.PlayOneShot(stairSE);
        }

        yield return new WaitForSeconds(2f);

        if (blackoutPanel != null)
        {
            blackoutPanel.SetActive(true);
        }

        if (gameManager != null)
        {
            gameManager.DisableAudio();
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(endingSceneName);
    }
}
