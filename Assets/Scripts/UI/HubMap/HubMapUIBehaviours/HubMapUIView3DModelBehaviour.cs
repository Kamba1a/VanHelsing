using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace BeastHunter
{
    class HubMapUIView3DModelBehaviour : MonoBehaviour, IDragHandler, IDropHandler
    {
        private const float ROTATE_SPEED = 100.0f;

        public Action OnDropHandler { get; set; }
        public GameObject RotateObject { get; set; }

        public void OnDrag(PointerEventData eventData)
        {
            if (RotateObject != null)
            {
                float rotX = Mouse.current.position.x.ReadValue() * ROTATE_SPEED * Mathf.Deg2Rad; //Input.GetAxis("Mouse X")
                RotateObject.transform.Rotate(Vector3.up, -rotX);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnDropHandler?.Invoke();
        }
    }
}
