using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Xibao;

namespace Xibao
{
	public class Xibao : Controller<Xibao>
	{
		public GameHandler gameHandler;
		public InputHandler inputHandler;
		public StageHandler stageHandler;
		public UIHandler uiHandler;
		
		void Start()
		{
			Debug.Log( "Starting game." );
			uiHandler.Init();
			gameHandler.SetUpGame();
		}

		void Update()
		{
			gameHandler.UpdateGame();
		}
	}

}
