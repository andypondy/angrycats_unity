using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	private float groundY;
	private float startingY;

	private int[] spawnPositions;
	private int lastSpawnIndex = 0;
    [SerializeField]
    GameObject[] balloonPrefabs;

    public GameManager game;

	// Use this for initialization
	public void Start () {
		Debug.Log("Screen Width : " + Screen.width + ", " + Screen.height);

		int maxSpawnPositions = 4;
        int zero = 24;
        var padding = ((Screen.width/maxSpawnPositions) - 30)/2;
        var onefourth = (Screen.width/maxSpawnPositions);

        spawnPositions = new int[maxSpawnPositions];
		for(int i=0; i < maxSpawnPositions; i++) {
            this.spawnPositions[i] = Mathf.FloorToInt(zero + padding + onefourth*i);
        }

        this.groundY = 30;
	}

	/**
     * Spawn Manager
     */
    public void spawnNewBomb () {
        
        Vector2 startPosition = this.getNewStartPosition();

        GameObject b  = this.balloonPrefabs[0];
        // generate a new node in the scene with a preset template
        var newBalloon = SimplePool.Spawn(this.balloonPrefabs[0], new Vector3(startPosition.x,startPosition.y,0), Quaternion.identity);

        // set math equation
        newBalloon.GetComponent<balloon>().setEq(this.game.getNextEquation());
        newBalloon.GetComponent<balloon>().spawnmanager = this;

    }

    Vector2 getNewStartPosition () {
        var randX = 0;
        // According to the position of the ground level and the main character's jump height, randomly obtain an anchor point of the bomb on the y axis
        var randY = Screen.height;
        // according to the width of the screen, randomly obtain an anchor point of bomb on the x axis
        // randX = (Math.random() - 0.5) * (this.game.ground.width - this.game.bombRadius);
        var newIndex = false;
        while(!newIndex) {
            int x = Mathf.FloorToInt(Random.Range(0, this.spawnPositions.Length));
            if (x != this.lastSpawnIndex) {
                this.lastSpawnIndex = x;
                newIndex = true;
            }
        }
        randX = this.spawnPositions[this.lastSpawnIndex];
        // return to the anchor point of the balloon
        return new Vector2(randX, randY);
    }

    public void returnBomb (GameObject balloon) {
        // console.log('anand - return balloon ');
        SimplePool.Despawn(balloon);
    }
}
