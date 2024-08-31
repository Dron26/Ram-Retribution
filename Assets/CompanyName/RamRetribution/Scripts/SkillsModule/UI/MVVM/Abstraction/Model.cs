using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM.Abstraction
{
    public abstract class Model
    {
        public readonly ReactiveProperty<ISpell[]> ModelSkillsContainer = new ReactiveProperty<ISpell[]>();
        
        public void SetSkills(ISpell[] skills)
        {
            ModelSkillsContainer.Value = skills;
        }
    }
}
