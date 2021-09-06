using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public static BulletGenerator inst;

    public GameObject bullet;
    private List<GameObject> bullets = new List<GameObject>();

    [SerializeField] private float speedAvg;
    [SerializeField] private float speedSeMult;             // 0 ~ 1.0
    [SerializeField] private int bulletTargetCount;
    [SerializeField] private int bulletCurrentCount;

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    private void Start()
    {
        StartCoroutine(AddBulletCountCo());
        StartCoroutine(CheckBulletCountCo());
    }

    public void InstantiateBullet()
    {
        GameObject curBullet = null;
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeSelf)
            {
                curBullet = bullets[i];
                break;
            }
        }
        if (curBullet == null)
        {
            curBullet = Instantiate(bullet, GetRandomPosition(GameManager.inst.distance), Quaternion.identity, gameObject.transform) as GameObject;
            bullets.Add(curBullet);
        }
        else
        {
            curBullet.SetActive(true);
            curBullet.transform.position = GetRandomPosition(GameManager.inst.distance);
        }
        curBullet.GetComponent<Bullet>().SetSpeed(Random.Range(speedAvg - (speedAvg * speedSeMult), speedAvg + (speedAvg * speedSeMult)));
        curBullet.GetComponent<Bullet>().Shoot();
        bulletCurrentCount++;

    }
    private Vector2 GetRandomPosition(float distance)
    {
        float theta = Random.Range(0, 360);
        Vector2 result = DogeController.inst.transform.position + new Vector3(distance *
            Mathf.Cos(theta * Mathf.Deg2Rad), distance * Mathf.Sin(theta * Mathf.Deg2Rad));
        return result;
    }
    public void BulletRemoved()
    {
        bulletCurrentCount--;
        Invoke("CheckBulletCount", Random.Range(0, 1f));
    }
    private void CheckBulletCount()
    {
        if (bulletCurrentCount < bulletTargetCount)
        {
            InstantiateBullet();
        }
    }
    private IEnumerator CheckBulletCountCo()
    {
        float timer;
        while (true)
        {
            timer = Random.Range(0, 1f);
            yield return new WaitForSeconds(timer);
            CheckBulletCount();
        }
    }
    private IEnumerator AddBulletCountCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            bulletTargetCount++;
        }
    }
}
