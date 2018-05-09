﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item {

	protected override void ActionToPlayer (GameObject go, int action) {
		hitAudio.Play ();
		base.ActionToPlayer (go, 2);
	}
}