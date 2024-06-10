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

    private void Awake()
    {
        trans = this.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        if(active)
        {
            rd = GetComponent<Renderer>();
            agent.speed = speed;
            ChangeState(new RunState());
            //FindPlatform();
        }
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
            if (count >= 15 || count == 0)
            {
                savingCount = count;
                ChangeState(new RunState());
            }
            currentState.OnExecute(this);
        }
        Debug.Log(count);
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

/*    private void FindPlatform()
    {
        RaycastHit hit;
        Physics.Raycast(trans.position, Vector3.down, out hit, platformLayer);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag(Constants.tagPlatform1))
            {
                Debug.Log("pf1");
                currentPlatform = currentMap.getPlatform()[0];
            }
            else if (hit.collider.gameObject.CompareTag(Constants.tagPlatform2))
            {
                Debug.Log("pf2");
                currentPlatform = currentMap.getPlatform()[1];
            }
        }
        else
        {
            Debug.Log("null");
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Brick brick = Dictionary.instance.vachamBrick(other);

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
    }
    public void SetActive(bool active)
    {
        this.active = active;
        ChangeAnim("Bidle");
    }
}
