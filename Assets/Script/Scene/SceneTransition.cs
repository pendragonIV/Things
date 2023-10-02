using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag == "ToWorld")
        {
            SceneManager.LoadScene("World");
            GameManager.instance.player.transform.position = GameManager.instance.player.GetComponent<Player>().playerProps.worldSpawnPosition;
           
        }
        else if(this.gameObject.tag == "ToHouse")
        {
            GameManager.instance.player.GetComponent<Player>().SetWorldSpawnPosition();
            SceneManager.LoadScene("House");
            GameManager.instance.player.transform.position = GameManager.instance.player.GetComponent<Player>().playerProps.homeSpawnPosition;
            
        }
    }
}
