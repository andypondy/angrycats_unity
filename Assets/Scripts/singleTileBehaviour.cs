using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class singleTileBehaviour : MonoBehaviour {

	[SerializeField]
	Text answerDisplay;
	private bool isDragged;
	private equation eq;
	private Vector3 startPosition;
	private Vector2 moveToPosition;

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
}
