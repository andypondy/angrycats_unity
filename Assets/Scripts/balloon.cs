using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class balloon : MonoBehaviour {

	[SerializeField]
	public int dropSpeed;
	[SerializeField]
	Text equationDisplay;
	public SpawnManager spawnmanager;

	private equation eq;
	private Vector2 target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setEq (equation eq) {
        this.eq = eq;
        this.equationDisplay.text = this.eq.equationString;

        // var randomIdx = Helpers.getRandomInt(0, this.spriteList.length);
        // var sprite = this.getComponent(cc.Sprite);
        // sprite.spriteFrame = this.spriteList[randomIdx];
    }

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "ground")
        {
			Debug.Log("balloon.cs - removing ");
			equation.debugEq(this.eq);
			
            this.spawnmanager.game.bombMissed(this.eq);
			SimplePool.Despawn(this.gameObject);
        }
		else {
			if (collision.gameObject.tag == "tile")
			{

			}
		}
	}
}
