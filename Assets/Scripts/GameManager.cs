using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

	[SerializeField]
	private TileManager refToTileManager;
	private SpawnManager refToSpawnManager;

	private GameObject bossHealth;
	private GameObject playerName;

	/* Level info
		to be moved to level mananger
	 */
	public int level=1;
	public int bossHP = 100;
	public int bossDamage = 20;
	public int difficulty = 1;
    public float spawnInterval = 2.0f;
	/* Level info
		to be moved to level mananger
	 */

	public int maxBombs = 4;
	public int bombRadius = 50;
	private int maxEquations = 4;
	private int currentBombs = 0;
	private int currentEquation = 0;
    private List<equation> equations;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		reset();

		// generate 5 equations to begin with
        for (var i=0; i < this.maxEquations; i++) {
            this.createMathEquation(this.difficulty);
        }
        // equation.debugEq(this.equations);
        this.refToSpawnManager = this.GetComponent<SpawnManager>();
        this.refToSpawnManager.game = this;
        this.refToTileManager.game = this;
   		this.refToTileManager.setupAnswers();

        // this.enemyHealthComponent = this.enemyHealth.getComponent('Health');
	}
	void reset() {
		this.currentBombs = 0;
		this.currentEquation = 0;
		this.equations = new List<equation>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        // Check if we have reached beyond 2 seconds.
        if (timer > spawnInterval) {
            timer = 0;
            // check if there are too many bombs on screen
            if (this.currentBombs < this.maxBombs) {
                this.refToSpawnManager.spawnNewBomb();
                this.currentBombs++;
            }
        }
	}

    public void bombMissed(equation eq) {
        // do some damage to Player

        // mark equation as missed
        this.equations[eq.id].status = equation.Status.missed;
        this.refToTileManager.setupAnswers();
        
        this.bombDestroyed();
    }

    public void bombDefused(equation eq) {
        // do some damage to Boss
        // this.enemyHealthComponent.takeDamage(this.catDamage);

        // mark equation as solved
        this.equations[eq.id].status = equation.Status.solved;
        this.refToTileManager.setupAnswers();

        this.bombDestroyed();
    }

    void bombDestroyed() {
        this.currentBombs--;
    }

	/**
     * Math Manager
     */
    private equation createMathEquation(int difficulty) {
        var x = Random.Range(1, 9);
        var y = Random.Range(1, 9);

        var answer = x + y;

        //generate unique id
        var eqString = "" + x + " + " + y;
        equation eq = new equation(this.equations.Count+1, x, y, answer, eqString, equation.Status.incomplete);//{x:x, y:y, answer:answer, eq: eq, id:this.equations.length, status:"incomplete"};

        this.equations.Add(eq);
        return eq;
    }

    public equation getNextEquation() {
        this.createMathEquation(this.difficulty);
        return this.equations[this.currentEquation++];
    }

    public List<equation> getNextEquations(int howmany) {
        var returnEquations = new List<equation>();
        for(var i=0; i < this.equations.Count; i++) {
            if (this.equations[i].status == equation.Status.incomplete) {
                returnEquations.Add(this.equations[i]);
            }
            if (returnEquations.Count == howmany) {
                break;
            }
        }
        if (returnEquations.Count < howmany) {
            for(var i=returnEquations.Count; i < howmany; i++) {
                returnEquations.Add(this.createMathEquation(this.difficulty));
            }
        }
        // equation.debugEq(returnEquations);
        return returnEquations;
    }
}
