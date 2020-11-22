using UnityEngine;

namespace CoxlinCore
{
    public abstract class MovementController2D : MovementController
    {
        [SerializeField]
        private CharaAnim2D _charaAnim;

        public override void Move(Vector3 move)
        {
            if (move == Vector3.zero)
            {
                move = Vector3.down;
            }
            move = DirectionUtils.ClosestDirection(move);
            _charaAnim.SetMoveAnim(move);
        }

        public override void Idle(Vector3 move)
        {
            if (move == Vector3.zero)
            {
                move = Vector3.down;
            }
            move = DirectionUtils.ClosestDirection(move);
            _charaAnim.SetIdleAnim(move);
        }
    }
}
