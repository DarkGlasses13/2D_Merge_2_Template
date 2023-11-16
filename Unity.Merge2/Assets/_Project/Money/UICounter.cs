using TMPro;
using UnityEngine;
using Zenject;

namespace Assets._Project.Money
{
    public class UICounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        private MoneyFormater _formater;

        [Inject]
        public void Construct(MoneyFormater formater) => _formater = formater;

        public void Set(ulong value) => _value.text = _formater.Format(value);
    }
}