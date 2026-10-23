// using TowerDefence.PlayerInput;
// using UnityEngine;

// namespace TowerDefence.Core
// {
//     public class PlayerRotation : MonoBehaviour
//     {
//         [SerializeField] private float _rotationSpedd = 5f;

//         // private Rigidbody2D _rb;
//         private GetRotation _getRotation;
//         private InputManager _inputManager;
//         private bool _initializationError = false;
//         public void Initalize(InputManager inputManager)
//         {
//             if(inputManager == null)
//             {
//                 Debug.LogError("[PlayerRotation] Input Manager is null");
//                 _initializationError = true;
//                 return;
//             }

//             _getRotation = new GetRotation();
//             _inputManager = inputManager;
//         }

//         private void Update()
//         {
//             if(_initializationError) 
//             {
//                 Debug.LogError("[PlayerRotation] Initialization error! Check logs");
//                 return;
//             }

//             RotatePlayer();
//         }

//         private void RotatePlayer()
//         {
//             Vector2 objectPosition = transform.position;
//             Vector2 cursosPosition = _inputManager.GetMousePositionConverted();

//             Quaternion targetRotation = _getRotation.FromPositionToTarget(objectPosition, cursosPosition);

//             transform.rotation = Quaternion.RotateTowards(
//                 transform.rotation,
//                 targetRotation,
//                 _rotationSpedd * Time.deltaTime
//             );
//         }
//     }
// }