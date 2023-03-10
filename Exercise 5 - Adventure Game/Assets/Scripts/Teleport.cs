using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    GameObject player;
    NavMeshAgent _navMeshAgent;
    Vector3 starting_point;
    public AudioClip nextSound; 
    public AudioClip wrongSound;
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // _navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent = player.GetComponent<NavMeshAgent>();    // this is the player
        starting_point = player.transform.position;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // if it's Door (6), then Door.cs script will be used for next level
            // otherwise: teleport

            // if teleporting to Teleport5 (wrong wroom)... coroutine:
            if (gameObject.name == "Door" || gameObject.name == "Door (2)" || gameObject.name == "Door (4)" || gameObject.name == "Door (7)") {
                StartCoroutine(WrongDoor());
            }
            else {
                // only teleport if not door 6 (bc that uses the Door.cs script to load next scene)
                if(gameObject.name != "NextDoor") {
                    _audioSource.PlayOneShot(nextSound);
                    _navMeshAgent.Warp(teleportTarget.transform.position);
                }
            }

        }


    }

    IEnumerator WrongDoor() {
        // teleport, wait, teleport back
        _navMeshAgent.Warp(teleportTarget.transform.position);
        _audioSource.PlayOneShot(wrongSound);
        yield return new WaitForSeconds(2);
        _navMeshAgent.Warp(starting_point);
    }




}
