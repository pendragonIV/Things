using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class HideTileMap : MonoBehaviour
{

    private TilemapRenderer tilemapRenderer;
    private TilemapCollider2D tilemapCollider2D;
    private void Awake()
    {
        tilemapRenderer = gameObject.GetComponent<TilemapRenderer>();
        tilemapCollider2D = gameObject.GetComponent<TilemapCollider2D>();

    }
    

    void Update()
    {
        
        if(tilemapRenderer != null)
        {
            if (SceneManager.GetActiveScene().name == "House")
            {
                tilemapRenderer.enabled = false;
                if(tilemapCollider2D != null)
                {
                    tilemapCollider2D.enabled = false;
                }
            }
            else
            {
                tilemapRenderer.enabled = true;
                if (tilemapCollider2D != null)
                {
                    tilemapCollider2D.enabled = true;
                }
            }
        }


    }
}
