
using System;

namespace Assets.Scripts.AI
{
    public class Car : Agent, ICar
    {   

        public float Speed 
        {
            get
            {
                return Speed;
            }
            
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Speed cannot be negative");
                }

                else if (value > 150)
                {
                    throw new ArgumentException("Speed cannot be greater than 150");
                }

                else
                {
                    Speed = value;
                }
            }
        }

        public float Acceleration 
        {
            get; private set;
        }

        public float Size 
        {
            get; private set;
        }

        public float Mass 
        {
            get; private set;
        }



        public Car(float speed, float acceleration, float size, float mass) 
        {
            Speed           = speed;
            Acceleration    = acceleration;
            Size            = size;
            Mass            = mass;

        }

        public override string ToString()
        {
            return $"{base.ToString()}, Speed: {Speed}";
        }

    }
}
