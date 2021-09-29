using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public List<AudioClip> adClip = new List<AudioClip>();
    public GameObject boomEff;
    private List<GameObject> boomEffs = new List<GameObject>();
    public GameObject sonicEff;
    private List<GameObject> sonicEffs = new List<GameObject>();
    public GameObject bigBoomEff;
    private List<GameObject> bigBoomEffs = new List<GameObject>();
    public GameObject rockParticle;
    private List<GameObject> rockParticles = new List<GameObject>();

    public float distance = 15f;           // 도지와의 거리(사건의 지평선)
    private float timeScale = 1f;
    public ConstantShakePreset shake;

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    private void Update()
    {
        // FOR TEST @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DogeController.inst.SetShield(true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            DogeController.inst.Move(1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            DogeController.inst.Move(0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            DogeController.inst.Move(2);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            DogeController.inst.Move(0);
        }
        ShakeScreen();
    }
    private void ShakeScreen()
    {
        if (shake.Intensity == DogeController.inst.speed * 2)
            return;
        shake.Intensity = DogeController.inst.speed * 2;
        Camera.main.GetComponent<ProCamera2DShake>().ConstantShake(0);
    }
    public void SetGameStop(float time)
    {
        StartCoroutine("SetGameStopCo", time);
    }
    private IEnumerator SetGameStopCo(float time)
    {
        Time.timeScale = timeScale * 0.0001f;
        yield return new WaitForSeconds(0.0001f * time);
        Time.timeScale = timeScale;
    }
    public void SetTimeScale(float value)
    {
        timeScale = value;
        Time.timeScale = timeScale;
    }
    public void InstantiateBoomEffect(Vector2 position)
    {
        SoundManager.inst.PlaySound(adClip[0]);

        GameObject current = null;

        for (int i = 0; i < boomEffs.Count; i++)
        {
            if (!boomEffs[i].activeSelf)
            {
                current = boomEffs[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(boomEff, position, Quaternion.identity, transform) as GameObject;
            boomEffs.Add(current);
        }
        else
        {
            current.transform.position = position;
            current.SetActive(true);
        }
    }
    public void InstantiateSonicboomEffect(Vector2 position)
    {
        GameObject current = null;

        for (int i = 0; i < sonicEffs.Count; i++)
        {
            if (!sonicEffs[i].activeSelf)
            {
                current = sonicEffs[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(sonicEff, position, Quaternion.identity, transform) as GameObject;
            sonicEffs.Add(current);
        }
        else
        {
            current.transform.position = position;
            current.SetActive(true);
        }
    }
    public void InstantiateBigExplosionEffect(Vector2 position)
    {
        GameObject current = null;

        for (int i = 0; i < bigBoomEffs.Count; i++)
        {
            if (!bigBoomEffs[i].activeSelf)
            {
                current = bigBoomEffs[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(bigBoomEff, position, Quaternion.identity, transform) as GameObject;
            bigBoomEffs.Add(current);
        }
        else
        {
            current.transform.position = position;
            current.SetActive(true);
        }
    }
    public void InstantiateRockParticles(Vector2 position)
    {
        GameObject current = null;
        for (int i = 0; i < rockParticles.Count; i++)
        {
            if (!rockParticles[i].activeSelf)
            {
                current = rockParticles[i];
                break;
            }
        }
        if (current == null)
        {
            current = Instantiate(rockParticle, position, Quaternion.identity, transform) as GameObject;
            rockParticles.Add(current);
        }
        else
        {
            current.transform.position = position;
            current.SetActive(true);
        }
    }
}
