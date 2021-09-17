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
    private void LateUpdate()
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
            curBullet = Instantiate(bullet, GetRandomPosition(GameManager.inst.distance - 2f), Quaternion.identity, gameObject.transform) as GameObject;
            bullets.Add(curBullet);
        }
        else
        {
            curBullet.transform.position = GetRandomPosition(GameManager.inst.distance - 2f);
            curBullet.GetComponent<Bullet>().sr.gameObject.SetActive(true);
            curBullet.SetActive(true);
            if (curBullet.GetComponent<Bullet>().rb2d.bodyType != RigidbodyType2D.Dynamic)
                curBullet.GetComponent<Bullet>().rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
        Bullet temp = curBullet.GetComponent<Bullet>();
        temp.rb2d.velocity = Vector2.zero;
        if (df > 100f && Random.Range(0, 100) < 15)
        {
            temp.SetIsGuided(true);

            float angle = Mathf.Atan2(DogeController.inst.transform.position.y - temp.transform.position.y,
                DogeController.inst.transform.position.x - temp.transform.position.x) * Mathf.Rad2Deg;
            temp.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            temp.SetTurnSpeed(df * turnSpeedMult);
            temp.SetSpeed(Random.Range(0.7f, 1.8f));
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
        // CheckBulletCount();
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
            yield return new WaitForSeconds(1.3f);
            df += 1;
        }
    }
}
