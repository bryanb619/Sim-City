using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
