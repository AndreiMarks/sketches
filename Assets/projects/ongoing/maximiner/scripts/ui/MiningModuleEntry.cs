
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Maximiner
{
    public class MiningModuleEntry : MenuItem<MiningModule>
    {
        [SerializeField] private Text _text;
	    [SerializeField] private Image _backgroundImage;
	    
		[SerializeField] private Color _activeColor;
		[SerializeField] private Color _inactiveColor;

	    private MiningModule _miningModule;
	    
	    void OnEnable()
	    {
			EventsController.OnAsteroidMiningStarted += OnAsteroidMiningStarted;
			EventsController.OnAsteroidMiningStopped += OnAsteroidMiningStopped;
			EventsController.OnAsteroidMiningUpdated += OnAsteroidMiningUpdated;
	    }

	    void OnDisable()
	    {
			EventsController.OnAsteroidMiningStarted -= OnAsteroidMiningStarted;
			EventsController.OnAsteroidMiningStopped -= OnAsteroidMiningStopped;
			EventsController.OnAsteroidMiningUpdated -= OnAsteroidMiningUpdated;
	    }
	    
        public override void Initialize(MiningModule miningModule)
        {
	        _miningModule = miningModule;
            _text.text = miningModule.Order.ToString();
        }
        
		public void OnAsteroidMiningStarted(AsteroidMiningInfo ami)
		{
			if (ami.MiningModule.Id != _miningModule.Id) return;
			_backgroundImage.color = _activeColor;
		}

		public void OnAsteroidMiningStopped(AsteroidMiningInfo ami)
		{
			if (ami.MiningModule.Id != _miningModule.Id) return;
			_backgroundImage.color = _inactiveColor;
		}

	    public void OnAsteroidMiningUpdated(AsteroidMiningInfo ami)
	    {
		    if (ami.MiningModule.Id != _miningModule.Id) return;
		    _text.text = ami.CycleTimeRemaining.ToString("F0");
	    }
    }
}
