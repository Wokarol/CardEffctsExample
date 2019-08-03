using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Card myCard;
    private Game game;
    private Actor dave;
    private Actor monster;

    void Start()
    {
        game = new Game();
        dave = new Actor("Dave", 10, 20);
        monster = new Actor("Monster", 0, 60);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            myCard.Execute(game, dave, monster);
        }
    }
}
