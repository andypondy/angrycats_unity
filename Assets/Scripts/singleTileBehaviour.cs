using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class singleTileBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField]
	Text answerDisplay;
	public equation eq;
	private Vector3 startPosition;
	public static GameObject DraggedInstance;
	Vector3 _offsetToMouse;
	float _zDistanceToCamera;
	private bool returnToStart = false;

	// Use this for initialization
	void Start () {
		this.startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (returnToStart) {
			// Set our position as a fraction of the distance between the markers.
			transform.position = Vector3.Lerp(transform.position, this.startPosition, 0.1f);
		}
		if (Mathf.Abs(transform.position.y - this.startPosition.y) < 0.5) {
			transform.position = this.startPosition;
			returnToStart = false;
		}
	}

    public void setEq(equation eq) {
        this.eq = eq;
        this.answerDisplay.text = this.eq.answer.ToString();
    }

	/*
		Touch handlers
	*/
	public void OnBeginDrag (PointerEventData eventData)
	{
		DraggedInstance = gameObject;
		_zDistanceToCamera = Mathf.Abs (this.startPosition.z - Camera.main.transform.position.z);

		_offsetToMouse = this.startPosition - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
		);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(Input.touchCount > 1)
			return;

		if (DraggedInstance == null)
			return;

		transform.position = Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
			) + _offsetToMouse;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		DraggedInstance = null;
		_offsetToMouse = Vector3.zero;
		returnToStart = true;
	}
}
