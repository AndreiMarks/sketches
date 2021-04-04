using System.Threading.Tasks;
using Bones.GameOfLife;
using UnityEngine;

public class ConwayCubes : MonoBehaviour
{
    [SerializeField] private int _columns;
    [SerializeField] private int _rows;

    [SerializeField] private float _turnDuration;
    
    [SerializeField] private ConwayCubesDisplay _display;
    [SerializeField] private Camera _camera;
    
    private GameOfLife _gameOfLife;
    
    private void Start()
    {
        _gameOfLife = new GameOfLife(_columns, _rows, _display);

        UpdateCameraPosition(_display.GridBounds);
        UpdateAsync();
    }

    private void UpdateCameraPosition(Bounds bounds)
    {
        Debug.Log("WTF");
        Debug.Log(bounds);
        float x = bounds.center.x;
        float y = _camera.transform.position.y;
        float z = bounds.center.z;

        Debug.Log(new Vector3(x, y, z));
        _camera.transform.position = new Vector3(x, y, z);
    }
    
    private async Task UpdateAsync()
    {
        while (true)
        {
            await Task.Delay((int) (_turnDuration * 1000));
            _gameOfLife.Update();
        }
    }
}
