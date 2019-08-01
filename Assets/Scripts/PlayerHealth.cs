using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void setIndicator(float diff) {
        this.bar.localScale = new Vector3(1f, 1f - diff);
    }
}
