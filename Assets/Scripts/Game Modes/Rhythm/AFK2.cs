using System.Collections;
using UnityEngine;
public class Abuda : MonoBehaviour
{
    public Rhth2 rhth2;
    private Coroutine afkCoroutine2;
    public int chargeLost;
    void Start()
    {
        ResetAFKTimer();
    }
    public void ResetAFKTimer()
    {
        if (afkCoroutine2 != null)
        {
            StopCoroutine(afkCoroutine2);
        }
        afkCoroutine2 = StartCoroutine(AFKCourotine2());
    }
    private IEnumerator AFKCourotine2()
    {
        yield return new WaitForSeconds(rhth2.delayToAFK);
        TurningIdle();
    }
    private void TurningIdle()
    {
        transform.position = rhth2.initialPosition;
        if(rhth2.frontAttack)
        {
            rhth2.animator.SetTrigger("frontafk");
        }
        if(rhth2.backAttack)
        {
            rhth2.animator.SetTrigger("backafk");
        }
        if(rhth2.leftAttack || rhth2.rightAttack)
        {
            rhth2.animator.SetTrigger("sideafk");
        }
        Invoke(nameof(Idle), rhth2.delayToIdle);
    }
    private void Idle()
    {
        rhth2.animator.SetBool("idle", true);
    }
}