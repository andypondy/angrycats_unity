using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	[SerializeField]
	private GameObject[] tiles = new GameObject[5];
	private int maxTiles=5;
	// Use this for initialization
	private List<equation> equations;
	public GameManager game;
	void Start () {
	}

	public void setupAnswers(bool shuffle = false) {
        var eqs = this.game.getNextEquations(this.maxTiles);
        if (shuffle) {
            this.equations = this.shuffle(eqs);
        }
        else {
            this.equations = eqs;
        }
        for(var i=0; i < this.maxTiles; i++) {
            singleTileBehaviour newTile = this.tiles[i].GetComponent<singleTileBehaviour>();
            newTile.setEq(this.equations[i]);
        }
    }

	List<equation> shuffle(List<equation> eqs) {
        int total = eqs.Count;

        int[] order = new int[total];
        for (int i=0; i < total; i++) {
            order[i] = -1;
        }
        
		bool isfree = true;
        for (int toset=0; toset < total; toset++) {
            int x = Random.Range(0, total);
            for (int tocheck=0; tocheck < total; tocheck++) {
                isfree = true;
                if (order[tocheck] == x) {
                    isfree = false;
                    break;
                }
            }
            if (isfree) {
                order[toset] = x;
            }
            else {
                toset--;
            }
        }
        
        List<equation> returnEquations = new List<equation>();
        for (int toset=0; toset < total; toset++) {
            returnEquations.Add(eqs[order[toset]]);
        }
        return returnEquations;
    }
}
