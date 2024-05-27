using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip bombSound;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("HideBomb", 1.5f);
    }

    private void HideBomb()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<AudioSource>().clip = bombSound;
        GetComponent<AudioSource>().Play();
    }
}
