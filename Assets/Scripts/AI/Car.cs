
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


            set
            {
                if (value < 0) 
                { 
                    
                }
                else if (value > 120) 
                { 

                }
                else
                {
                    Speed = value;
                }
            }
        }

        public Car()
        {

        }

        public override string ToString()
        {
            return $"{base.ToString()}";
        }

    }
}
