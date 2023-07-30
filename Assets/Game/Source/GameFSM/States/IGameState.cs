namespace Game.Source.GameFSM
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}