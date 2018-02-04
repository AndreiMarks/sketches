using System.Collections;
using UnityEngine;

namespace Maximiner
{
	public class MiningModule : Module
	{
		public override int Id { get; protected set; }

		public override ModuleType Type
		{
			get { return ModuleType.Mining; }
		}

		/// <summary>
		/// In meters cubed.
		/// </summary>
		public float MiningAmount { get; private set; }
		public float CycleTime { get; private set; }
		public bool IsActivated { get; private set; }
		public int Order { get; private set; }

		private IEnumerator _currentCycle;
		private Coroutine _currentCycleCoroutine;
		private AsteroidMiningInfo _currentAsteroidMiningInfo;
		
		private Asteroid _target;
		public Asteroid Target
		{
			get { return _target; }
		}

		public Coroutiner _Coroutiner
		{
			get { return Coroutiner.Instance; }
		}

		public MiningModule(int order)
		{
			Id = GetHashCode();
			Order = order;
			CycleTime = 5f;
			//CycleTime = 60f;  << normal time.
			MiningAmount = 40;
		}

		public AsteroidMiningInfo StartMining(Asteroid asteroid)
		{
			IsActivated = true;
			_target = asteroid;
            _currentAsteroidMiningInfo = new AsteroidMiningInfo(asteroid, this, CycleTime);
			
			StartCycle();

			return _currentAsteroidMiningInfo;
		}
		
		public AsteroidMiningInfo StopMining(Asteroid asteroid)
		{
			_target = null;
			IsActivated = false;

			StopCycle();

			return _currentAsteroidMiningInfo;
		}

		private void StartCycle()
		{
			_currentCycle = RunCycle();
			_currentCycleCoroutine = _Coroutiner.Run(_currentCycle);
		}

		private void StopCycle()
		{
			if (_currentCycleCoroutine != null)
			{
				_Coroutiner.Stop(_currentCycleCoroutine);
                EventsController.Instance.ReportAsteroidMiningCycleFinished(_currentAsteroidMiningInfo);
			}

			_currentCycleCoroutine = null;
		}

		private IEnumerator RunCycle()
		{
			Debug.Log("Starting a " + CycleTime.ToString() + " long cycle");
			float runTime = CycleTime;
			
			while (runTime > 0f)
			{
				runTime -= Time.deltaTime;
				_currentAsteroidMiningInfo.SetCycleTime(runTime);
				EventsController.Instance.ReportAsteroidMiningUpdated(_currentAsteroidMiningInfo);
                yield return 0;
			}

			EventsController.Instance.ReportAsteroidMiningCycleFinished(_currentAsteroidMiningInfo);
			Debug.Log("Finishing cycle.");
			
			StartCycle();
		}
	}
}
