using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public System.Action actOnCollisionPlayerEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        Debug.Log(tag);

        if(tag=="Player")
        {
            actOnCollisionPlayerEnter?.Invoke();
        }
    }
}
