using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class podFunctionMoveForBlocks : MonoBehaviour {

    public float speed = 0f;

    public bool reorg = false;
    public bool itIsStart = true;
    public float startTime;
    public float journeyLength = 0f;

    public Vector2 start_point;
    public Vector2 absoluteEndPoint;

    public bool useSpeedHowConst = false;

    public float returnTimePause;

    public List<Vector2> collection_point = new List<Vector2>();

	// Use this for initialization
	void Start2 () {
        startTime = Time.time;
        journeyLength = Vector3.Distance(start_point, absoluteEndPoint);
        Debug.Log(" journeyLength " + journeyLength);
        itIsStart = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (itIsStart) Start2();
        MovePointToPointLine();
	}

    private void MovePointToPointLine () {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        if (distCovered != 0) {
            //Debug.Log(" start_point " + start_point + " absoluteEndPoint " + absoluteEndPoint + " fracJourney " + fracJourney + " distCovered " + distCovered);
            transform.position = Vector3.Lerp(start_point, absoluteEndPoint, fracJourney / 60);
        }

        checkBeginAndEndPossition(absoluteEndPoint.x, absoluteEndPoint.y, "MovePointToPointLine");

        AutoCorrectZ();
    }

    private void checkBeginAndEndPossition (float end_x, float end_y, string name_function) {
        if (transform.position.x == end_x && transform.position.y == end_y) {
            if (Mathf.Round(end_x) == Mathf.Round(transform.position.x) && Mathf.Round(end_y) == Mathf.Round(transform.position.y)) {
                reorg = true;
            }
        } else {
            reorg = false;
        }

        if (reorg) {
            itIsStart = true;
            reorg = false;
           this.gameObject.GetComponent<MoveBlock>().reorg = true;
           if (this.gameObject.GetComponent<MoveBlock>().propertyMoveLine.MoveLine_ML.timer_pause_move_line <= 0) {
               this.gameObject.GetComponent<MoveBlock>().propertyMoveLine.MoveLine_ML.timer_pause_move_line = returnTimePause;
           }
           this.gameObject.GetComponent<podFunctionMoveForBlocks>().enabled = false;
           
        }
    }

    void AutoCorrectZ () {
        if (transform.position.z != 0) { transform.position = new Vector3(transform.position.x, transform.position.y, 0); };
    }
}
