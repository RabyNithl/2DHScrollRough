using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAction : MonoBehaviour
{
    public static void TimerSet(float wait, System.Action action)
    {
        new GameObject().AddComponent<TimerAction>().SetAction(action).bomb = Time.realtimeSinceStartup+wait;
    }


    float bomb = 0;


    System.Action action;


    // Update is called once per frame
    void Update()
    {
        if(bomb<Time.realtimeSinceStartup)
        {
            action?.Invoke();
            Destroy(this.gameObject);
        }
    }

    TimerAction SetAction(System.Action action)
    {
        this.action = action;
        return this;
    }
}
