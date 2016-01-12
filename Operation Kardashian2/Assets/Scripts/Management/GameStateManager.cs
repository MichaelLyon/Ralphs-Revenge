using UnityEngine;
using System.Collections;

public static class GameStateManager
{
    public enum GameState
    {
        Level,
        Challenge
    }

    public static GameState gameState;
}
