using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = new Vector3(0f, 0f, player.transform.position.z);
    }
}
