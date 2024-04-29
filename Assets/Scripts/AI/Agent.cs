
namespace Assets.Scripts.AI
{
    public abstract class Agent
    {
        public Agent() { }


        public override string ToString ()
        { 
            return $"{GetType().Name}"; 
        }

    }
}
