namespace Assets.Scripts.AI
{   

    /// <summary>
    /// Reprsents the agent state in the simulation.
    /// </summary>
    public enum AgentState 
    {
        /// <summary>
        /// State of the agent is proceding to destination.
        /// </summary>
        Move, 

        /// <summary>
        /// State of the agent when it is stopped at a destination.
        /// </summary>
        Idle, 

        /// <summary>
        /// State of the agent when it is in an accident.
        /// </summary>
        Accident
    }
}

