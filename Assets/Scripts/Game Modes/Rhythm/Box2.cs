using UnityEngine;
public class Box2 : MonoBehaviour
{
    private Rhth2 rhth2;
    private Abuda afk2;
    public GameObject frontBox;
    public GameObject backBox;
    public GameObject leftBox;
    public GameObject rightBox;
    public Animator animator;
    public AudioSource actualAudio;
    public int stageBonus;
    private string actualHit;
    void Start()
    {
        if(rhth2 == null) rhth2 = FindAnyObjectByType<Rhth2>();
        if(afk2 == null) afk2 = FindAnyObjectByType<Abuda>();
        stageBonus = 1;
    }
    private void CheckStage()
    {
        if(rhth2.charge == 0 || rhth2.charge <14)
        {
            actualHit = "hit1";
            afk2.chargeLost = 1;
        }
        if(rhth2.charge >= 15 && rhth2.charge <59)
        {
            actualHit = "hit2";
            afk2.chargeLost = 3;
        }
        if(rhth2.charge >= 60)
        {
            actualHit = "hit3";
            afk2.chargeLost = 12;
        }
    }
    public void CleanBools()
    {
        animator.SetBool("hit1", false);
        animator.SetBool("hit2", false);
        animator.SetBool("hit3", false);
    }
    public void GetHit()
    {
        CheckStage();
        if(rhth2.frontAttack)
        {
            animator = frontBox.GetComponentInChildren<Animator>();
            animator.SetBool(actualHit, true);
            actualAudio = frontBox.GetComponent<AudioSource>();
            actualAudio.Play();
        }
        if(rhth2.backAttack)
        {
            animator = backBox.GetComponentInChildren<Animator>();
            animator.SetBool(actualHit, true);
            actualAudio = backBox.GetComponent<AudioSource>();
            actualAudio.Play();
        }
        if(rhth2.leftAttack)
        {
            animator = leftBox.GetComponentInChildren<Animator>();
            animator.SetBool(actualHit, true);
            actualAudio = leftBox.GetComponent<AudioSource>();
            actualAudio.Play();
        }
        if(rhth2.rightAttack)
        {
            animator = rightBox.GetComponentInChildren<Animator>();
            animator.SetBool(actualHit, true);
            actualAudio = rightBox.GetComponent<AudioSource>();
            actualAudio.Play();
        }
        rhth2.charge += afk2.chargeLost;
    }
}