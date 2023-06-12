using System.Collections;
using UnityEngine;

enum GameEventEnum
{
    None = -1,
    Leaf,
    Fish,
    WaterFall,
    Count
}

public class GameEventManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LeafEvent _leafEvent = null;
    [SerializeField] private FishManager _FishEvent = null;
    [SerializeField] private WaterFallManager _waterFallEvent = null;

    [Header("Varibles")]
    [SerializeField] private Vector2 _waitTimeRange = new Vector2(10.0f, 45.0f);

    private GameEventEnum _currentGameEvent = GameEventEnum.None;
    private IGameEvent _currentEvent = null;

    public void StartRandomEvents()
    {
        StartCoroutine(PlayEvent());
    }

    private IEnumerator PlayEvent()
    {
        yield return new WaitForSeconds(Random.Range(_waitTimeRange.x, _waitTimeRange.y));

        GameEventEnum newGameEvents;
        while (true)
        {
            newGameEvents = (GameEventEnum)Random.Range(0, (int)GameEventEnum.Count);

            if(newGameEvents != _currentGameEvent)
            {
                _currentGameEvent = newGameEvents;
                break;
            }
            yield return null;
        }

        // Stop current event
        if (_currentEvent != null)
        {
            _currentEvent.StopGameEvent();
        }

        switch (_currentGameEvent) 
        {
            case GameEventEnum.Leaf:
                _currentEvent = _leafEvent as IGameEvent;
                break;
            case GameEventEnum.Fish:
                _currentEvent = _FishEvent as IGameEvent;
                break;
            case GameEventEnum.WaterFall:
                _currentEvent = _waterFallEvent as IGameEvent;
                break;
        }

        yield return new WaitForSeconds(Random.Range(_waitTimeRange.x, _waitTimeRange.y));

        _currentEvent.StartGameEvent();
        StartCoroutine(PlayEvent());
    }


}
