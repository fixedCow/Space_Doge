using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Bullet = 0,
    CoreRock = 1,
    Rock = 2
}
public class BulletGenerator : MonoBehaviour
{
    public static BulletGenerator inst;

    public GameObject bullet;
    private List<GameObject> bullets = new List<GameObject>();
    public GameObject coreRock;
    private List<GameObject> coreRocks = new List<GameObject>();
    public GameObject rock;
    private List<GameObject> rocks = new List<GameObject>();

    private float speed = 1f;
    private const float speedMult = 0.006f;
    private const float turnSpeedMult = 0.01f;
    [SerializeField] private float df;              // difficulty factor
    private int bulletCurrentCount;
    private int rockCurrentCount;
    private int coreRockCurrentCount;

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
        CheckCount();
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
            curBullet = Instantiate(bullet, GetRandomPosition(GameManager.inst.distance - 2f), Quaternion.Euler(0, 0, Random.Range(0, 360)), gameObject.transform) as GameObject;
            bullets.Add(curBullet);
        }
        else
        {
            curBullet.transform.position = GetRandomPosition(GameManager.inst.distance - 2f);
            curBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
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
    public void InstantiateRock()
    {
        rockCurrentCount++;
        GameObject current = null;
        for (int i = 0; i < rocks.Count; i++)
        {
            if (!rocks[i].activeSelf)
            {
                current = rocks[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(rock, GetRandomPosition(GameManager.inst.distance - 2f), Quaternion.Euler(0, 0, Random.Range(0, 360)), gameObject.transform) as GameObject;
            rocks.Add(current);
        }
        else
        {
            current.transform.position = GetRandomPosition(GameManager.inst.distance - 2f);
            current.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            current.SetActive(true);
            if (current.GetComponent<Rock>().rb2d.bodyType != RigidbodyType2D.Dynamic)
                current.GetComponent<Rock>().rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
        Rock temp = current.GetComponent<Rock>();
        temp.rb2d.velocity = Vector2.zero;
        temp.SetSpeed(Random.Range(0.1f, 0.5f));
        temp.Shoot();
    }
    public void InstantiateCoreRock()
    {
        coreRockCurrentCount++;
        GameObject current = null;
        for (int i = 0; i < coreRocks.Count; i++)
        {
            if (!coreRocks[i].activeSelf)
            {
                current = coreRocks[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(coreRock, GetRandomPosition(GameManager.inst.distance - 2f), Quaternion.Euler(0, 0, Random.Range(0, 360)), gameObject.transform) as GameObject;
            coreRocks.Add(current);
        }
        else
        {
            current.transform.position = GetRandomPosition(GameManager.inst.distance - 2f);
            current.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            current.SetActive(true);
            if (current.GetComponent<CoreRock>().rb2d.bodyType != RigidbodyType2D.Dynamic)
                current.GetComponent<CoreRock>().rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
        CoreRock temp = current.GetComponent<CoreRock>();
        temp.rb2d.velocity = Vector2.zero;
        temp.SetSpeed(Random.Range(0.1f, 0.3f));
        temp.Shoot();
    }
    private Vector2 GetRandomPosition(float distance)
    {
        float theta = Random.Range(0, 360);
        Vector2 result = DogeController.inst.transform.position + new Vector3(distance *
            Mathf.Cos(theta * Mathf.Deg2Rad), distance * Mathf.Sin(theta * Mathf.Deg2Rad));
        return result;
    }
    public void ObjectRemoved(int num)
    {
        switch (num)
        {
            case (int)ObjectType.Bullet:
                bulletCurrentCount--;
                break;
            case (int)ObjectType.CoreRock:
                coreRockCurrentCount--;
                break;
            case (int)ObjectType.Rock:
                rockCurrentCount--;
                break;
        }
    }
    private void CheckCount()
    {
        if (bulletCurrentCount < (df / 3))
            InstantiateBullet();
        if (coreRockCurrentCount < (df / 30))
            InstantiateCoreRock();
        if (rockCurrentCount < (df / 55))
            InstantiateRock();
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
