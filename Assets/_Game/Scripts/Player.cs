using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Joystick js;

    void Start()
    {
        trans = this.transform;
        rd = GetComponent<Renderer>();
    }

    public void OnInit()
    {
        trans.position = new Vector3(0, 0, -10f);
        for(int i = 0; i < characterBricks.Count; i++)
        {
            Destroy(characterBricks[i].gameObject);
        }
        count = 0;
    }

    void Update()
    {
        /*if (!active)
        {
            speed = 0;
        }
        else*/
        {
            speed = 650;
            Move();
        }
    }

    private void Move()
    {
        float x = js.Horizontal;
        float z = js.Vertical;
        /*float x, z;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");*/
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            rb.velocity = new Vector3(x, 0, z).normalized * speed * Time.deltaTime;
            trans.forward = rb.velocity;
            ChangeAnim("run");
        }
        else ChangeAnim("idle");
        checkGround();
    }
    public override void OnBridge(Collider item)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + ((trans.GetComponent<Rigidbody>().velocity.z > 0) ? new Vector3(0, 0, 1f) : new Vector3(0, 0, -1f)), transform.TransformDirection(Vector3.down), out hit, 10f);
        Stair stair = Dictionary.instance.vachamStair(item);
        if(stair != null)
        {
            if (hit.collider.GetComponent<MeshRenderer>().material.color != rd.material.color)
            {
                if (count > 0)
                {
                    speed = 700;
                    stair.ChangeColor(rd.material);
                    Destroy(trans.GetChild(trans.childCount - 1).gameObject);
                    count--;
                }
                else if (hit.transform.GetComponent<MeshRenderer>().material != rd.material)
                {
                    transform.position -= new Vector3(0, 0, 0.3f);
                }
                else speed = 600;
            }
            if (trans.position.z < 0)
            {
                checkGround();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.tagSubGoal))
        {
            other.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    private void checkGround()
    {
        RaycastHit hit;
        Physics.Raycast(trans.position, Vector3.down, out hit, 5f);
        trans.position = hit.point + new Vector3(0, 1f, 0);
    }

}
