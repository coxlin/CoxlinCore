using UnityEngine;

namespace CoxlinCore
{
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour
    {
        private Health _internalHealth;
        public Health Health
        {
            get
            {
                if (_internalHealth == null)
                {
                    _internalHealth = GetComponent<Health>();
                }
                return _internalHealth;
            }
        }
        public enum State
        {
            Patrolling,
            Investigate,
            Combat,
            LostPlayer
        }

        private State _currentState = State.Patrolling;
        protected void SetState(State state)
        {
            _currentState = state;
        }

        private void Awake()
        {
            Health.AliveCheck(OnDie);
        }

        private void Update()
        {
            switch (_currentState)
            {
                case State.Patrolling:
                    {
                        PatrolUpdate();
                        break;
                    }
                case State.Investigate:
                    {
                        InvestigateUpdate();
                        break;
                    }
                case State.Combat:
                    {
                        CombatUpdate();
                        break;
                    }
                case State.LostPlayer:
                    {
                        LostPlayerUpdate();
                        break;
                    }
            }
        }

        public abstract void PatrolUpdate();

        public abstract void InvestigateUpdate();

        public abstract void CombatUpdate();

        public abstract void LostPlayerUpdate();

        public abstract void OnPlayerGlimpse();

        public abstract void OnPlayerSeen();

        public abstract void OnDie();
    }
}
