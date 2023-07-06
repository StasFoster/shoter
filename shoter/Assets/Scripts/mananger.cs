using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mananger : MonoBehaviour
{
    public static bool gamemode;
    public static bool menumode;
    public GameObject menu, gamescene, enemy, player;
    List<GameObject> enemyList = new List<GameObject>();
    Vector3 start_pos;
    private void Start()
    {
        gamemode = false;
        menumode = true;
        start_pos = player.transform.position;
    }
    private void Update()
    {
        endgame();
    }
    public void start_game()
    {
        gamemode = true;
        menumode = false;
        Spawn();
        menu.SetActive(false);
        player.transform.position = start_pos;
    }
    public void Spawn()
    {
        for(int i = 1; i < 2; i++)
        {
            GameObject a = Instantiate(enemy, gamescene.transform.Find(i.ToString()).transform.position, Quaternion.identity);
            enemyList.Add(a);
        }
    }
    public void endgame()
    {
        if (player.transform.position.y <= -10)
        {
            gamemode = false;
            menumode = true;
            foreach (GameObject a in enemyList) Destroy(a);
            menu.SetActive(true);
        }
    }
}
