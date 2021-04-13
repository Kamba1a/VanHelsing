using UnityEngine;
using UnityEngine.EventSystems;


namespace BeastHunter
{
    class HubMapUIView3DModelBehaviour : MonoBehaviour, IDragHandler
    {
        private const float ROTATE_SPEED = 100.0f;

        public GameObject RotateObject { get; set; }

        public void OnDrag(PointerEventData eventData)
        {
            if (RotateObject != null)
            {
                float rotX = Input.GetAxis("Mouse X") * ROTATE_SPEED * Mathf.Deg2Rad;
                RotateObject.transform.Rotate(Vector3.up, -rotX);
            }
        }

        //todo: equipment items find slots by drop on this object
    }
}
