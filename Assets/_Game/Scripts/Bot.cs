using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    private IState currentState;
    private Vector3 finalGoal;
    private Vector3 goal;

    private NavMeshAgent agent;
    private Platform currentPlatform;
    private Map currentMap;

    private int savingIndex, savingCount;
    private float[] dis = new float[41];

    private List<Vector3> targetBrick = new List<Vector3>();

    private void Awake()
    {
        trans = this.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        rd = GetComponent<Renderer>();
        agent.speed = speed;
        ChangeState(new IdleState());
    }
    private void Update()
    {
        //if(active)
        {
            if(Vector3.Distance(trans.position, goal) < 1f)
            {
                targetBrick[savingIndex] += new Vector3(999f, 999f, 999f);
                StartCoroutine(ReAppear(3f));
            }
            //len cau & xuong cau
            if (count >= 15 || count == 0)
            {
                savingCount = count;
            }
            ChangeState(new RunState());
        }
        currentState.OnExecute(this);
    }
    public void OnInit(ColorEnum color, Map map, Vector3 finalGoal)
    {
        setColor(color);
        currentMap = map;
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
        Stair stair = Dictionary.instance.vachamStair(item);
        Debug.Log("up stair");
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
    IEnumerator ReAppear(float time)
    {
        yield return new WaitForSeconds(time);
        targetBrick[savingIndex] -= new Vector3(999f, 999f, 999f);
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
        if (other.gameObject.CompareTag(Constants.tagSubGoal))
        {
            // chuyen platform
            for (int i = 0; i < currentPlatform.getTarget().Count; i++)
            {
                Brick item = currentPlatform.getTarget()[i];
                if (item.color == color)
                {
                    targetBrick.Add(currentPlatform.getTarget()[i].transform.position);
                }
            }
        }
        if (other.gameObject.CompareTag(Constants.tagFinalGoal))
        {
            currentState.OnExit(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.tagSubGoal))
        {
            savingCount = 0;
            ChangeState(new RunState());
            other.GetComponent<BoxCollider>().isTrigger = false;
            targetBrick.Clear();
            for (int i = 0; i < currentPlatform.getTarget().Count; i++)
            {
                Brick item = currentPlatform.getTarget()[i];
                if (item.color == color)
                {
                    targetBrick.Add(currentPlatform.getTarget()[i].transform.position);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if(collision.gameObject.CompareTag(Constants.tagPlatform1))
        {
            Debug.Log("P1");
            currentPlatform = currentMap.getPlatform()[0];
        }
        else if (collision.gameObject.CompareTag(Constants.tagPlatform2))
        {
            Debug.Log("P2");
            currentPlatform = currentMap.getPlatform()[1];
        }

    }
}
