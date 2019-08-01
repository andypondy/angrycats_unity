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
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
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
		onCollisionHander(collision);
	}

	void OnCollisionStay2D(Collision2D collision) {
		onCollisionHander(collision);
    }

	void onCollisionHander(Collision2D collision) {
		if (collision.gameObject.tag == "ground")
        {
            this.spawnmanager.game.bombMissed(this.eq);
			SimplePool.Despawn(this.gameObject);
        }
		else {
			if (collision.gameObject.tag == "tile")
			{
				singleTileBehaviour tile = collision.gameObject.GetComponent<singleTileBehaviour>();
				tile.OnEndDrag(null);

				if (tile.eq.answer == this.eq.answer) {
					this.spawnmanager.game.bombDefused(this.eq);

					// shot animation
					animator.Play("balloon_splash");

					Invoke("postSplash", 0.5f);
				}
			}
		}
	}

	void postSplash() {
		SimplePool.Despawn(this.gameObject);
	}
}
