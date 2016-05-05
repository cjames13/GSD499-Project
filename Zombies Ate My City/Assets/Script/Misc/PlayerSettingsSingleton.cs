using UnityEngine;
using System.Collections;

public class PlayerSettingsSingleton {
	private static readonly PlayerSettingsSingleton instance = new PlayerSettingsSingleton();

	// Current control scheme
	private ControlContext _controlContext = new ControlContext (new ThirdPersonControl ());
	public ControlContext controlContext {
		get { return _controlContext; }
		set { _controlContext = value; }
	}

	public static PlayerSettingsSingleton Instance {
		get { return instance; }
	}

	static PlayerSettingsSingleton(){}
	private PlayerSettingsSingleton(){}

}
