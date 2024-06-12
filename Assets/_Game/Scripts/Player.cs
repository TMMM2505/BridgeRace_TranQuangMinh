using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Joystick js;
    void Start()
    {
        trans = this.transform;
        rd = GetComponent<Renderer>();
    }

    public void OnInit(Vector3 startPoint, Joystick js, ColorEnum color)
    {
        setColor(color);
        transform.position = startPoint;
        if(characterBricks.Count > 0)
        {
            for(int i = 0; i < characterBricks.Count; i++)
            {
                if (characterBricks[i] != null)
                {
                    Destroy(characterBricks[i].gameObject);
                }
            }
        }
        count = 0;
        this.js = js;
    }

    void Update()
    {
        if(active)
        {
            speed = 750;
            Move();
        }
        else ChangeAnim("idle");
    }

    private void Move()
    {
        float x = js.Horizontal;
        float z = js.Vertical;
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            rb.velocity = new Vector3(x, 0, z).normalized * speed * Time.deltaTime;
            trans.forward = rb.velocity;
            ChangeAnim("run");
            CheckGround();
        }
        else ChangeAnim("idle");
    }
    public override void OnBridge(Collider item)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + ((trans.GetComponent<Rigidbody>().velocity.z > 0) ? new Vector3(0, 0, 1f) : new Vector3(0, 0, -1f)), transform.TransformDirection(Vector3.down), out hit, 10f);
        Stair stair = CacheDictionary.instance.vachamStair(item);
        if(stair != null)
        {
            if (hit.collider.GetComponent<MeshRenderer>().material.color != rd.material.color)
            {
                if (count > 0)
                {
                    speed = 800;
                    stair.ChangeColor(rd.material);
                    Destroy(trans.GetChild(trans.childCount - 1).gameObject);
                    count--;
                }
                else if (hit.transform.GetComponent<MeshRenderer>().material != rd.material)
                {
                    transform.position -= new Vector3(0, 0, 0.3f);
                }
                else speed = 750;
            }
        }
    }
    private void CheckGround()
    {
        RaycastHit hit;
        Physics.Raycast(trans.position, Vector3.down, out hit, 5f);
        if(!hit.collider.gameObject.CompareTag(Constants.tagBrick))
            trans.position = hit.point + new Vector3(0, 1f, 0);
    }
    public override void SetActive(bool active)
    {
        this.active = active;
    }
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag(Constants.tagDoor))
        {
            if (rb.velocity.z < 0f)
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.tagDoor))
        {
            if (rb.velocity.z > 0f)
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag(Constants.tagDoor))
        {
            Debug.Log("va cham");
            if (rb.velocity.z >= 0f)
            {
                Debug.Log("pass Coli");
                collision.collider.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }
}
