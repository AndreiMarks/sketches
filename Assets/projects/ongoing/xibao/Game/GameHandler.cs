using System;
using System.Linq;
using UnityEngine;

namespace Xibao
{
    public class GameHandler : XBBehaviour
    {
        public static event Action<Phase> OnPhaseChanged = ( phase ) => { };

        [SerializeField]
        private PhaseObject[] phaseObjects;
        private PhaseObject _currentPhaseObject;

        public void SetUpGame()
        {
            Debug.Log( "Setting up game." );
            SetPhase( Phase.Glycolysis );
        }
        
        public void UpdateGame()
        {
            _currentPhaseObject.UpdatePhase();
        }

        private PhaseObject GetPhaseObjectByType( Phase phaseType )
        {
            return phaseObjects.FirstOrDefault( p => p.phase == phaseType );
        }
        
        private void SetPhase( Phase newPhase )
        {
            _currentPhaseObject = GetPhaseObjectByType( newPhase );
            
            switch ( newPhase )
            {
                case ( Phase.Glycolysis ):
                    _currentPhaseObject.init = InitGlycolysis;
                    _currentPhaseObject.update = UpdateGlycolysis;
                break;
                    
                case ( Phase.KrebsCycle ):
                    _currentPhaseObject.init = InitKrebsCycle;
                    _currentPhaseObject.update = UpdateKrebsCycle;
                break;
                    
                case ( Phase.ElectronTransportChain ):
                    _currentPhaseObject.init = InitElectronTransportChain;
                    _currentPhaseObject.update = UpdateElectronTransportChain;
                break;
            }

            _currentPhaseObject.LoadInitialStep();
            
            OnPhaseChanged( _currentPhaseObject.phase );
        }
        
        #region Phases Functions ==================================================

        private void InitGlycolysis()
        {
            Debug.Log( "I am starting glycolysis." );
            _currentPhaseObject.LoadInitialStep();
        }

        private void DebugUpdate( Phase nextPhase )
        {
            if ( _InputHandler.WaitForAnyInput() )
            {
                Debug.Log( "Move on to the next transition." );
                if ( _currentPhaseObject.CheckCompletedFinalStep() )
                {
                    SetPhase( nextPhase );
                }
            }
        }
        
        private void UpdateGlycolysis()
        {
            Debug.Log( "I am updating glycolysis." );
            DebugUpdate( nextPhase: Phase.KrebsCycle );
        }
        
        private void InitKrebsCycle()
        {
            Debug.Log( "I am starting Krebs cycle." );
        }
        
        private void UpdateKrebsCycle()
        {
            Debug.Log( "I am updating Krebs cycle." );
            DebugUpdate( nextPhase: Phase.ElectronTransportChain );
        }
        
        private void InitElectronTransportChain()
        {
            Debug.Log( "I am starting Electron transport chain." );
        }
        
        private void UpdateElectronTransportChain()
        {
            Debug.Log( "I am updating Electron transport chain." );
            DebugUpdate( nextPhase: Phase.Glycolysis );
        }
        #endregion
        
        [Serializable]
        private class PhaseObject
        {
            public Phase phase;
            public string[] notes;
            
            public TransitionStep initialStep;
            public TransitionStep finalStep;
            
            public Action init;
            public Action update;

            public void UpdatePhase()
            {
                update();
            }

            public void LoadInitialStep()
            {
                Debug.Log( "Loading initial step for: " + phase );
            }

            public bool CheckCompletedFinalStep()
            {
                return true;
            }
        }
    }

    public enum Phase
    {
        Glycolysis,
        KrebsCycle,
        ElectronTransportChain
    }
}
