using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
  
    [SerializeField] private Segment _segmentTempale;

    public List<Segment> Generate(int count)
    {
        List<Segment> tail = new List<Segment>();
        for (int i = 0; i < count; i++)
        {
            tail.Add(Instantiate(_segmentTempale, transform));
        }
        return tail;
    }
}
