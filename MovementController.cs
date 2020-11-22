using UnityEngine;

namespace CoxlinCore
{
    public abstract class MovementController : MonoBehaviour
    {
        protected bool _canMove = false;
        protected Vector3 _oldPos;
        protected Vector3 _moveDir = Vector3.down;
        public Vector3 MoveDir => _moveDir;
        protected bool _moving;
        public bool Moving => _moving;

        public void EnableMove()
        {
            _canMove = true;
        }

        public void DisableMove()
        {
            Idle(_moveDir);
            _canMove = false;
        }

        protected void SetMoveDir(Vector3 moveDir)
        {
            _moveDir = moveDir;
        }

        private void Update()
        {
            if (!_canMove)
            {
                return;
            }
            _moving = _oldPos != transform.position;
            if (_moving)
            {
                Move(_moveDir);
            }
            else
            {
                Idle(_moveDir);
            }
        }


        public abstract void Move(Vector3 move);
        public abstract void Idle(Vector3 move);
    }
}
