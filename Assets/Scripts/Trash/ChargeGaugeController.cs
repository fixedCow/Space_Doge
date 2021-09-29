using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeGaugeController : MonoBehaviour
{
    public static ChargeGaugeController inst;

    public Image gaugeImg;
    public Animator animator;

    [SerializeField] private float point;
    [SerializeField] private float pointMask;

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    private void Start()
    {
        // StartCoroutine(SetPointMaskCo());
        StartCoroutine(AutoIncreaseGaugeCo());
    }
    private void Update()
    {
        if (point >= 80 && !animator.GetBool("isReady"))
        {
            animator.SetBool("isReady", true);
        }
    }

    public bool CheckDashPossibility()
    {
        return animator.GetBool("isReady");
    }
    public void ModPoint(float value)
    {
        point += value;
    }
    private IEnumerator SetPointMaskCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            pointMask = point + Random.Range(-1, 1);
            pointMask = Mathf.Clamp(pointMask, 0, 100);
            gaugeImg.fillAmount = pointMask / 100;
        }
    }
    private IEnumerator AutoIncreaseGaugeCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            point++;
            point = Mathf.Clamp(point, 0, 100);

            pointMask = point + Random.Range(-2f, 2f);
            pointMask = Mathf.Clamp(pointMask, 0, 100);
            gaugeImg.fillAmount = pointMask / 100;
        }
    }
}
