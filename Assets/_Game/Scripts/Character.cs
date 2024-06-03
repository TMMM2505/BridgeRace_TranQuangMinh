using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;

    public static Character instance { get; private set; }

    protected List<CharacterBrick> characterBricks = new List<CharacterBrick>();
    protected Transform trans;
    protected Renderer rd;
    protected Vector3[] target = new Vector3[41];
    protected ColorEnum color;
    protected int count;
    protected string currentAnim;
    protected string cl;
    protected bool active;
    private void Awake()
    {
        instance = this;
    }
    public void setColor(ColorEnum chosenColor)
    {
        rd = GetComponent<Renderer>();
        rd.material = LevelManager.instance.colorData.GetColorData(chosenColor);
        color = chosenColor;
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Brick brick = Dictionary.instance.vachamBrick(other);
        if(brick != null)
        {
            if (brick.color == color)
            {
                brick.TurnOff();
                brick.ReAppear();
                count++;

                CharacterBrick characterBrick = SimplePool.Spawn<CharacterBrick>(PoolType.CharBrick, -transform.forward + new Vector3(transform.position.x, (count + 3f) * 0.2f + transform.position.y, transform.position.z), Quaternion.identity);
                characterBrick.ChangeColor(rd.material, color);
                characterBrick.OnTaken(trans);
                characterBricks.Add(characterBrick);
            }
        }

    }

    protected void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag(Constants.tagStair))
        {
            OnBridge(other.collider);
        }
    }
    public virtual void OnBridge(Collider item)
    {

    }

    public void setActive(bool active)
    {
        this.active = active;
    }    
}
