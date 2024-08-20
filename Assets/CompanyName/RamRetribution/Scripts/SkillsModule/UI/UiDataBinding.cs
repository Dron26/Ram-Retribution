using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM.Abstraction;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.UI
{
    public class UiDataBinding
    {
        private readonly Model _uiModel;
        
        public UiDataBinding(Model uiModel) 
            => _uiModel = uiModel;

        public void SetNewDataForGame(params ISpell[] skills)
        {
            _uiModel.SetSkills(skills);
        }
    }
}