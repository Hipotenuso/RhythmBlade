using System.Collections;
using UnityEngine;
public class AFK : MonoBehaviour
{
    public Rhythm rhythm;
    private Coroutine afkCoroutine;
    public int chargeLost;
    void Start()
    {
        if(rhythm == null) rhythm = GetComponentInChildren<Rhythm>();
        ResetAFKTimer();
    }
    public void ResetAFKTimer()
    {
        if (afkCoroutine != null)
        {
            StopCoroutine(afkCoroutine);
        }
        afkCoroutine = StartCoroutine(AFKCourotine());
    }
    private IEnumerator AFKCourotine()
    {
        yield return new WaitForSeconds(rhythm.delayToAFK);
        TurningIdle();
    }
    private void TurningIdle()
    {
        transform.position = rhythm.initialPosition;
        if(rhythm.frontAttack)
        {
            rhythm.animator.SetTrigger("frontafk");
        }
        if(rhythm.backAttack)
        {
            rhythm.animator.SetTrigger("backafk");
        }
        if(rhythm.leftAttack || rhythm.rightAttack)
        {
            rhythm.animator.SetTrigger("sideafk");
        }
        Invoke(nameof(Idle), rhythm.delayToIdle);
    }
    private void Idle()
    {
        rhythm.animator.SetBool("idle", true);
    }
}