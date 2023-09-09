using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameOver : Scene
{
    [SerializeField] private GameOverUI ui;


    public System.Action onEndingEnded;


    public override void Initialize()
    {
        ui.gameObject.SetActive(false);

        ui.onTaped = EndScene;
    }

    // Start is called before the first frame update
    public override void StartScene()
    {
        ui.gameObject.SetActive(true);

        ui.ManualStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void EndScene()
    {
        ui.gameObject.SetActive(false);

        onEndingEnded?.Invoke();
    }
}
