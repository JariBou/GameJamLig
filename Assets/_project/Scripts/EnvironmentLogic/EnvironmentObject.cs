using UnityEngine;

namespace _project.Scripts.EnvironmentLogic
{
    public class EnvironmentObject : MonoBehaviour
    {
        [SerializeField] private EnvironmentType _environmentType;

        public bool IsOfType(EnvironmentType environmentType)
        {
            return _environmentType.HasFlag(environmentType);
        }

        public void SetEnvironmentType(EnvironmentType environmentType)
        {
            _environmentType = environmentType;
        }

        public void AddEnvironmentType(EnvironmentType environmentType)
        {
            _environmentType |= environmentType;
        }

        public void RemoveEnvironmentType(EnvironmentType environmentType)
        {
            _environmentType &= ~environmentType;
        }
    }
}