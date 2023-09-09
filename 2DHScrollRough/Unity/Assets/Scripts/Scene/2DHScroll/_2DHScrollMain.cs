using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class _2DHScrollMain : MonoBehaviour
{
    [SerializeField] private Stage sampleStage;
    [SerializeField] private Stage stage1;
    [SerializeField] private Stage stage2;


    public Stage.Result _Result { get { return current!=null? current._Result:Stage.Result.Dead; } }
    public int Life { get { return current!=null? current.Life:0; } }

    private Stage current;


    public System.Action onMainEnded;
    public System.Action onLifeChanged;


    // Start is called before the first frame update
    public void ManualStart()
    {
        sampleStage.gameObject.SetActive(false);
        stage1.gameObject.SetActive(false);
        stage2.gameObject.SetActive(false);

        sampleStage.onLifeChanged = ()=>{onLifeChanged?.Invoke();};
        stage1.onLifeChanged = ()=>{onLifeChanged?.Invoke();};
        stage2.onLifeChanged = ()=>{onLifeChanged?.Invoke();};

        sampleStage.onStageEnded = ResolveEndStage;
        stage1.onStageEnded = ResolveEndStage;
        stage2.onStageEnded = ResolveEndStage;

        StartStage(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndMain()
    {
        onMainEnded?.Invoke();
    }

    private void StartStage(int stage)
    {
        if(true)
        {
            current = sampleStage;
            current.iStageData = stage;
            string streamingAssetsPath = Application.streamingAssetsPath;
            if(!System.IO.Directory.Exists(streamingAssetsPath))
            {
                System.IO.Directory.CreateDirectory(streamingAssetsPath);
            }

            string folderPath = streamingAssetsPath + "/Stage";
            if(!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            string stageDataPath = folderPath +"/StageData_"+stage.ToString("D4")+".json";
            current.objectDatas = JsonUtility.FromJson<Stage.ObjectDatas>(System.IO.File.ReadAllText(stageDataPath));
        }
        else
        {
            switch(stage)
            {
                case 1:
                    current = sampleStage;
                    current.iStageData = 1;
                    current.objectDatas = JsonUtility.FromJson<Stage.ObjectDatas>(
                        "{\"objectDatas\":[{\"type\":50,\"anchoredPosition\":{\"x\":462.79998779296877,\"y\":452.0252990722656},\"sizeDelta\":{\"x\":90.0,\"y\":136.0}},{\"type\":60,\"anchoredPosition\":{\"x\":-198.30078125,\"y\":356.49993896484377},\"sizeDelta\":{\"x\":384.0,\"y\":544.0}},{\"type\":200,\"anchoredPosition\":{\"x\":-0.0001220703125,\"y\":20.0},\"sizeDelta\":{\"x\":3734,\"y\":1000.0}},{\"type\":100,\"anchoredPosition\":{\"x\":83.0,\"y\":-44.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":475.3885498046875,\"y\":22.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":720.988525390625,\"y\":30.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":781.988525390625,\"y\":42.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":840.988525390625,\"y\":49.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":812.0,\"y\":-44.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1038.0,\"y\":59.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":702.988525390625,\"y\":491.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1261.988525390625,\"y\":596.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1481.988525390625,\"y\":372.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1781.0,\"y\":-121.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2004.0,\"y\":-121.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2248.0,\"y\":77.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2515.0,\"y\":274.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2744.000244140625,\"y\":-121.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2988.0,\"y\":77.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":3255.000244140625,\"y\":274.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1038.0,\"y\":59.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":3539.988525390625,\"y\":49.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}}]}"
                        //"{\"objectDatas\":[" +
                        //"   {\"type\":50,\"anchoredPosition\":{\"x\":462.79998779296877,\"y\":452.0252990722656},\"sizeDelta\":{\"x\":90.0,\"y\":136.0}},{\"type\":60,\"anchoredPosition\":{\"x\":-198.30078125,\"y\":356.49993896484377},\"sizeDelta\":{\"x\":384.0,\"y\":544.0}},{\"type\":200,\"anchoredPosition\":{\"x\":-0.0001220703125,\"y\":20.0},\"sizeDelta\":{\"x\":0.0,\"y\":1000.0}}" +
                        //"]}"
                        );
                    break;
                case 2:
                //    current = stage1;
                //    break;
                //case 3:
                //    current = stage2;
                //    break;
                    current = sampleStage;
                    current.iStageData = 2;
                    current.objectDatas = JsonUtility.FromJson<Stage.ObjectDatas>(
                        "{\"objectDatas\":[{\"type\":50,\"anchoredPosition\":{\"x\":462.79998779296877,\"y\":452.0252990722656},\"sizeDelta\":{\"x\":90.0,\"y\":136.0}},{\"type\":60,\"anchoredPosition\":{\"x\":-198.30078125,\"y\":356.49993896484377},\"sizeDelta\":{\"x\":384.0,\"y\":544.0}},{\"type\":200,\"anchoredPosition\":{\"x\":-0.00048828125,\"y\":20.0},\"sizeDelta\":{\"x\":3734.0,\"y\":1000.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1713.0,\"y\":492.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":167.0,\"y\":282.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":475.3885498046875,\"y\":22.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":3185.0,\"y\":569.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":3146.0,\"y\":701.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1006.0,\"y\":49.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1497.0,\"y\":-204.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":830.0,\"y\":-10.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":536.0,\"y\":530.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1114.0,\"y\":611.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1606.0,\"y\":435.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1886.0,\"y\":-195.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2082.0,\"y\":719.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2248.0,\"y\":56.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2515.0,\"y\":768.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2521.0,\"y\":-121.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":2748.0,\"y\":123.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":3128.0,\"y\":-138.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":1150.0,\"y\":41.0},\"sizeDelta\":{\"x\":100.0,\"y\":500.0}},{\"type\":100,\"anchoredPosition\":{\"x\":3539.988525390625,\"y\":49.0},\"sizeDelta\":{\"x\":380.777099609375,\"y\":100.0}}]}"
                        //"{\"objectDatas\":[" +
                        //"{\"type\":50,\"anchoredPosition\":{\"x\":462.79998779296877,\"y\":452.0252990722656},\"sizeDelta\":{\"x\":90.0,\"y\":136.0}},{\"type\":60,\"anchoredPosition\":{\"x\":-198.30078125,\"y\":356.49993896484377},\"sizeDelta\":{\"x\":384.0,\"y\":544.0}},{\"type\":200,\"anchoredPosition\":{\"x\":-0.00048828125,\"y\":20.0},\"sizeDelta\":{\"x\":3734.0,\"y\":1000.0}}" +
                        //"]}"
                        );
                    break;
            }
        }

        current.ManualStart();
    }

    private void ResolveEndStage()
    {
        //if(current==sampleStage)
        //{
        //    if(_Result==Stage.Result.Clear)
        //    {
                if(current.iStageData<2)
                {
                    StartStage(current.iStageData+1);
                    return;
                }
        //    }
        //}
        //else if(current==stage1)
        //{
        //    if(_Result==Stage.Result.Clear)
        //    {
        //        StartStage(3);
        //        return;
        //    }
        //}
        
        EndMain();
    }
}
