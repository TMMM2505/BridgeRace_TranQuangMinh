using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    [SerializeField] private LayerMask platformLayer;
    private IState currentState;
    private Vector3 finalGoal;
    private Vector3 goal;

    private NavMeshAgent agent;
    private Platform currentPlatform;
    private Map currentMap;

    private int savingIndex, savingCount;
    private float[] dis = new float[41];

    private List<Vector3> targetBrick = new List<Vector3>();
    private int needAmount;

    private void Awake()
    {
        trans = this.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        rd = GetComponent<Renderer>();
        agent.speed = speed;
        ChangeState(new RunState());
        //FindPlatform();
    }
    private void Update()
    {
        if(active)
        {
            /*if(Vector3.Distance(trans.position, goal) < 0.5f)
            {
                targetBrick[savingIndex] += new Vector3(999f, 999f, 999f);
                StartCoroutine(ReAppear(3f));
            }*/
            //len cau & xuong cau
            if (count >= needAmount || count == 0)
            {
                savingCount = count;
                ChangeState(new RunState());
            }
            currentState.OnExecute(this);
        }
        else
        {
            currentState.OnExit(this);
        }
    }
    public void OnInit(ColorEnum color, Map map, Vector3 finalGoal)
    {
        setColor(color);
        currentMap = map;
        //FindPlatform();
        currentPlatform = currentMap.getPlatform()[0];

        targetBrick.Clear();
        for(int i = 0; i < currentPlatform.getTarget().Count; i++)
        {
            Brick item = currentPlatform.getTarget()[i];
            if(item.color == color)
            {
                targetBrick.Add(currentPlatform.getTarget()[i].transform.position);
            }
        }
        needAmount = UnityEngine.Random.Range(10, 15);

        this.finalGoal = finalGoal;
    }
    public void goToFinish()
    {
        goal = finalGoal;
    }
    public int getCount()
    {
        return savingCount;
    }

    public override void OnBridge(Collider item)
    {
        Stair stair = CacheDictionary.instance.vachamStair(item);
        if (stair != null)
        {
            if(stair.GetMaterial().color != rd.material.color)
            {
                if(count > 0)
                {
                    agent.speed = 10;
                    stair.ChangeColor(rd.material);
                    Destroy(trans.GetChild(trans.childCount - 1).gameObject);
                    count--;
                }
                else
                {
                    agent.speed = 0;
                    FindMinTarget();
                    needAmount = UnityEngine.Random.Range(10, 15);
                }
            }
            else
            {
                agent.speed = 10;
            }
        }
    }
    public void FindMinTarget()
    {
        int index = 0;
        for (int i = 0; i < targetBrick.Count; i++)
        {
            dis[i] = Vector3.Distance(trans.position, targetBrick[i]);
        }
        float min = dis.Min();
        for (int i = 0; i < targetBrick.Count; i++)
        {
            if (min == dis[i]) index = i;
        }
        goal = targetBrick[index];
        savingIndex = index;
    }
    public void Move()
    {
        agent.destination = goal;
        agent.speed = 5;
        ChangeAnim("Brun");
    }

    public void setGoal(Vector3 goal)
    {
        this.goal = goal;
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnStart(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Brick brick = CacheDictionary.instance.vachamBrick(other);

        if(brick != null)
        {
            if (brick.color == color)
            {
                targetBrick[savingIndex] += new Vector3(999f, 999f, 999f);
                StartCoroutine(ReAppear(3f, savingIndex));
                FindMinTarget();
            }
        }
        
        if (other.gameObject.CompareTag(Constants.tagFinalGoal))
        {
            ChangeState(new IdleState());
        }

        Vector3 velocity = agent.velocity;
        Vector3 movementDirectionNormalized = velocity.normalized;
        float dotProduct = Vector3.Dot(movementDirectionNormalized, transform.forward);
        if (other.gameObject.CompareTag(Constants.tagDoor))
        {
            if (dotProduct < 0f)
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

    IEnumerator ReAppear(float time, int index)
    {
        yield return new WaitForSeconds(time);
        targetBrick[index] -= new Vector3(999f, 999f, 999f);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.tagDoor) && touchDoor)
        {
            touchDoor = false;
            // chuyen platform
            targetBrick.Clear();
            for (int i = 0; i < currentPlatform.getTarget().Count; i++)
            {
                Brick item = currentPlatform.getTarget()[i];
                if (item.color == color)
                {
                    targetBrick.Add(currentPlatform.getTarget()[i].transform.position);
                }
            }
            savingCount = 0;
            ChangeState(new RunState());
            other.GetComponent<BoxCollider>().isTrigger = false;
        }

        Vector3 velocity = agent.velocity;
        Vector3 movementDirectionNormalized = velocity.normalized;
        float dotProduct = Vector3.Dot(movementDirectionNormalized, transform.forward);
        if (other.gameObject.CompareTag(Constants.tagDoor))
        {
            if (dotProduct > 0f)
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag(Constants.tagPlatform1))
        {
            currentPlatform = currentMap.getPlatform()[0];
        }
        else if (collision.gameObject.CompareTag(Constants.tagPlatform2))
        {
            currentPlatform = currentMap.getPlatform()[1];
        }

        Vector3 velocity = agent.velocity;
        Vector3 movementDirectionNormalized = velocity.normalized;
        float dotProduct = Vector3.Dot(movementDirectionNormalized, transform.forward);
        if (collision.gameObject.CompareTag(Constants.tagDoor))
        {
            if (dotProduct > 0f)
            {
                collision.collider.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }
    public override void SetActive(bool active)
    {
        this.active = active;
        ChangeAnim("Bidle");
    }
    public int getNeedAmount()
    {
        return needAmount;
    }
}
