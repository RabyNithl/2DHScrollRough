using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class _2DHScroll : Scene
{
    [SerializeField] private _2DHScrollMain main;
    [SerializeField] private _2DHScrollUI ui;


    public Stage.Result _Result { get { return main._Result; } }


    public System.Action on2DHScrollEnded;


    public override void Initialize()
    {
        main.gameObject.SetActive(false);
        ui.gameObject.SetActive(false);

        main.onMainEnded = EndScene;
        main.onLifeChanged = UpdateUIItemLife;
    }

    // Start is called before the first frame update
    public override void StartScene()
    {
        main.gameObject.SetActive(true);
        ui.gameObject.SetActive(true);

        main.ManualStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void EndScene()
    {
        main.gameObject.SetActive(false);
        ui.gameObject.SetActive(false);

        on2DHScrollEnded?.Invoke();
    }

    private void UpdateUIItemLife()
    {
        ui.SetLife(main.Life);
    }
}
