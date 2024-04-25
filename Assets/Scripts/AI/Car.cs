
using System;

namespace Assets.Scripts.AI
{
    public class Car : Agent, ICar
    {   
        public float Speed 
        {
            get; set;
        }

        public Car(float speed = 40f)
        {
            Speed = speed;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Speed: {Speed}";
        }

    }
}
