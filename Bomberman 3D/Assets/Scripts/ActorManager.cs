using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.MouseLook cursor;

    [SerializeField] private List<Actor> actors;
    private int amountPlayersAlive;

    private static ActorManager _instance;

    public static ActorManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private void Start()
    {
        _instance = this;

        amountPlayersAlive = actors.Count;
    }

    public void RemoveFromList(Actor _actor)
    {
        actors.Remove(_actor);
        amountPlayersAlive--;
        if(amountPlayersAlive == 1)
        {
            cursor.CursorIsVisible();
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.WinGame();
        }
    }
}
