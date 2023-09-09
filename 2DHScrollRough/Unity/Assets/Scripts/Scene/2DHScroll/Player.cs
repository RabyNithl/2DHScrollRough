using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class _2DHScrollObject : MonoBehaviour
{
    public enum CollisionDirection
    {
        Up      ,
        Down    ,
        Left    ,
        Right   ,
    }
}

public class Player : _2DHScrollObject
{
    [SerializeField] private OneShotSound sampleJumpSE;


    private new Rigidbody2D rigidbody2D;
    private RectTransform rectTransform;

    private Dictionary<GameObject,CollisionDirection> collisions = new Dictionary<GameObject,CollisionDirection>();
    private List<Vector2> positionLog = new List<Vector2>();
    private Vector2 lastCollisionOffsetMin = Vector2.zero;
    private Vector2 lastCollisionOffsetMax = Vector2.zero;

    float power = 0f;


    public bool IsNoCollision => collisions.Count==0;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        rectTransform = this.transform as RectTransform;

        collisions = new Dictionary<GameObject,CollisionDirection>();

        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        positionLog.Add(rectTransform.anchoredPosition);
        positionLog.Add(rectTransform.anchoredPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)&&(collisions.Where(e=>e.Key.tag=="Ground"&&e.Value==CollisionDirection.Left).Count()<=0))
        {
            rectTransform.anchoredPosition += new Vector2(-400,0)*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow)&&(collisions.Where(e=>e.Key.tag=="Ground"&&e.Value==CollisionDirection.Right).Count()<=0))
        {
            rectTransform.anchoredPosition += new Vector2(400,0)*Time.deltaTime;
        }

        if((0<collisions.Where(e=>e.Key.tag=="Ground"&&e.Value==CollisionDirection.Down).Count()))
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                //rigidbody2D.AddForce(new Vector2(0,1000000));
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
                power = 300000.0f/300.0f;
                System.Action act = null;
                act = ()=>
                    {
                        if(Input.GetKey(KeyCode.Z)&&(collisions.Where(e=>e.Key.tag=="Ground"&&e.Value==CollisionDirection.Up).Count()<=0)&&(0<power))
                        {
                            power += (3000.0f*Time.deltaTime);
                            TimerAction.TimerSet(UnityEngine.Time.deltaTime,act);
                        }
                        else
                        {
                            //rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                        }
                    };
                TimerAction.TimerSet(UnityEngine.Time.deltaTime,act);
                Instantiate(sampleJumpSE,this.transform).gameObject.SetActive(true);
            }            
        }
        else
        {
            power-=(4000.0f*Time.deltaTime);
        }
        rectTransform.anchoredPosition += new Vector2(0,power)*Time.deltaTime;


        collisions.Select(
            e=>
            {
                CollisionDirection direction = e.Value;
                RectTransform rtCollisiion = e.Key.transform as RectTransform;
                lastCollisionOffsetMin = rectTransform.offsetMin;
                lastCollisionOffsetMax = rectTransform.offsetMax;
                float downDist = lastCollisionOffsetMin.y - rtCollisiion.offsetMax.y;
                float upDist = rtCollisiion.offsetMin.y - lastCollisionOffsetMax.y;
                float leftDist = lastCollisionOffsetMin.x - rtCollisiion.offsetMax.x;
                float rightDist = rtCollisiion.offsetMin.x - lastCollisionOffsetMax.x;
                float dist = downDist;

                switch (direction)
                {
                    case CollisionDirection.Up:
                        dist = upDist;
                        break;
                    case CollisionDirection.Down:
                        dist = downDist;
                        break;
                    case CollisionDirection.Left:
                        dist = leftDist;
                        break;
                    case CollisionDirection.Right:
                        dist = rightDist;
                        break;
                }
                return (direction,dist);
            }).OrderBy(e=>e.dist).Select((e,i)=>(i,e)).Where(e=>e.i==0).Select(
            e=>
            {
                CollisionDirection direction = e.e.direction;
                switch (direction)
                {
                    case CollisionDirection.Up:
                        if(0<power)
                        {
                            rectTransform.anchoredPosition += new Vector2(0,e.e.dist);
                            power = 0;
                        }
                        break;
                    case CollisionDirection.Down:
                        if(power<=0)
                        {
                            rectTransform.anchoredPosition -= new Vector2(0,e.e.dist);
                            power = 0;
                        }
                        break;
                    case CollisionDirection.Left:
                        rectTransform.anchoredPosition -= new Vector2(e.e.dist,0);
                        break;
                    case CollisionDirection.Right:
                        rectTransform.anchoredPosition += new Vector2(e.e.dist,0);
                        break;
                }
                return e;
            }).ToList();
        positionLog[1]=rectTransform.anchoredPosition;



        positionLog.RemoveAt(0);
        positionLog.Add(rectTransform.anchoredPosition);
        //Debug.Log((positionLog[0]-positionLog[1]).magnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionUpdateDirection(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionUpdateDirection(collision);
    }

    private void OnCollisionUpdateDirection(Collision2D collision)
    {
        CollisionDirection direction = CollisionDirection.Down;
        lastCollisionOffsetMin = rectTransform.offsetMin;
        lastCollisionOffsetMax = rectTransform.offsetMax;
        RectTransform rtCollisiion = collision.transform as RectTransform;
        float downDist = lastCollisionOffsetMin.y - rtCollisiion.offsetMax.y;
        float upDist = rtCollisiion.offsetMin.y - lastCollisionOffsetMax.y;
        float leftDist = lastCollisionOffsetMin.x - rtCollisiion.offsetMax.x;
        float rightDist = rtCollisiion.offsetMin.x - lastCollisionOffsetMax.x;
        float lessThanNearZero = new List<float>(){downDist,upDist,leftDist,rightDist}.Where(e=>e<=0.5f).Max();

        if(leftDist==lessThanNearZero)
        {
            direction = CollisionDirection.Left;
        }
        if(rightDist==lessThanNearZero)
        {
            direction = CollisionDirection.Right;
        }
        if(upDist==lessThanNearZero)
        {
            direction = CollisionDirection.Up;
            lessThanNearZero = upDist;
        }
        if(downDist==lessThanNearZero)
        {
            direction = CollisionDirection.Down;
            if(downDist<-10.0f)
            {
                Debug.Log("");
            }
        }

        if((lessThanNearZero<0)&&((direction==CollisionDirection.Left)||(direction==CollisionDirection.Right))&&(downDist<=0)&&(-20.0f<downDist))
        {
            direction = CollisionDirection.Down;
        }

        Vector2 logMove = positionLog[1]-positionLog[0];
        if(10.0f<logMove.magnitude)
        {
            if(Mathf.Abs(logMove.x)<Mathf.Abs(logMove.y))
            {
                if(0<=logMove.y&&direction==CollisionDirection.Down)
                {
                    direction = CollisionDirection.Up;
                }
                else if(logMove.y<0&&direction==CollisionDirection.Up)
                {
                    direction = CollisionDirection.Down;
                }
            }
            else
            {
                if(0<=logMove.x&&direction==CollisionDirection.Left)
                {
                    direction = CollisionDirection.Right;
                }
                else if(logMove.x<0&&direction==CollisionDirection.Right)
                {
                    direction = CollisionDirection.Left;
                }
            }
        }

        if(collisions.ContainsKey(collision.gameObject))
        {
            collisions[collision.gameObject] = direction;
        }
        else
        {
            collisions.Add(collision.gameObject,direction);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collisions.ContainsKey(collision.gameObject))
        {
            collisions.Remove(collision.gameObject);
        }
    }
}
