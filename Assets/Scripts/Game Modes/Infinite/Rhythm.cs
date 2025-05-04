using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;

public class Rhythm : MonoBehaviour
{
    public TextMeshProUGUI combo;
    public Infinite infinite;
    public AFK afk;
    public Box box;
    public SpriteRenderer _spriteRender;
    public Animator animator;
    public KeyCode Front = KeyCode.UpArrow;
    public KeyCode Back = KeyCode.DownArrow;
    public KeyCode Left = KeyCode.LeftArrow;
    public KeyCode Right = KeyCode.RightArrow;
    public bool inverse = false;
    public bool canAttack = true;
    public bool frontAttack = false;
    public bool backAttack = false;
    public bool leftAttack = false;
    public bool rightAttack = false;
    public float delayToCancel = 0.15f;
    public float delayToReset = 0.18f;
    public float delayToPosition = 0.3f;
    public float delayToIdle = 0.5f;
    public float delayToAFK = 3f;
    public float tick = 0;
    public int charge = 0;
    public int hit;
    public  Vector3 initialPosition;
    void Start()
    {
        if(_spriteRender == null) _spriteRender = GetComponentInChildren<SpriteRenderer>();
        if(animator == null) animator = GetComponentInChildren<Animator>();
        if(afk == null) afk = GetComponent<AFK>();
        if(box == null) box = FindAnyObjectByType<Box>();
        if(infinite == null) infinite = FindAnyObjectByType<Infinite>();
        initialPosition = gameObject.transform.position;
        _spriteRender.color = new Color32(40,40,40,255);
    }
    void Update()
    {
        Slashes();
        Stage();
        //LostCharge();
    }
    #region ATTACK
    private void Slashes()
    {
        if(Input.GetKeyDown(Front) && canAttack)
        {
            transform.position = new Vector3(0,0.3f,0);
            if(inverse == false)
            {
                _spriteRender.flipX = false;
                animator.SetTrigger("front");
                inverse = true;
            }
            else
            {
                _spriteRender.flipX = true;
                animator.SetTrigger("front");
                inverse = false;
            }
            hit = 0;
            CleanLast();
            frontAttack = true;
            EndAttack();
            box.GetHit();
        }
        if(Input.GetKeyDown(Back) && canAttack)
        {
            transform.position = new Vector3(0,-0.3f,0);
            if(inverse == false)
            {
                animator.SetBool("back", true);
                inverse = true;
            }
            else
            {
                _spriteRender.flipX = true;
                animator.SetBool("back", true);
                inverse = false;
            }
            hit = 1;
            CleanLast();
            backAttack = true;
            EndAttack();
            box.GetHit();
        }
        if(Input.GetKeyDown(Left) && canAttack)
        {
            transform.position = new Vector3(-0.3f,0,0);
            if(inverse == false)
            {
                _spriteRender.flipX = true;
                animator.SetBool("side", true);
                inverse = true;
            }
            else
            {
                _spriteRender.flipX = true;
                _spriteRender.flipY = true;
                animator.SetBool("side", true);
                inverse = false;
            }
            hit = 2;
            CleanLast();
            leftAttack = true;
            EndAttack();
            box.GetHit();
        }
        if(Input.GetKeyDown(Right) && canAttack)
        {
            transform.position = new Vector3(0.3f,0,0);
            if(inverse == false)
            {
                animator.SetBool("side", true);
                inverse = true;
            }
            else
            {
                _spriteRender.flipY = true;
                animator.SetBool("side", true);
                inverse = false;
            }
            hit = 3;
            CleanLast();
            rightAttack = true;
            EndAttack();
            box.GetHit();
        }
    }
    private void CleanLast()
    {
        frontAttack = false;
        backAttack = false;
        leftAttack = false;
        rightAttack = false;
    }
    private void EndAttack()
    {
        infinite.GetScore();
        afk.ResetAFKTimer();
        canAttack = false;
        Invoke(nameof(EndSlash), delayToCancel);
        Invoke(nameof(ResetAttack), delayToReset);
    }
    private void EndSlash()
    {
        _spriteRender.flipY = false;
        _spriteRender.flipX = false;
    }
    private void ResetAttack()
    {
        canAttack = true;
        box.CleanBools();
    }
    #endregion
    #region STAGES
    private void Stage()
    {
        if(charge <= 0)
        {
            _spriteRender.color = new Color32(40,40,40,255);
            box.stageBonus = 1;
            combo.text = "x1".ToString();
            infinite.actualRando = infinite.Rando1;
            infinite.actualGap = infinite.gap1;
        }
        if(charge >= 5)
        {
            _spriteRender.color = new Color32(0,41,38,255);
        }
        if(charge >= 15)
        {
            _spriteRender.color = new Color32(0,87,80,255);
            box.stageBonus = 2;
            combo.text = "x2".ToString();
            infinite.actualRando = infinite.Rando2;
            infinite.actualGap = infinite.gap2;
        }
        if(charge >= 30)
        {
            _spriteRender.color = new Color32(0,180,166,255);
        }
        if(charge >= 60)
        {
            _spriteRender.color = new Color32(0,255,255,255);
            box.stageBonus = 4;
            combo.text = "x4".ToString();
            infinite.actualRando = infinite.Rando3;
            infinite.actualGap = infinite.gap3;
        }
    }                                      //This is a test function, now only avalible in Rhythm mode.
    //private void LostCharge()
    //{
        //if(charge > 0)
        //{
            //tick += Time.deltaTime;
            //if(tick >= 1f)
            //{
                //charge -= afk.chargeLost;
                //tick -= 1;
            //}
        //}
        //else if(charge == 0)
        //{
            //return;
        //}
    //}
    #endregion
}