namespace Maximiner
{
    public static class AsteroidExtensions
    {
        public static float GetVolume(this AsteroidType type)
        {
            float volume = 0f;

            switch (type)
            {
                case(AsteroidType.Alpha):
                    volume = .1f;
                    break;

                case(AsteroidType.Beta):
                    volume = .15f;
                    break;

                case(AsteroidType.Gamma):
                    volume = .3f;
                    break;
            }

            return volume;
        }

    }
}
