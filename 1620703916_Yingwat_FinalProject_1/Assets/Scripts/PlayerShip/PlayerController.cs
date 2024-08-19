using Spaceship;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerShip
{
    public class PlayerController : MonoBehaviour
    {
        //[SerializeField] private float playerShipSpeed = 10;
        [SerializeField] private PlayerSpaceship playerSpaceship;
        private Vector2 movementInput = Vector2.zero;
        private ShipInputActions inputAction;
        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;
        private float padding = 1;

        private void Awake()
        {
            InitInput();
            CreateMovementBoundary();
        }

        private void InitInput()
        {
            inputAction = new ShipInputActions();
            inputAction.Player.Move.performed += OnMove;
            inputAction.Player.Move.canceled += OnMove;
            inputAction.Player.Fire.performed += OnFire;
        }

        private void OnFire(InputAction.CallbackContext obj)
        {
           playerSpaceship.Fire();
        }

        public void OnMove(InputAction.CallbackContext obj)
        {
            if (obj.performed)
            {
                movementInput = obj.ReadValue<Vector2>();
            }
            if (obj.canceled)
            {
                movementInput = Vector2.zero;
            }
            
            Debug.Log(obj);
        }

        /*void Start()
        {
            Application.targetFrameRate = 240;
            SetupMoveBounderies();
            
        }*/
        
        void Update()
        {
            Move();
        }

        /*private void SetupMoveBounderies()
        {
            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector2(1, 0)).x - padding;
            
            yMin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector2(1, 1)).y - padding;
            
        }*/

        private void Move()
        {
            /*movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            movementInput = movementInput.normalized;

            var inputDirection = movementInput.normalized;
            var inputVelocity = inputDirection * playerShipSpeed;
            var gravityForce = new Vector2(0, 2);
            var blackholeForce = new Vector2(-2, 0);

            var finalVelocity = inputVelocity + gravityForce + blackholeForce;

            Debug.DrawRay(transform.position,inputVelocity,Color.green);
            Debug.DrawRay(transform.position,gravityForce,Color.red);
            Debug.DrawRay(transform.position,blackholeForce,Color.yellow);
            Debug.DrawRay(transform.position,finalVelocity,Color.white);
            Debug.Log(movementInput);
            
            
            var newX = transform.position.x + finalVelocity.x*Time.deltaTime;
            var newY = transform.position.y + finalVelocity.y*Time.deltaTime;

            newX = Mathf.Clamp(newX, xMin, xMax);
            newY = Mathf.Clamp(newY, yMin, yMax);
            
            transform.position = new Vector2(newX, newY);
            
            var distanceX = movementInput.x*Time.deltaTime*playerShipSpeed;
            var distanceY = movementInput.y*Time.deltaTime*playerShipSpeed;
            transform.Translate(new Vector2(distanceX, distanceY));*/
            
            var inputVelocity = movementInput * playerSpaceship.Speed;
            
            var newPosition = transform.position;
            newPosition.x = transform.position.x + inputVelocity.x * Time.smoothDeltaTime;
            newPosition.y = transform.position.y + inputVelocity.y * Time.smoothDeltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, xMin ,xMax);
            newPosition.y = Mathf.Clamp(newPosition.y, yMin ,yMax);

            transform.position = newPosition;
        }

        private void CreateMovementBoundary()
        {
            var mainCamera = Camera.main;

            var spriteRenderer = playerSpaceship.GetComponent<SpriteRenderer>();

            var offset = spriteRenderer.bounds.size;
            xMin = mainCamera.ViewportToWorldPoint(mainCamera.rect.min).x + offset.x / 2;
            xMax = mainCamera.ViewportToWorldPoint(mainCamera.rect.max).x + offset.x / 2;
            yMin = mainCamera.ViewportToWorldPoint(mainCamera.rect.min).y + offset.y / 2;
            yMax = mainCamera.ViewportToWorldPoint(mainCamera.rect.max).y + offset.y / 2;
        }

        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }
    }
}
