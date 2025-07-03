using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject tilemap;
    [SerializeField] private GameManager gameManager;


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerManager player = collider.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.audioSource.PlayOneShot(player.itemSE);
                player.changeJumpFlg();
                GameManager.wJumpFlg = true;
                Debug.Log("“ñ’iƒWƒƒƒ“ƒv");
            }
            tilemap.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
