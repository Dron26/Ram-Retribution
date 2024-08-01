using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.Skills.MVVM
{
    public class DefaultView : View
    {

        [SerializeField] GameObject _skillsPrefab; // prefab ����� = ��� ������ ���� Image � Buttone �����������
        [SerializeField] Transform _skillsContainerParentTransform; //� ������ laoutGroup ���������� �����
        protected override void PrintActiveSkills(ISkill[] skills)
        {
            foreach (var skill in skills)
            {
                GameObject skillObject = Instantiate(_skillsPrefab, _skillsContainerParentTransform);
                if (skillObject.TryGetComponent(out Image image))
                    image.sprite = skill.SkillImage;
                if (skillObject.TryGetComponent(out Button button))
                    button.onClick.AddListener(() => _viewModel.OnActiveSpellButt0oneClicked(skill));
            }

        }
    }
}
