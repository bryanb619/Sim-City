namespace Assets.Scripts.AI
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITrafficLight
    {

        LightState Light { get; }

        void SetInitialState(LightState state);

        void SwapLightState();

    }
}