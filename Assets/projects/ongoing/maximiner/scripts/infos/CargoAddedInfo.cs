using Maximiner;

public class CargoAddedInfo
{
    public CargoModule module;
    public CargoLoadInfo cargoLoadInfo;

    public CargoAddedInfo(CargoModule module, CargoLoadInfo cargoLoadInfo)
    {
        this.module = module;
        this.cargoLoadInfo = cargoLoadInfo;
    }
}
