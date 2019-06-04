using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class equation {
    public enum Status
    {
        incomplete,
        solved,
        failed,
        missed
    }
    public int positionX;
    public int positionY;
    public int answer;
    public string equationString;
    public int id {get;set;}
    public Status status;

    public equation(int id, int x, int y, int answer, string eqString, Status st) {
        this.id = id;
        this.positionX = x;
        this.positionY = y;
        this.answer = answer;
        this.equationString = eqString;
        this.status = st;
    }

	public static void debugEq(List<equation> equations) {
        for(var i=0; i < equations.Count; i++) {
            Debug.Log("equation:  " + equations[i].positionX + "+" + equations[i].positionY + "=" + equations[i].answer + ", status=" + equations[i].status);
        }
    }
	public static void debugEq(equation eq) {
        Debug.Log("equation:  " + eq.positionX + "+" + eq.positionY + "=" + eq.answer + ", status=" + eq.status);
    }

}