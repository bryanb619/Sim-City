using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    public interface ICar
    {
        float Speed { get;}

        float Acceleration { get; }

        float Size { get; }

        float Mass { get; }
    }
}