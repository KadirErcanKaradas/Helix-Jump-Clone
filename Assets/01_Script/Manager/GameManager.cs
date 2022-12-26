using System;

public class GameManager : Singleton<GameManager>
{
    public GameStage GameStage { get; private set; }

    public int comboCount;
    public void SetGameStage(GameStage gameStage)
    {
        GameStage = gameStage;
    }
    
}
public enum GameStage
{
    NotLoaded, Loaded, Started, Win, Fail, Cannon
}