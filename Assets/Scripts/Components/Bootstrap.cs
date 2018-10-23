using UnityEngine;
using UnityEngine.UI;
using Base;

public class Bootstrap
    : MonoBehaviour
{
    [SerializeField]
    private Button startGame;

    protected void Start()
    {
        
    }

    public void StartGame()
    {
        Log.Info("Game", "Start Game!");
    }
}
