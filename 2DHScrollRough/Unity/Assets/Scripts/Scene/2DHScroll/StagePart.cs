using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePart : MonoBehaviour
{
    [SerializeField] private Vector2 positionRatio;

    private UnityEngine.UI.ScrollRect scroll;
    private Player player;


    public Vector2 Hope = Vector2.zero;
    public Vector2? Suggestion = null;
    public Vector2 Diff = Vector2.zero;

    public bool OnTrigger { get; private set; } = false;


    // Start is called before the first frame update
    public void Initialize(UnityEngine.UI.ScrollRect in_scroll, Player in_player)
    {
        scroll = in_scroll;
        player = in_player;
        Suggestion = null;
        OnTrigger = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        OnTrigger = false;
    }

    public void Calc()
    {
        if(scroll==null||player==null)
        {
            return;
        }

        //if(collision.gameObject!=player.gameObject)
        //{
        //    return;
        //}

        Vector3 cameraTargetPosition = Vector3.zero;
        //if (player != null)
        //{
        //    cameraTargetPosition = player.transform.position;
        //}
        //else if (samplePlayer != null)
        //{
        //    cameraTargetPosition = samplePlayer.transform.position;
        //}
        //camera.transform.position = cameraTargetPosition;
        //
        //cameraTargetPosition = (camera.transform as RectTransform).anchoredPosition3D;
        RectTransform rectTrans = this.transform as RectTransform;
        //cameraTargetPosition = new Vector3(Mathf.Clamp(cameraTargetPosition.x, cameraRange.rect.left + (Screen.width / 2), cameraRange.rect.right - (Screen.width / 2)), Mathf.Clamp(cameraTargetPosition.y, cameraRange.rect.yMin + (Screen.height / 2), cameraRange.rect.yMax - (Screen.height / 2)), 0);
        //(camera.transform as RectTransform).anchoredPosition3D = cameraTargetPosition + new Vector3(0, 0, -10);

        cameraTargetPosition = (player.transform as RectTransform).anchoredPosition;
        //cameraTargetPosition += new Vector3(cameraTargetPosition.x+(Screen.width/2),cameraTargetPosition.y+(Screen.height/2),0);
        //scroll.verticalNormalizedPosition = (cameraTargetPosition.y/cameraRange.rect.size.y);
        //scroll.horizontalNormalizedPosition = ((cameraTargetPosition.x-(Screen.width/2))/(cameraRange.rect.size.x-(Screen.width)));
        RectTransform scrollRectTrans = scroll.transform as RectTransform;

        Vector2 signedPositionRatio = (positionRatio - (Vector2.one/2))*-1;
        
        cameraTargetPosition = (player.transform as RectTransform).anchoredPosition+(new Vector2(scrollRectTrans.rect.width*signedPositionRatio.x,scrollRectTrans.rect.height*signedPositionRatio.y));
        Hope = new Vector2(
                -(cameraTargetPosition.x - (scrollRectTrans.rect.width  / 2))
            ,   -(cameraTargetPosition.y - (scrollRectTrans.rect.height / 2))
            );

        Suggestion
            = new Vector2(
                Mathf.Clamp(Hope.x  , (-(rectTrans.rect.xMax - (scrollRectTrans.rect.width))-rectTrans.anchoredPosition.x), -rectTrans.rect.xMin-rectTrans.anchoredPosition.x)
            ,   Mathf.Clamp(Hope.y  , (-(rectTrans.rect.yMax - (scrollRectTrans.rect.height))-rectTrans.anchoredPosition.y), -rectTrans.rect.yMin-rectTrans.anchoredPosition.y)
            );

        Diff = Hope - Suggestion.Value;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        return;

        if(scroll==null||player==null)
        {
            return;
        }

        if(collision.gameObject!=player.gameObject)
        {
            return;
        }

        Calc();

        Vector3 cameraTargetPosition = Vector3.zero;
        //if (player != null)
        //{
        //    cameraTargetPosition = player.transform.position;
        //}
        //else if (samplePlayer != null)
        //{
        //    cameraTargetPosition = samplePlayer.transform.position;
        //}
        //camera.transform.position = cameraTargetPosition;
        //
        //cameraTargetPosition = (camera.transform as RectTransform).anchoredPosition3D;
        RectTransform rectTrans = this.transform as RectTransform;
        //cameraTargetPosition = new Vector3(Mathf.Clamp(cameraTargetPosition.x, cameraRange.rect.left + (Screen.width / 2), cameraRange.rect.right - (Screen.width / 2)), Mathf.Clamp(cameraTargetPosition.y, cameraRange.rect.yMin + (Screen.height / 2), cameraRange.rect.yMax - (Screen.height / 2)), 0);
        //(camera.transform as RectTransform).anchoredPosition3D = cameraTargetPosition + new Vector3(0, 0, -10);

        cameraTargetPosition = (player.transform as RectTransform).anchoredPosition;
        //cameraTargetPosition += new Vector3(cameraTargetPosition.x+(Screen.width/2),cameraTargetPosition.y+(Screen.height/2),0);
        //scroll.verticalNormalizedPosition = (cameraTargetPosition.y/cameraRange.rect.size.y);
        //scroll.horizontalNormalizedPosition = ((cameraTargetPosition.x-(Screen.width/2))/(cameraRange.rect.size.x-(Screen.width)));
        RectTransform scrollRectTrans = scroll.transform as RectTransform;
        scroll.content.anchoredPosition
            = Suggestion.Value;
        //if (!manualScroll)
        //{
        //    debugScrollValue = scroll.horizontalNormalizedPosition;
        //}
        //else
        //{
        //    scroll.horizontalNormalizedPosition = debugScrollValue;
        //}
    }
}
