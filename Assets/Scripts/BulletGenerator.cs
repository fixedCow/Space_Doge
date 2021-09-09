using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public static BulletGenerator inst;

    public GameObject bullet;
    private List<GameObject> bullets = new List<GameObject>();

    private float speed = 1f;
    private const float speedMult = 0.006f;
    private const float turnSpeedMult = 0.01f;
    [SerializeField] private float df;              // difficulty factor
    [SerializeField] private int bulletCurrentCount;

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    private void Start()
    {
        StartCoroutine(SpeedAddCo());
    }
    private void Update()
    {
        CheckBulletCount();
    }

    public void InstantiateBullet()
    {
        bulletCurrentCount++;
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
            curBullet = Instantiate(bullet, GetRandomPosition(GameManager.inst.distance - 1f), Quaternion.identity, gameObject.transform) as GameObject;
            bullets.Add(curBullet);
        }
        else
        {
            curBullet.GetComponent<Bullet>().rb2d.bodyType = RigidbodyType2D.Dynamic;
            curBullet.SetActive(true);
            curBullet.transform.position = GetRandomPosition(GameManager.inst.distance - 1f);
        }
        Bullet temp = curBullet.GetComponent<Bullet>();
        temp.sr.gameObject.SetActive(true);
        temp.rb2d.velocity = Vector2.zero;
        if (df > 100f && Random.Range(0, 100) < 15)
        {
            temp.SetIsGuided(true);
            temp.SetTurnSpeed(df * turnSpeedMult);
            temp.SetSpeed(Random.Range(0.7f, 1.8f));
            // temp.gameObject.transform.LookAt(DogeController.inst.transform);
        }
        else
        {
            temp.SetSpeed(Random.Range(speed, speed + (df * speedMult)));
            temp.Shoot();
        }
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
        CheckBulletCount();
    }
    private void CheckBulletCount()
    {
        if (bulletCurrentCount < (df / 3))
        {
            InstantiateBullet();
        }
    }
    private IEnumerator SpeedAddCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            df += 1;
        }
    }
}
