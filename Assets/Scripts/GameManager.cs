using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    public GameManagerState GMState;

    public void Start()
    {
        UpdateGameManagerState();
    }

    public void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                break;

            case GameManagerState.Gameplay:
                break;

            case GameManagerState.GameOver:
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
