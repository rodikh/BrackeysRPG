using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour {

    public LayerMask movementMask;
    Camera cam;
    PlayerController playerController;

	void Start () {
        cam = Camera.main;
        playerController = GetComponent<PlayerController>();
	}
	
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

		if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                playerController.MoveToPoint(hit.point);
                playerController.RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    playerController.SetFocus(interactable);
                }
            }
        }
    }
}
