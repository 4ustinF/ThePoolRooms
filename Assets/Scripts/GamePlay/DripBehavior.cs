using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripBehavior : MonoBehaviour
{
    [SerializeField] private Vector2 _waitTime = new Vector2(1.0f, 1.5f);
    [SerializeField] private List<GameObject> _waterDriplets = new List<GameObject>();

    private void Awake()
    {
        foreach(var drip in _waterDriplets)
        {
            drip.SetActive(false);
        }

        StartCoroutine(DripRandomRoutine(Random.Range(_waitTime.x, _waitTime.y)));
    }

    private IEnumerator DripRandomRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        while(true)
        {
            GameObject drip = _waterDriplets[Random.Range(0, _waterDriplets.Count)];
            if (drip.activeInHierarchy == false)
            {
                drip.transform.position = new Vector3(Random.Range(-25.0f, 8.0f), drip.transform.position.y, Random.Range(2.0f, 20.0f));
                drip.SetActive(true);
                break;
            }
            yield return null;
        }

        StartCoroutine(DripRandomRoutine(Random.Range(_waitTime.x, _waitTime.y)));
    }

}
