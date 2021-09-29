using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : MonoBehaviour
{
    public Animator animator;
    public float percentage = 2;

    void Start()
    {
        StartCoroutine(ShootingStarCo());
    }

    private IEnumerator ShootingStarCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Random.Range(0, 100) < percentage && !animator.GetBool("shoot"))
            {
                transform.localPosition = new Vector3(Random.Range(-5, 5), Random.Range(-7, 7));
                transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                animator.SetBool("shoot", true);
            }
        }
    }
}
