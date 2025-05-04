using UnityEngine;
public class Box : MonoBehaviour
{
    public Infinite infinite;
    public AudioSource audioSource;
    public AudioClip miss;
    public AudioClip hit;
    private Rhythm rhythm;
    private AFK afk;
    public GameObject frontBox;
    public GameObject backBox;
    public GameObject leftBox;
    public GameObject rightBox;
    public Animator animator;
    public int stageBonus;
    private string actualHit;
    void Start()
    {
        if(infinite == null) infinite = FindAnyObjectByType<Infinite>();
        if(rhythm == null) rhythm = FindAnyObjectByType<Rhythm>();
        if(afk == null) afk = FindAnyObjectByType<AFK>();
        stageBonus = 1;
    }
    private void CheckStage()
    {
        if(rhythm.charge == 0 || rhythm.charge <14)
        {
            actualHit = "hit1";
            afk.chargeLost = 1;
        }
        if(rhythm.charge >= 15 && rhythm.charge <59)
        {
            actualHit = "hit2";
            afk.chargeLost = 3;
        }
        if(rhythm.charge >= 60)
        {
            actualHit = "hit3";
            afk.chargeLost = 12;
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
        if(rhythm.frontAttack)
        {
            animator = frontBox.GetComponentInChildren<Animator>();
            audioSource = frontBox.GetComponentInChildren<AudioSource>();
            animator.SetBool(actualHit, true);
            if(frontBox == infinite.actualBox1.gameObject)
            {
                HitSound();
            }
            else
            {
                MissSound();
                infinite.erros.value += 1;
            }
        }
        if(rhythm.backAttack)
        {
            animator = backBox.GetComponentInChildren<Animator>();
            audioSource = frontBox.GetComponentInChildren<AudioSource>();
            animator.SetBool(actualHit, true);
            if(backBox == infinite.actualBox1.gameObject)
            {
                HitSound();
            }
            else
            {
                MissSound();
                infinite.erros.value += 1;
            }
        }
        if(rhythm.leftAttack)
        {
            animator = leftBox.GetComponentInChildren<Animator>();
            audioSource = frontBox.GetComponentInChildren<AudioSource>();
            animator.SetBool(actualHit, true);
            if(leftBox == infinite.actualBox1.gameObject)
            {
                HitSound();
            }
            else
            {
                MissSound();
                infinite.erros.value += 1;
            }
        }
        if(rhythm.rightAttack)
        {
            animator = rightBox.GetComponentInChildren<Animator>();
            audioSource = frontBox.GetComponentInChildren<AudioSource>();
            animator.SetBool(actualHit, true);
            if(rightBox == infinite.actualBox1.gameObject)
            {
                HitSound();
            }
            else
            {
                MissSound();
                infinite.erros.value += 1;
                rhythm.charge -= 10;
            }
        }
        rhythm.charge += stageBonus;
    }
    private void HitSound()
    {
        audioSource.PlayOneShot(hit);
    }
    private void MissSound()
    {
        audioSource.PlayOneShot(miss);
    }
}