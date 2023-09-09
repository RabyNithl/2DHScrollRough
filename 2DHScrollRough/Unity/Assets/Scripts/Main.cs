using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public enum Scene
    {
        Title       ,
        _2DHScroll  ,
        Ending      ,
        GameOver    ,
    }

    [SerializeField] private Title title;
    [SerializeField] private _2DHScroll _2DHScroll;
    [SerializeField] private Ending ending;
    [SerializeField] private GameOver gameOver;


    private global::Scene current;


    // Start is called before the first frame update
    void Start()
    {
        title.Initialize();
        _2DHScroll.Initialize();
        ending.Initialize();
        gameOver.Initialize();

        title.onTitleEnded = ResolveEndScene;
        _2DHScroll.on2DHScrollEnded = ResolveEndScene;
        ending.onEndingEnded = ResolveEndScene;
        gameOver.onEndingEnded = ResolveEndScene;

        StartScene(Scene.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartScene(Scene scene)
    {
        switch(scene)
        {
            case Scene.Title:
                current = title;
                break;
            case Scene._2DHScroll:
                current = _2DHScroll;
                break;
            case Scene.Ending:
                current = ending;
                break;
            case Scene.GameOver:
                current = gameOver;
                break;
        }
        current.StartScene();
    }

    private void ResolveEndScene()
    {
        if(current==title)
        {
            StartScene(Scene._2DHScroll);
            return;
        }
        if(current==_2DHScroll)
        {
            if(_2DHScroll._Result==Stage.Result.Clear)
            {
                StartScene(Scene.Ending);
                return;
            }
            else if(_2DHScroll._Result==Stage.Result.Dead)
            {
                StartScene(Scene.GameOver);
                return;
            }
        }
        StartScene(Scene.Title);
    }
}