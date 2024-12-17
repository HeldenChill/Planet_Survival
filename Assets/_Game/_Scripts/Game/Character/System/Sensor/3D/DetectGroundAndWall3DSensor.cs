using UnityEngine;
namespace Dynamic.WorldInterface.Sensor
{
    using Data;
    using Utilities.Core.Character.WorldInterfaceSystem;
    public class DetectGroundAndWall3DSensor : BaseSensor
    {
        [SerializeField] protected float groundCheckRadius;
        [SerializeField] protected float wallCheckRadius;
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected Transform wallCheck;

        Collider[] colliders;

        private void Awake()
        {
            colliders = new Collider[1];
        }
        /// <summary>
        /// Create a circle to check collide with ground or not
        /// Create a raycast to check collide with wall or not
        /// </summary>
        public override void UpdateState()
        {
            Physics.Raycast(new Ray(groundCheck.position, -groundCheck.up), out Data.TouchingGroundPoint, groundCheckRadius, layer);
            Data.Normal = groundCheck.up;
            Data.IsGrounded = Physics.OverlapSphereNonAlloc(groundCheck.position, groundCheckRadius, colliders, layer) > 0;
            Data.IsTouchingWall = Physics.Raycast(new Ray(wallCheck.position, wallCheck.forward), out Data.TouchingWallPoint, wallCheckRadius, layer);
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.forward * wallCheckRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckRadius);
        }
    }
}