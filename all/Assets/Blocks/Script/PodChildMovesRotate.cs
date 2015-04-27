using UnityEngine;
using System.Collections;

public class PodChildMovesRotate : MonoBehaviour {

    public bool useStartAngle;
    [Range(0, 360)]
    public float startAngle;
    [Range(100, 0.1f)]
    public float speedRotate = 60;
    [Range(0, 360)]
    public float targetAngle = 90.0f;
    [Range(0, 360)]
    public float collection_rotate = 0;

    public int iElSCusRotatePoint_ml = 0;
    public float startTimeRotate;
    public float journeyLengthRotate;

    private bool startChecker = true;
    public float returnTimePause;

    // Use this for initialization
    void Start2 () {
        //Параметры для поворотов
        //Временной счетчик
        startTimeRotate = Time.time;

        journeyLengthRotate = summCalc(startAngle, targetAngle) / 360;
        //startrotation = transform.rotation;

        startChecker = false;
    }

    // Update is called once per frame
    void Update () {
        if (startChecker) Start2();
        RotatePointToPoint();
    }

    private void RotatePointToPoint () {
        float distCoveredAngle = (Time.time - startTimeRotate) * speedRotate;
        //Debug.Log(" distCoveredAngle " + distCoveredAngle);
        float fracJourneyRotate = distCoveredAngle / journeyLengthRotate;
        float angle = Mathf.LerpAngle(startAngle, targetAngle, fracJourneyRotate / 60);
        transform.localEulerAngles = new Vector3(0, 0, angle);
        checkAndSelectNextRotateAngle(angle);
    }

    private void checkAndSelectNextRotateAngle (float _angle) {
        bool reorgAngle = false;
        float _angle2 = _angle;

        if (_angle < 0) { _angle += 360; }
        if (_angle > 360) { _angle -= 360; }

        _angle = (Mathf.Round(_angle * 100)) / 100;
        float _targetAngle = (Mathf.Round(targetAngle * 100)) / 100;

        //Debug.Log(" _angle " + _angle + " propertyMoveLine.targetAngle " + _targetAngle);


        if (_targetAngle == _angle) {
            reorgAngle = true;
        } else {
            reorgAngle = false;
        }

        if (reorgAngle) {
            startChecker = true;
            if (this.gameObject.GetComponent<ChildMoves>().timer_pause_rotate <= 0) {
                this.gameObject.GetComponent<ChildMoves>().timer_pause_rotate = returnTimePause;
            }
            this.gameObject.GetComponent<ChildMoves>().reorgAngle = true;
            this.gameObject.GetComponent<PodChildMoves>().enabled = false;
            //this.gameObject.GetComponent<MoveBlock>().
        }
    }

    //Высчитываем общую сумму игнорируя минус
    private float summCalc (float a, float b) {
        float result = 0;
        result = (convertSign(a) + convertSign(b));
        return result;
    }

    //Конвертируем отрицательные значения
    private float convertSign (float _f) {
        if (Mathf.Sign(_f) == -1f) _f *= -1f;
        return _f;
    }
}
