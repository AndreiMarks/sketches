namespace Maximiner
{
	public class AsteroidMiningInfo
	{
		private Asteroid _asteroid;
		public Asteroid Asteroid
		{
			get { return _asteroid; }
		}

		private MiningModule _miningModule;
		public MiningModule MiningModule
		{
			get { return _miningModule; } 
		}
		
		private float _totalCycleTime;
		private float _currentCycleRunTime;

		public float CycleTimeRemaining
		{
			get { return _totalCycleTime - _currentCycleRunTime; }
		}

		public AsteroidMiningInfo(Asteroid asteroid, MiningModule miningModule, float cycleTime)
		{
			_asteroid = asteroid;
			_miningModule = miningModule;
			_totalCycleTime = cycleTime;
			_currentCycleRunTime = 0f;
		}

		public OreYieldInfo GetCycleOreYieldInfo()
		{
			float amount = MiningModule.MiningAmount;
			float units = amount / Asteroid.OreVolume;
			
			return new OreYieldInfo(Asteroid.Type, units, amount);
		}
		
		public void SetCycleTime(float time)
		{
			_currentCycleRunTime = _totalCycleTime - time;
		}
	}
}
