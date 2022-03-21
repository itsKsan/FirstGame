using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayerController), typeof(WeaponController))]
public class Player : LivingEntity
{
    private PlayerController _controller;
    private AnimationController _animationController;
    private WeaponController _weaponController;
    private Camera _camera;
    
    
    [SerializeField][Range(0.1f, 10f)] private float moveSpeed = 4f;

    [SerializeField] private Transform weaponSlot;
    
    private void Awake()
    {
        _camera = Camera.main;
        _controller = GetComponent<PlayerController>();
        _weaponController = GetComponent<WeaponController>();
        _animationController = GetComponentInChildren<AnimationController>();
    }

    
    private void Update()
    {
        Attack();
        MovePlayer();
        RotatePlayer();
    }

    private void Attack()
    {
        _animationController.AttackAnimation(Input.GetMouseButton(0));
        
        if (Input.GetMouseButton(0))
        {
            _weaponController.Shoot();
        }
    }
    private void MovePlayer()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        _controller.Move(moveVelocity);
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _animationController.MoveAnimation(true);
        }
        else
        {
            _animationController.MoveAnimation(false);
        }
    }

    private void RotatePlayer()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        
        
        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            var point = ray.GetPoint(rayDistance);
            _controller.LookAt(point);
            
            // Weapon slot staff
            var heightCorrectionPoint = new Vector3(point.x, weaponSlot.position.y, point.z);
            weaponSlot.LookAt(heightCorrectionPoint);
        }
    }
}
