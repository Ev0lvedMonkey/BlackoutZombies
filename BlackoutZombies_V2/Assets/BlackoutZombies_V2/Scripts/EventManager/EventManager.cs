using System;

public class EventManager : IService
{
    public EventManager() { }

    public Action OnStartGame;
    public Action OnStopGame;
    public Action OnPauseGame;
    public Action OnResumeGame;

    public Action OnScoreIncremented;
    public Action OnKilledZombiesIncremented;


}
