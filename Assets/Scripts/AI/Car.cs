using System;

namespace Assets.Scripts.AI
{
    public class Car : Agent, ICar
    {   

        public float Speed 
        {
            get
            {
                return this.Speed;
            }
            
            private set
            {
                if (value < 0)
                {
                
                }
                

                if (value > 150)
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

        public float AngularSpeed 
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="acceleration"></param>
        /// <param name="size"></param>
        /// <param name="mass"></param>
        public Car(float speed, float acceleration, float size, float mass) 
        {
            Speed           = speed;
            Acceleration    = acceleration;
            Size            = size;
            Mass            = mass;
        }   


        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Base string + 
        /// </returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Speed: {Speed} "
            + $"Acceleration: {Acceleration} Size: {Size} Mass: {Mass}";
        }

        public static implicit operator float(Car v)
        {
            throw new NotImplementedException();
        }
    }
}
