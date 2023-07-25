using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody player_;
    Ray to_floor;
    int len;
    public static Vector3 pos_player;
    public float speed;
    public float len_atack;
    bool atack_mode = false;
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
            atack();
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
    public void atack()
    {
        if (atack_bonus.flag) 
        {
            atack_mode = true;
        }
        if (atack_mode)
        {
            List<GameObject> enemylist = new List<GameObject>(); 
            enemylist.AddRange(GameObject.FindGameObjectsWithTag("enemy"));
            GameObject neares_enemy = null;
            len = enemylist.Count;
            
            float min_dir = 100000000;
            foreach(GameObject i in enemylist)
            {
                float dir = (i.transform.position - pos_player).magnitude;
                neares_enemy = dir < min_dir ? i : neares_enemy;
                min_dir = dir < min_dir ? dir : min_dir;
                
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (min_dir < len_atack)
                {
                    Destroy(neares_enemy);
                    atack_mode = false;
                    atack_bonus.flag = false;
                    if (enemylist.Count <= 1)
                    {
                        mananger.win = true;
                        mananger.n++;
                    }
                }
            }
        }

    }
}
