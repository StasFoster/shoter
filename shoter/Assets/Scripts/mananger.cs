using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class mananger : MonoBehaviour
{
    public static bool gamemode;

    public static bool win = false;
    public static int n = 0;
    public GameObject menu, gamescene, _enemy, player, atackbonus;
    public int amountEnemy, amountAtack;
    List<GameObject> enemyList = new List<GameObject>();
    Vector3 start_pos;
    public static Action gamemode_Event;
    private void Start()
    {
        gamemode_Event += () => gamemode = !gamemode;
        gamemode = false;
        start_pos = player.transform.position;
    }
    private void Update()
    {
        endgame_death();
        if (win)
        {
            endgame_win();
            win = false;
        }
    
    }
    public void start_game()
    {
        gamemode_Event.Invoke();
        Spawn();
        menu.SetActive(false);
        enemy.impact = false;
    }
    public IEnumerator Time_active(GameObject atack_bonus)
    {
        int n = 0;
        while (true)
        {
            if (n % 10 == 0)
            {
                atack_bonus.SetActive(true);
            }
            yield return new WaitForSeconds(1);
            n++;
        }
    }
    public void Spawn()
    {
        for(int i = 1; i < amountAtack + 1; i++)
        {
            GameObject a = Instantiate(atackbonus, gamescene.transform.Find("atack" + i.ToString()).transform.position, Quaternion.identity);
            StartCoroutine(Time_active(a));
        }
        for (int i = 1; i < amountEnemy + 1; i++)
        {
            Instantiate(_enemy, gamescene.transform.Find(i.ToString()).transform.position, Quaternion.identity);
        }
    }
    public void endgame_death()
    {
        if (player.transform.position.y <= -10)
        {
            if (gamemode) gamemode_Event.Invoke();
            menu.SetActive(true);
            StopAllCoroutines();
            player.transform.position = start_pos;
        }
    }
    public void endgame_win()
    {
        StopAllCoroutines();
        n++;
        SceneManager.LoadScene(n);
    }
}
