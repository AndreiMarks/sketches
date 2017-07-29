using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReflectionCubeSpawner : ReflectionsBehaviour 
{
    private const int _WEEK_DURATION_IN_DAYS = 7;

    public ReflectionCube reflectionCubePrefab;
    public int initSpawnCount = 10;

    public SpawnPattern spawnPattern;

    private List<ReflectionCube> _currentReflectionCubes = new List<ReflectionCube>();

    private void SpawnCubes()
    {
        _currentReflectionCubes.Clear();

        ReflectionEntry[] reflections = _Reflections.CurrentReflectionCollection.entries;
        
        int lifetime = 0;
        // https://www.ssa.gov/cgi-bin/longevity.cgi
        float expectedYears = 82.2f;
        float expectedDays = expectedYears * 365;

        lifetime = Mathf.RoundToInt( expectedDays ); 

        switch ( spawnPattern ) 
        {
            case ( SpawnPattern.LinearHorizontal ):
                SpawnInDirection( reflections, Vector3.right );
            break;
            case ( SpawnPattern.LinearForward ):
                SpawnInDirection( reflections, Vector3.forward );
            break;
            case ( SpawnPattern.Weekly ):
                SpawnByTimePeriod( reflections, _WEEK_DURATION_IN_DAYS );
            break;
        }
    }

    private void SpawnInDirection( ReflectionEntry[] reflections, Vector3 direction )
    {
        float width = 1.5f;
        for( int i = 0; i < reflections.Length; i++ )
        {
            Vector3 spawnPos = direction * width * i;
            ReflectionCube newCube = SpawnCubeAtPosition( reflections[i], spawnPos );
            _currentReflectionCubes.Add( newCube );
        }
    }

    private void SpawnByTimePeriod( ReflectionEntry[] reflections, int timePeriodInDays )
    {
        float width = 1.5f;

        int timeCounter = 0;
        int timeCount = 0;

        for( int i = 0; i < reflections.Length; i++ )
        {
            Vector3 spawnPos = Vector3.right * timeCounter * width + Vector3.down * timeCount * width;

            ReflectionCube newCube = SpawnCubeAtPosition( reflections[i], spawnPos );
            _currentReflectionCubes.Add( newCube );

            if ( timeCounter == timePeriodInDays - 1 ) timeCount++;
            timeCounter = ( timeCounter + 1 ) % timePeriodInDays;
        }
    }

    private ReflectionCube SpawnCubeAtPosition( ReflectionEntry reflection, Vector3 spawnPos )
    {
        ReflectionCube cube = (ReflectionCube) Instantiate( reflectionCubePrefab, spawnPos, Quaternion.identity );
        cube.Initialize( reflection );
        cube.gameObject.name = spawnPos.ToString();

        reflection.ReflectionCube = cube;

        return cube;
    }

    public ReflectionCube GetCubeByIndex( int index )
    {
        if ( index >= _currentReflectionCubes.Count ) return null;
        return _currentReflectionCubes[index];
    }

    public enum SpawnPattern
    {
        LinearHorizontal,
        LinearForward,
        Weekly
    }
}
