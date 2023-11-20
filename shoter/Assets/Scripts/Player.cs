using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Player : MonoBehaviour
{
    Rigidbody player_rig;
    public GameObject camera;
    Ray to_floor;
    int len;
    public static Vector3 pos_player;
    float len_atack, mouse_sen = 10f, mouse, mouseX, mouseY, xrot, yrot;
    bool atack_mode = false, active = false, impact = false;
    
    private void Start()
    {
        SubscribeEvent();
        player_rig = GetComponent<Rigidbody>();
        GetComponent<MeshCollider>().convex = true;
    }
    private void Update()
    {
        if (active)
        {
            getposition();
            MovePos();
            atack();
            MoveMouse();
            Shot();
        }
        else
        {
            mouseX = 0;                       
            mouseY = 0;
            xrot = 0;
            yrot = 0;
        }
        player_rig.WakeUp();
    }
    // основные методы ..........................................................................................................................
    public void MovePos()
    {
        if (!impact)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"),0) * 10f * Time.deltaTime); // прердвижение в направлениии
        }
    }
    void MoveMouse()
    {
        xrot += Input.GetAxis("Mouse X") * mouse_sen;
        yrot += Input.GetAxis("Mouse Y") * mouse_sen;
        mouseX = Mathf.SmoothDamp(xrot, mouseX, ref mouse, 0.1f);
        mouseY = Mathf.SmoothDamp(yrot, mouseY, ref mouse, 0.1f); 
        transform.rotation = Quaternion.Euler(-90f, xrot, 0f);
        camera.transform.rotation = Quaternion.Euler(mouseY > 90 ? -90 : mouseY < -90 ? 90 : -mouseY, mouseX, 0f);
    }
    void SubscribeEvent() //Здесь будет происходить подписка на события которые будут менять какинто значения в Player
    {
        mananger.gamemode_Event += () => active = !active;
        enemy.Impact_Event += () => impact = !impact;
    }
    //...........................................................................................................................................
    // методы для данной игры/////////////////////////////////////////////////////////
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
    } // рефакторинг
    public void getposition()
    {
        to_floor = new Ray(player_rig.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(to_floor, out hit)) 
        {
            pos_player = hit.point;
        }
    }    // рефакторинг
    void Shot()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward * 20f);  // направление оносительно САМОГО ОБЪЕКТА а не в общем
        Debug.DrawRay(camera.transform.position, camera.transform.forward * 20f, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            hit.rigidbody.AddForce(camera.transform.forward * 10f);
        }
    }
    void VisionNPS()
    {

    }
    /////////////////////////////////////////////////////////////////////////////////////////
}
