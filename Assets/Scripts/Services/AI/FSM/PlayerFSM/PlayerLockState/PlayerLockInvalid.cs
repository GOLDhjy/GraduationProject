using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class PlayeLockInvalid : LockAIState
    {
        public PlayeLockInvalid()
        {
            type = AIStateEnum.INVALID;
        }
    }
}