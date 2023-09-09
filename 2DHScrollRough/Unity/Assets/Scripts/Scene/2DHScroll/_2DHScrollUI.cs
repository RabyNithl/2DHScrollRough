using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _2DHScrollUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text life;


    public void SetLife(int life)
    {
        this.life.text = "Life : "+life;
    }
}
