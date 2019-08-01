using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    protected override void setIndicator(float diff) {
        this.bar.localScale = new Vector3(diff, 1f);
    }
}
