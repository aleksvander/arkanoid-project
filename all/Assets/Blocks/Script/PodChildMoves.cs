using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PodChildMoves : MonoBehaviour {

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
        //Debug.Log(" journeyLength " + journeyLength);
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
            transform.localPosition = Vector3.Lerp(start_point, absoluteEndPoint, fracJourney / 60);
        }

        checkBeginAndEndPossition(absoluteEndPoint.x, absoluteEndPoint.y, "MovePointToPointLine");

        AutoCorrectZ();
    }

    private void checkBeginAndEndPossition (float end_x, float end_y, string name_function) {
        if (transform.localPosition.x == end_x && transform.localPosition.y == end_y) {
            if (Mathf.Round(end_x) == Mathf.Round(transform.localPosition.x) && Mathf.Round(end_y) == Mathf.Round(transform.localPosition.y)) {
                reorg = true;
            }
        } else {
            reorg = false;
        }

        if (reorg) {
            itIsStart = true;
            reorg = false;
            this.gameObject.GetComponent<ChildMoves>().reorg = true;
            if (this.gameObject.GetComponent<ChildMoves>().timer_pause_move_line <= 0) {
                this.gameObject.GetComponent<ChildMoves>().timer_pause_move_line = returnTimePause;
            }
            this.gameObject.GetComponent<PodChildMoves>().enabled = false;

        }
    }

    void AutoCorrectZ () {
        if (transform.localPosition.z != 0) { transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0); };
    }

}
