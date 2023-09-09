using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private Player player = null;


    public System.Action actOnCollisionPlayerEnter;


    void Awake()
    {
        player = null;
    }

    void Update()
    {
        if(player!=null)
        {
            if(player.IsNoCollision)
            {
                actOnCollisionPlayerEnter?.Invoke();
                player = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        Debug.Log(tag);

        if(tag=="Player")
        {
            player = collision.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        Debug.Log(tag);

        if(tag=="Player")
        {
            player = null;
        }
    }
}
