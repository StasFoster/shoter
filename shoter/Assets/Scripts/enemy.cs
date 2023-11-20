using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class enemy : MonoBehaviour, IDeath_from_player
{
    Rigidbody _enemy;
    NavMeshAgent _agent;
    public float pauer, radius, dir;
    float dir_pos;
    public Rigidbody player;
    public static bool impact = false;
    bool flag = false;
    Vector3 start_pos;
    public static Action Impact_Event;

    private void OnCollisionEnter(Collision collision)
    {
       flag = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        flag = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        flag = true;
    }
    private void Start()
    {
        Impact_Event += () => impact = !impact;
        start_pos = transform.position;
        _enemy = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
    private void Update()
    {    
        if (mananger.gamemode)
        {   
            //move_to_player();
            if (flag && !impact)
            {
                StartCoroutine(ForceAtPlayer());
                flag = false;
            }
        }
        Death();
    }
    public void move_to_player()
    {
        dir_pos = (Player.pos_player - start_pos).magnitude;
        if (dir_pos > dir)
        {
            _agent.SetDestination(start_pos);
        }
        else
        {
            _agent.SetDestination(Player.pos_player);
        }
    }
    public IEnumerator ForceAtPlayer()
    {
        Impact_Event.Invoke();
        player.AddExplosionForce(pauer, transform.position, radius);  
        yield return new WaitForSeconds(1f);
        Impact_Event.Invoke();
    }

    public void Death()
    {
        if (!mananger.gamemode)
        {
            Destroy(this.gameObject);
        }
    }
}
