using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoopyCandle : MonoBehaviour
{
    [Header("Timer Times")]
    public float RiseTime;
    public float StillTime;

    [Header("Speeds")]
    public float RiseSpeed;
    public float Rushspeed;

    [Header("Booleans")]
    public bool CanRise = false;
    public bool CanRush = false;

    [Header("SFX")]
    public AudioClip Laugh;
    public AudioClip Jump;

    [Header("Components")]
    public CapsuleCollider CapCollide;
    public AudioSource AudieSource;

    [Header("Other")]
    public GameObject Player;
    public Vector3 TargetPOS;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanRise == true)
        {
            transform.Translate(Vector3.up * RiseSpeed * Time.deltaTime);
        }
        else if (CanRush == true)
        {
            TargetPOS = Player.transform.position - transform.position.normalized;
            transform.Translate(TargetPOS * Rushspeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CapCollide.enabled = false;
            StartCoroutine(Rising());
        }
    }

    IEnumerator Rising()
    {
        AudieSource.PlayOneShot(Laugh);
        CanRise = true;
        yield return new WaitForSeconds(RiseTime);

        CanRise = false;
        StartCoroutine(Idle());
    }

    IEnumerator Idle() 
    {
        yield return new WaitForSeconds(StillTime);

        AudieSource.PlayOneShot(Jump);
        CanRush = true;
    }
}
