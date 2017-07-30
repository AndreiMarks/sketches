using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Xibao
{
    public class GameHandler : XBBehaviour
    {
        public static event Action<Phase> OnPhaseChanged = ( phase ) => { };
        public static event Action<RoundPhase> OnRoundPhaseChanged = ( phase ) => { };

        [SerializeField]
        private PhaseObject[] phaseObjects;
        private PhaseObject _currentPhaseObject;

        private RoundPhase _currentRoundPhase;

        private List<Monad> _monadPool = new List<Monad>();

        #region Plumbing ==================================================
        public void SetUpGame()
        {
            Debug.Log( "Setting up game." );
            SetPhase( Phase.Glycolysis );
            SetRoundPhase( RoundPhase.Precursor );
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
                    _currentPhaseObject.update = UpdateGlycolysis;
                break;
                    
                case ( Phase.KrebsCycle ):
                    _currentPhaseObject.update = UpdateKrebsCycle;
                break;
                    
                case ( Phase.ElectronTransportChain ):
                    _currentPhaseObject.update = UpdateElectronTransportChain;
                break;
            }

            List<Monad> startingMonads = _currentPhaseObject.LoadInitialStepPrecursors();
            LoadInitialMonadsIntoStage( startingMonads );            
            
            OnPhaseChanged( _currentPhaseObject.phase );
        }

        private void SetRoundPhase( RoundPhase newPhase )
        {
            Debug.Log( "GameHandler: SetRoundPhase" );
            _currentRoundPhase = newPhase;
            OnRoundPhaseChanged( _currentRoundPhase );
        }
        #endregion
        
        #region Game State Functions==================================================

        private void AddMonadsToPool( List<Monad> monads )
        {
            _monadPool.AddRange( monads );
        }

        private void LoadInitialMonadsIntoStage( List<Monad> monads )
        {
            //AddMonadsToPool( monads );
            _StageHandler.AddMonads( monads );
        }
        #endregion
        
        #region Phases Functions ==================================================

        private void UpdateGlycolysis()
        {
        }

        private void UpdateKrebsCycle()
        {
        }

        private void UpdateElectronTransportChain()
        {
        }

        #endregion
        
        #region Round Status Functions

        public void GoToActivePhase()
        {
            _StageHandler.MovePrecursorToActive();
            SetRoundPhase( RoundPhase.Active );
        }

        public void GoToProductPhase()
        {
            // Here we validate if the proper thing was created.
            // For now we just pass on.
            bool productsValidated = true;
            if ( productsValidated )
            {
                HandleProductPhase();
            }
        }

        public void GoToNextTransition()
        {
            // Here we move everything to the precursor pool and see what's up?
            Debug.Log( "Here we move everything to the precursor pool and see what's up?" );
            _currentPhaseObject.AdvanceStep();
            
            SetRoundPhase( RoundPhase.Precursor );
        }

        private void HandleProductPhase()
        {
            List<Monad> productMonads = _currentPhaseObject.LoadCurrentStepProducts();
            _StageHandler.ReplaceMonads( productMonads );
            _StageHandler.MoveActiveToProduct();
            SetRoundPhase( RoundPhase.Product );
        }
        #endregion
        
        [Serializable]
        private class PhaseObject
        {
            public Phase phase;
            public string[] notes;
            
            public TransitionStep[] transitionSteps;
            private TransitionStep _currentTransitionStep;
            
            public Action init;
            public Action update;

            public void UpdatePhase()
            {
                update();
            }

            public List<Monad> LoadInitialStepPrecursors()
            {
                Debug.Log( "Loading initial step for: " + phase );
                _currentTransitionStep = transitionSteps[0];
                return LoadCurrentStepPrecursors();
            }

            public List<Monad> LoadCurrentStepPrecursors()
            {
                return _currentTransitionStep.GetPrecursors();
            }

            public List<Monad> LoadCurrentStepProducts()
            {
                return _currentTransitionStep.GetProducts();
            }
            
            public void AdvanceStep()
            {
                int index = Array.IndexOf( transitionSteps, _currentTransitionStep );
                if ( index == transitionSteps.Length - 1 )
                {
                    Debug.Log("On the final step, deal with this later.");
                    return;
                }

                index++;
                _currentTransitionStep = transitionSteps[index];
            }

            public bool CheckCompletedFinalStep()
            {
                return true;
            }
        }
    }

    public enum RoundPhase
    {
        Precursor,
        Active,
        Product
    }
    
    public enum Phase
    {
        Glycolysis,
        KrebsCycle,
        ElectronTransportChain
    }
}
