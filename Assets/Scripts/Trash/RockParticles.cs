using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockParticles : MonoBehaviour
{
    public GameObject[] particles = new GameObject[3];

    private void OnEnable()
    {
        particles[0].transform.localPosition = new Vector2(0f, 0.3f);
        particles[1].transform.localPosition = new Vector2(-0.2f, -0.2f);
        particles[2].transform.localPosition = new Vector2(0.3f, -0.1f);

        particles[0].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 8.5f));
        particles[1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -20f));
        particles[2].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 40f));

        for (int i = 0; i < particles.Length; i++)
            particles[i].gameObject.SetActive(true);

        particles[0].GetComponent<RockParticlesMini>().rb2d.velocity = new Vector2(0, 0.1f);
        particles[1].GetComponent<RockParticlesMini>().rb2d.velocity = new Vector2(-0.07f, -0.07f);
        particles[2].GetComponent<RockParticlesMini>().rb2d.velocity = new Vector2(0.12f, -0.12f);
    }
    public void CheckActivation()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].activeSelf)
                return;
        }
        gameObject.SetActive(false);
    }
}
