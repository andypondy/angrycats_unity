using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class singleTileBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	[SerializeField]
	Text answerDisplay;
	private bool isDragged;
	private equation eq;
	private Vector3 startPosition;
	private Vector2 moveToPosition;
	public static GameObject DraggedInstance;
 
	Vector3 _startPosition;
	Vector3 _offsetToMouse;
	float _zDistanceToCamera;

	// Use this for initialization
	void Start () {
		this.startPosition = this.transform.position;
		this.isDragged = false;
		this.moveToPosition = new Vector2(0,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void reset() {
        this.isDragged = false;
        this.transform.position = this.startPosition;
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
		_startPosition = transform.position;
		_zDistanceToCamera = Mathf.Abs (_startPosition.z - Camera.main.transform.position.z);

		_offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
		);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(Input.touchCount > 1)
			return;

		transform.position = Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
			) + _offsetToMouse;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		DraggedInstance = null;
		_offsetToMouse = Vector3.zero;
		transform.position = _startPosition;
	}
}
