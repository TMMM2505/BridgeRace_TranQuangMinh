using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : GameUnit
{
    [SerializeField] protected GameObject skin;
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
    protected bool touchDoor = true;
    private void Awake()
    {
        instance = this;
    }
    public void setColor(ColorEnum chosenColor)
    {
        rd = GetComponent<Renderer>();
        skin.GetComponent<Renderer>().material = LevelManager.instance.colorData.GetColorData(chosenColor);
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
        Brick brick = CacheDictionary.instance.vachamBrick(other);
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
        if(other.gameObject.CompareTag(Constants.tagFinalGoal))
        {
            int index = LevelManager.instance.GetIndexMap();
            Debug.Log("indexCurMap: " + index);
            if (index < 2)
            {
                if (gameObject.CompareTag(Constants.tagPlayer))
                {
                    gameManager.Ins.Victory();
                }
                else if (gameObject.CompareTag(Constants.tagBot))
                {
                    gameManager.Ins.Defeat();
                }
                LevelManager.instance.SetIndexMap(index++);
            }
            else
            {
                Time.timeScale = 0f;
                UIManager.Ins.CloseAll();
                if (gameObject.CompareTag(Constants.tagPlayer))
                {
                    gameManager.Ins.Victory();
                }
                else if (gameObject.CompareTag(Constants.tagBot))
                {
                    gameManager.Ins.Defeat();
                }
                UIManager.Ins.OpenUI<TextEG>();
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
    public virtual void OnBridge(Collider item){}
    public virtual void SetActive(bool active) { }
}
