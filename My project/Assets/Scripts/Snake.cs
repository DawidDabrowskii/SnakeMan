using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    private void Start() // lista segmentow
    {
        _segments = new List<Transform> ();
        _segments.Add(this.transform);
    }
    private void Update() // poruszanie za pomoca strzalek
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {    
                _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }                
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
        { 
            _direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    { // respawn segmentow w odwrotnej kolejnosci
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f);
    }

    private void Grow() // funkcja  spawn nowych segmentow
    {
       Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState() // co sie dzieje po 'przegranej'. niszczymy segmenty, ale wracamy do '1' i powrot pozycji do pozycji startowej
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }    

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
            
    }
    private void OnTriggerEnter2D(Collider2D other) // spawn nowych segmentow na kolizje z tag."Food"
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
