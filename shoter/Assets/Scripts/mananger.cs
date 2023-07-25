using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mananger : MonoBehaviour
{
    public static bool gamemode;
    public static bool menumode;
    public static bool win = false;
    public static int n = -1;
    public GameObject menu, gamescene, _enemy, player, atackbonus;
    public Rigidbody _player;
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
        endgame_death();
        if (win)
        {
            endgame_win();
            win = false;
        }
    
    }
    public void start_game()
    {
        gamemode = true;
        menumode = false;
        Spawn();
        menu.SetActive(false);
        player.transform.position = start_pos;
        _player.isKinematic = true;
        _player.isKinematic = false;
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
        GameObject a = Instantiate(atackbonus, gamescene.transform.Find("atack").transform.position, Quaternion.identity);
        StartCoroutine(Time_active(a));
        for (int i = 1; i < 7; i++)
        {
            Instantiate(_enemy, gamescene.transform.Find(i.ToString()).transform.position, Quaternion.identity);
        }
    }
    public void endgame_death()
    {
        if (player.transform.position.y <= -10)
        {
            gamemode = false;
            menumode = true;
            menu.SetActive(true);
            StopAllCoroutines();
        }
    }
    public void endgame_win()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(n);
    }
}
