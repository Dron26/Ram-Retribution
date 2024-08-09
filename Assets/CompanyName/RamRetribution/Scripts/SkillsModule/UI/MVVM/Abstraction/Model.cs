using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Infrastructure;

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
