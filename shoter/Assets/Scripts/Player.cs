using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody player_;
    Ray to_floor;
    public static Vector3 pos_player;
    public float speed;
    private void Start()
    {
        player_ = GetComponent<Rigidbody>();
       
    }
    private void Update()
    {
        if (mananger.gamemode)
        {
            getposition();
            Move();     
        }
        player_.WakeUp();
    }
    public void getposition()
    {
        to_floor = new Ray(player_.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(to_floor, out hit)) 
        {
            pos_player = hit.point;
        }
    }
    public void Move()
    {
        if (!enemy.impact)
        {
            player_.velocity = new Vector3(-Input.GetAxis("Horizontal") * speed, player_.velocity.y, -Input.GetAxis("Vertical") * speed);
        }
    }
}
