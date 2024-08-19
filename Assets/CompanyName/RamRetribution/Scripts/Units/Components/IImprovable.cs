namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public interface IImprovable
    {
        public void Improve(ref float field, float bonusValue)
        {
            field += bonusValue;
        }

        public void UnImprove(ref float field, float decreaseValue)
        {
            field -= decreaseValue;
        }
    }
}