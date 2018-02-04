using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Maximiner;
using UnityEngine;

namespace Maximiner
{
	public class CargoModule : Module
	{
		#region General Module Properties --------------------------------------------------
		
		public override int Id { get; protected set; }

		public override ModuleType Type
		{
			get { return ModuleType.Cargo; }
		}
		
		#endregion

		private List<OreYieldInfo> _oreYieldInfos = new List<OreYieldInfo>();
		
		private float _totalVolume;
		public float TotalVolume
		{
			get { return _totalVolume; }
		}

		public float AvailableVolume
		{
			get { return TotalVolume - OccupiedVolume; }
		}

		public float OccupiedVolume
		{
			get { return _oreYieldInfos.Sum(oyi => oyi.volume); }
		}

		public CargoModule(float totalVolume)
		{
			_totalVolume = totalVolume;
		}

		public bool TryAddOre(OreYieldInfo oyi, out OreYieldInfo remainingYield)
		{
			if (oyi.volume > AvailableVolume)
			{
				float remaining = oyi.volume - AvailableVolume;
				remainingYield = oyi.GetFractionalClone(remaining);

				OreYieldInfo allowedYield = oyi.GetFractionalClone(AvailableVolume);
				AddOre(allowedYield);
				
				return false;
			}

			remainingYield = default (OreYieldInfo);
			AddOre(oyi);
			
            return true;
		}

		private void AddOre(OreYieldInfo oyi)
		{
			_oreYieldInfos.Add(oyi);
			CargoAddedInfo cai = new CargoAddedInfo(this, oyi);
			EventsController.Instance.ReportCargoAddedContents(cai);
		}
	}
}
