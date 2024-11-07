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
    }
}