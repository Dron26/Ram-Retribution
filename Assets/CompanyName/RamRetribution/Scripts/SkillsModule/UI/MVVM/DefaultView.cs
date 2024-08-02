using System;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.Skills.MVVM
{
    public class DefaultView : View
    {
        [SerializeField] GameObject _skillsPrefab; // prefab ����� = ��� ������ ���� Image � Buttone �����������
        [SerializeField] Transform _skillsContainerParentTransform; //� ������ laoutGroup ���������� �����

        protected override void ShowSkills(ISkill[] skills)
        {
            PrintActiveSkills(skills).Forget();
        }

        private async UniTaskVoid PrintActiveSkills(ISkill[] skills)
        {
            await UniTask.WaitUntil(() => Services.LeaderTransform == null);

            foreach (var skill in skills)
            {
                GameObject skillObject = Instantiate(_skillsPrefab, _skillsContainerParentTransform);
                
                skillObject.GetComponentInChildren<Image>().sprite = skill.SkillImage;
                
                skillObject.GetComponentInChildren<Button>()
                    .onClick
                    .AddListener(() => _viewModel.OnActiveSpellButt0oneClicked(skill));
            }
        }
    }
}
