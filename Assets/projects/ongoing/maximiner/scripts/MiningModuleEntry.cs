
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
    public class MiningModuleEntry : MenuItem<MiningModule>
    {
        [SerializeField] private Text _text;

	    private MiningModule _miningModule;
	    
	    void OnEnable()
	    {
			EventsController.OnAsteroidMiningStarted += OnAsteroidMiningStarted;
			EventsController.OnAsteroidMiningStopped += OnAsteroidMiningStopped;
	    }

	    void OnDisable()
	    {
			EventsController.OnAsteroidMiningStarted -= OnAsteroidMiningStarted;
			EventsController.OnAsteroidMiningStopped -= OnAsteroidMiningStopped;
	    }
	    
        public override void Initialize(MiningModule miningModule)
        {
	        _miningModule = miningModule;
            _text.text = miningModule.Order.ToString();
        }
        
		public void OnAsteroidMiningStarted(Asteroid asteroid, MiningModule module)
		{
			if (module.Id != _miningModule.Id) return;
			StartCoroutine("UpdateEntry");
		}

		public void OnAsteroidMiningStopped(Asteroid asteroid, MiningModule module)
		{
			if (module.Id != _miningModule.Id) return;
			StopCoroutine("UpdateEntry");
		}

	    private IEnumerator UpdateEntry()
	    {
		    while (true)
		    {
			    _text.text = Random.Range(0, 10).ToString();
			    yield return new WaitForSeconds(1f);
		    }
	    }
		
    }
}
