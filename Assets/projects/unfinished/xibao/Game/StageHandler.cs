using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xibao
{
    public class StageHandler : MonoBehaviour
    {
        public Transform monadHolder;
        
        public StagePositioner precursorHolder;
        public StagePositioner activeHolder;
        public StagePositioner productHolder;
    
        public void AddMonads( List<Monad> monads )
        {
            CreateMonads( monads, precursorHolder );
        }

        public void ReplaceMonads( List<Monad> monads )
        {
            activeHolder.ClearDisplays();
            CreateMonads( monads, activeHolder );
        }

        private void CreateMonads( List<Monad> monads, StagePositioner positioner )
        {
            foreach ( Monad m in monads )
            {
                MonadDisplay md = m.GetMonadDisplay( parent: monadHolder );
                positioner.AddDisplay( md, positioner.transform.position );
            }
        }

        public void MovePrecursorToActive()
        {
            ShiftMonadsFromTo( precursorHolder, activeHolder );
        }

        public void MoveActiveToProduct()
        {
            ShiftMonadsFromTo( activeHolder, productHolder );
        }
        
        public void MoveProductToPrecursor()
        {
            ShiftMonadsFromTo( productHolder, precursorHolder );
        }

        private void ShiftMonadsFromTo( StagePositioner from, StagePositioner to )
        {
            List<MonadDisplay> monads = from.TakeCurrentMonads();
            to.AddDisplays( monads, to.transform.position );
        }
    }
}
