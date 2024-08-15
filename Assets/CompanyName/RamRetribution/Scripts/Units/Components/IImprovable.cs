namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public interface IImprovable
    {
        public void Improve(float bonusValue, ref float field)
        {
            field += bonusValue;
        }

        public void UnImprove(float decreaseValue, ref float field)
        {
            field -= decreaseValue;
        }
    }
}