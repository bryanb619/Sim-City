
namespace Assets.Scripts.AI
{
    public abstract class Agent
    {
        public Agent() { }


        public virtual float craziness { get; }


        public override string ToString ()
        { 
            return $"{GetType().Name}"; 
        }

    }
}
