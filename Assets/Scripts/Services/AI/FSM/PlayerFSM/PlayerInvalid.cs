using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class PlayerInvalid : AIState
    {
        public PlayerInvalid()
        {
            type = AIStateEnum.INVALID;
        }
    }
}
