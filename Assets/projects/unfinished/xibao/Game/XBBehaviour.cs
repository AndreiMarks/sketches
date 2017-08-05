using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xibao
{
    public class XBBehaviour : MonoBehaviour
    {
        protected Xibao _Xibao
        {
            get { return Xibao.Instance; }
        }

        protected GameHandler _GameHandler
        {
            get { return _Xibao.gameHandler; }
        }
        
        protected InputHandler _InputHandler
        {
            get { return _Xibao.inputHandler; }
        }

        protected StageHandler _StageHandler
        {
            get { return _Xibao.stageHandler; }
        }
    }
}
