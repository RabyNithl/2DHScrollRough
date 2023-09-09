using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Title : Scene
{
    [SerializeField] private TitleUI ui;


    public System.Action onTitleEnded;


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

        onTitleEnded?.Invoke();
    }
}
