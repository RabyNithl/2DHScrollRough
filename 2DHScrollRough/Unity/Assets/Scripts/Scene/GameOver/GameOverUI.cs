using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private UnityEngine.UI.Button button;


    public System.Action onTaped;


    public void ManualStart()
    {
        this.enabled = true;

        button.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(EndUI));

        bgm.Stop();
        bgm.loop = true;
        bgm.time = 0;
        bgm.Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.Z))
        {
            EndUI();
        }
    }

    public void EndUI()
    {
        if(!this.enabled)
        {
            return;
        }

        bgm.Stop();

        this.enabled = false;
        onTaped?.Invoke();
    }
}
