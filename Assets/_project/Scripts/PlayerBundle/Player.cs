using System;
using _project.Scripts.PhaseLogic;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    public class Player : MonoBehaviour
    {
        public PhaseManager phaseManager { get; private set; }

        private void Awake()
        {
            phaseManager = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
