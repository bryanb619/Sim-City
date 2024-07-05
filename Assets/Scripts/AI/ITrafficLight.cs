using Assets.Scripts.AI;
using LibGameAI.FSMs;

/// <summary>
/// 
/// </summary>
public interface ITrafficLight 
{

    LightState Light { get; }

    void SwapLightState();

}
