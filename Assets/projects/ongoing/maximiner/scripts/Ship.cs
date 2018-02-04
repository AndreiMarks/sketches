using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maximiner
{
	public class Ship
	{
		public List<Module> modules = new List<Module>();
		
		#region Modules --------------------------------------------------

		public List<CargoModule> CargoModules
		{
			get
			{
				return modules.Where(m => m.Type == ModuleType.Cargo).Cast<CargoModule>().ToList();
			}
		}
		
		public List<MiningModule> MiningModules
		{
			get
			{
				return modules.Where(m => m.Type == ModuleType.Mining).Cast<MiningModule>().ToList();
			}
		}
		
		#endregion

		#region Statuses --------------------------------------------------

		public bool HasAvailableMiningModules
		{
			get { return CalculateAvailableMiningModules() > 0; }
		}
		
		public bool HasAvailableCargoSpace
		{
			get { return CargoHoldAvailableVolume > 0; }
		}

		public bool IsReadyToMine
		{
			get { return HasAvailableCargoSpace && HasAvailableMiningModules; }
		}
		
		#endregion

		public float CargoHoldAvailableVolume
		{
			get { return CalculateAvailableCargoVolume(); }
		}

		public Ship()
		{
			// Create default ship.
		}

		public void AddModule(Module module)
		{
			modules.Add(module);		
		}

		private float CalculateAvailableCargoVolume()
		{
			float availableVolume = 0f;
			
			foreach (CargoModule cm in CargoModules)
			{
				availableVolume = cm.AvailableVolume;
			}
			
			return availableVolume;
		}

		private int CalculateAvailableMiningModules()
		{
			List<MiningModule> miningModules = MiningModules.Where(mm => !mm.IsActivated).ToList();
			return miningModules.Count;
		}

		private MiningModule GetFirstAvailableMiningModule()
		{
			return MiningModules.FirstOrDefault(mm => !mm.IsActivated);
		}
		
		private CargoModule GetFirstAvailableCargoModule()
		{
			return CargoModules.FirstOrDefault(cm => cm.AvailableVolume > 0f);
		}

		#region Asteroid Mining --------------------------------------------------

		public bool TryAddOreToCargoModule(OreYieldInfo oyi)
		{
			if (!HasAvailableCargoSpace)
			{
				string warning = "You do not have enough cargo space. Ore jettisoned into space.";
				EventsController.Instance.ReportDoWarningMessage(warning);
				
				return false;
			}

			CargoModule cm = GetFirstAvailableCargoModule();
			
			OreYieldInfo remainder;
			if (!cm.TryAddOre(oyi, out remainder))
			{
				return TryAddOreToCargoModule(remainder);	
			}

			return true;
		}
		
		public bool IsMiningAsteroid(Asteroid asteroid)
		{
			return MiningModules.Any(mm => mm.Target != null && mm.Target.Id == asteroid.Id);
		}
		
		public void StartMiningAsteroid(Asteroid asteroid)
		{
			MiningModule miner = GetFirstAvailableMiningModule();
			AsteroidMiningInfo newInfo = miner.StartMining(asteroid);
			EventsController.Instance.ReportAsteroidMiningStarted(newInfo);
		}

		public void StopMiningAsteroid(Asteroid asteroid)
		{
			List<MiningModule> modules = MiningModules.Where(mm => mm.IsActivated).ToList();
			foreach (MiningModule mm in modules)
			{
				if (mm.Target.Id == asteroid.Id)
				{
					AsteroidMiningInfo info = mm.StopMining(asteroid);
                    EventsController.Instance.ReportAsteroidMiningStopped(info);
				}
			}
		}
		#endregion
	}
}
