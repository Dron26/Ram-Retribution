using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM.Abstraction;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.UI.MVVM
{
    public class DefaultView : View
    {
        [SerializeField] GameObject _skillPrefab; 
        [SerializeField] Transform _skillsContainerParent;

        protected override void ShowSkills(ISkill[] skills)
        {
            PrintActiveSkills(skills).Forget();
        }

        private async UniTaskVoid PrintActiveSkills(ISkill[] skills)
        {
            await UniTask.WaitUntil(() => Services.LeaderTransform == null);

            foreach (var skill in skills)
            {
                var instance = Instantiate(_skillPrefab, _skillsContainerParent);

                if (instance.TryGetComponent(out Image image))
                    image.sprite = skill.Image;
                
                if(instance.TryGetComponent(out Button button))
                    button.onClick.AddListener(() => ViewModel.OnActiveSpellButtonClicked(skill));
            }
        }
    }
}
