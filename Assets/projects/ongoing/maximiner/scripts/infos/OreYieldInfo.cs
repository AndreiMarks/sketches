namespace Maximiner
{
	public class OreYieldInfo : CargoLoadInfo
	{
		public AsteroidType type;
		public float units;

		public OreYieldInfo(AsteroidType type, float units, float volume)
		{
			this.type = type;
			this.units = units;
			this.volume = volume;
		}

		public OreYieldInfo GetFractionalClone(float volume)
		{
			float units = volume / this.type.GetVolume();
			return new OreYieldInfo(this.type, units, volume);
		}
	}
}
