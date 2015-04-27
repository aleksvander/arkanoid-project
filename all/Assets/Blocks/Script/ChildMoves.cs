using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChildMoves : MonoBehaviour {

    //[Range(0, 20f)]
    //public float timeStartDelay;

    //Таймеры прайват
    private float save_timer_pause_ml;
    private float save_timer_pause_ro;
    private float save_timer_pause_roa;
    private int i_timer_pause_ml_collect = 0;
    private int i_timer_pause_ro_collect = 0;
    private int i_timer_pause_roa_collect = 0;

    // ======== MOVE LINE =======
    

    public bool useMove;

    //Скорость
    //Скорость для MoveLine
    [Range(0, 500)]
    public float speedML;
    [Range(0, 500)]
    public List<float> speedMLCollection = new List<float>();
    //Скорость постоянная
    public bool useSpeedHowConst;

    //Таймеры
    //Пауза для MoveLine
    public bool firstNoPauseML;
    [Range(0, 500)]
    public float timer_pause_move_line;
    [Range(0, 500)]
    public List<float> timer_pause_move_list = new List<float>();

    //Использовать начальную позицию
    public bool beginPointItIsStartPoint;
    //Координаты
    public Vector2 start_point;
    public Vector2 absoluteEndPoint;
    //Испольовать относительные координаты
    public bool use_relativeEndPoint;
    //Список точек
    public List<Vector2> collection_point = new List<Vector2>();

    //Использовать ось для... перечисления
    public bool useFM = false;
    public bool useVallX = false;
    public bool useVallY = false;
    //Сила прогиба
    [Range(0.1f, 2f)]
    public float bendingStrength = 0.1f;
    //Количество вставок
    [Range(0, 50)]
    public int countIntermediaPoint;
    
    // ======== ROTATE =======
    public bool useRotate;

        // ========= ROA ===========
        public bool RotateAround;
        public bool firstNoPauseROA;
        [Range(200, 0.1f)]
        public float speedRotateAround = 60;
        [Range(200, 0.1f)]
        public List<float> speedRotateAroundCollection = new List<float>();
        public bool RotateClockWise;

    //Скорость поворота
    [Range(100, 0.1f)]
    public float speedRotate = 60;
    //Коллекция скорости
    [Range(100, 0.1f)]
    public List<float> speedRotateCollection = new List<float>();

    //Пауза для Rotate
    public bool firstNoPauseRO;
    [Range(0, 500)]
    public float timer_pause_rotate;
    [Range(0, 500)]
    public List<float> timer_pause_rotate_list = new List<float>();

    //
    public bool useStartAngle;
    //
    [Range(0, 360)]
    public float startAngle;
    
    //
    [Range(0, 360)]
    public float targetAngle = 90.0f;
    [Range(0, 360)]
    public List<float> collection_rotate = new List<float>();

    //Счетчик
    private int i_speedMoveCollection = 0;
    private int i_speedRotateCollection = 0;
    private int iElCusPoint_ml = 0;
    private int iElSCusRotatePoint_ml = 0;

    //Условный переключатель для изменения конечной с начальной точкой
    [HideInInspector]
    public bool reorg = false;
    [HideInInspector]
    public bool reorgAngle = false;

    //Переключатели
    private float startTime;
    private float startTimeRotate;
    private bool checkTimer = false;
    private bool checkTimer2 = true;
    private bool checkTimerRotate = true;

    //Переменные для временных расчетов
    private float journeyLength;
    private float journeyLengthRotate;
    private Quaternion startrotation;


	// Use this for initialization
	void Start () {
        //Функция MoveLine
        if (use_relativeEndPoint) ConvertAbsoluteEndPointToRelativeEndPoint();

        startSetAllSettings();

        //Функции для moveline
        if (collection_point.Count > 0 && useFM) CalculateFunctionForMoveLine();

        //Параметры для moveline
        //Счетчик
        if (beginPointItIsStartPoint) iElCusPoint_ml++;

        //journeyLength = Vector3.Distance(start_point, absoluteEndPoint);

        //Параметры для поворотов
        //Временной счетчик
        startTimeRotate = Time.time;

        journeyLengthRotate = summCalc(startAngle, targetAngle) / 360;
        //startrotation = transform.rotation;

        if (useStartAngle) {
            //Debug.Log(" transform.rotation.z " + transform.localEulerAngles.z);
            startAngle = transform.localEulerAngles.z;
            useStartAngle = false;
        }


        //Debug.Log(transform.localPosition.x);
        if (beginPointItIsStartPoint) {
            if (transform.localPosition.x != 0 && transform.localPosition.y != 0) {
                start_point = transform.localPosition;
            } else {
                if (this.gameObject.GetComponent<BuildingScenes_block>().line.useAbsolutePoint) {
                    start_point = this.gameObject.GetComponent<BuildingScenes_block>().line.beginPoint;
                } else if (this.gameObject.GetComponent<BuildingScenes_block>().line.useRelativePoint) {
                    start_point = new Vector2(this.gameObject.GetComponent<BuildingScenes_block>().line.beginRelativePoint.x - transform.localPosition.x,
                                                                this.gameObject.GetComponent<BuildingScenes_block>().line.beginRelativePoint.y - transform.localPosition.y);
                }
            }
        }
        //Переключатели
        //MoveLine
        checkTimer2 = true;

        //RotatePointToPoint
        checkTimerRotate = true;
        //Debug.Log(" MOVE BLOCK START ");
	}

    private void ConvertAbsoluteEndPointToRelativeEndPoint () {
        absoluteEndPoint = new Vector2(transform.localPosition.x + absoluteEndPoint.x, transform.localPosition.y + absoluteEndPoint.y);
    }

    private void startSetAllSettings () {
        setStartPause();
    }

    private void setStartPause () {
        if (timer_pause_move_list.Count > 0) {
            save_timer_pause_ml = timer_pause_move_list[0];
        } else {
            save_timer_pause_ml = timer_pause_move_line;
        }

        if (firstNoPauseML) { timer_pause_move_line = 0; } else { timer_pause_move_line = save_timer_pause_ml; }

        if (timer_pause_rotate_list.Count > 0) {
            save_timer_pause_ro = timer_pause_rotate_list[0];
        } else {
            save_timer_pause_ro = timer_pause_rotate;
        }

        if (firstNoPauseRO) { timer_pause_rotate = 0; } else { timer_pause_rotate = save_timer_pause_ro; }
    }

	
	// Update is called once per frame
	void Update () {
        //Debug.Log("ПЕРЕДЕЛАЙ POSITION в LOCAL POSITION");
        if (timer_pause_move_line < 0) {
            if (useMove) MovePointToPointLine();
        } else {
            timer_pause_move_line = timer_pause_move_line - Time.deltaTime * 10;
        }

        if (timer_pause_rotate < 0) {
            if (useRotate) RotatePointToPoint();
        } else {
            timer_pause_rotate = timer_pause_rotate - Time.deltaTime * 10;
        }

        if (RotateAround) RotateBlock();
	}


    private void MovePointToPointLine () {
        //Debug.Log("ВСЕ ХРЕНЬ!!!!!!!!!! ИСПОЛЬЗУЙ УРОКИ ДВИЖЕНИЯ ПО ТОЧКАМ!!! ЛЮБОЙ ТИПА РПГ!!! ЗАТЕМ СОЗДАЙ К ЭТОЙ ХРЕНИ КОНТРОЛЛЕР!!!! А ЗАТЕМ ДУМАЙ ПО ПОВОДУ ТОГО КАК ИСКРЕВЛЯТЬ ТРАНСЛЭЙТ!!!");

        if (beginPointItIsStartPoint) { start_point = transform.localPosition; beginPointItIsStartPoint = false; }

        //Определяем последнюю точку Массив или?
        Vector2 _endPoint = new Vector2();
        if (collection_point.Count > 1) {
            _endPoint = collection_point[iElCusPoint_ml];
        } else {
            _endPoint = absoluteEndPoint;
        }

        //Скрипты определения времени/дистанции - служат для изменения позиции, перенесены в подфункцию
        //float distCovered = (Time.time - startTime) * speed;
        //float fracJourney = distCovered / journeyLength;
        //transform.localPosition = Vector3.Lerp(start_point, _endPoint, fracJourney / 60);

        //Запуск при первом проходе функции по движению
        if (checkTimer2) {
            //timer_pause_move_line = save_timer_pause_ml;
            StartPodFunctionMoveForBlock(_endPoint);
            checkTimer2 = false;
        }

        //Проверка и переключатель для линейного движения
        checkBeginAndEndPossition(_endPoint.x, _endPoint.y, "MovePointToPointLine");

        //Корректировка отклонении
        AutoCorrectZ();
    }

    private void RotatePointToPoint () {

        float distCoveredAngle = (Time.time - startTimeRotate) * speedRotate;
        //Debug.Log(" distCoveredAngle " + distCoveredAngle);
        float fracJourneyRotate = distCoveredAngle / journeyLengthRotate;
        float angle = Mathf.LerpAngle(startAngle, targetAngle, fracJourneyRotate / 60);
        //Debug.Log("angle" + angle);
        /*transform.localEulerAngles = new Vector3(0, 0, angle);*/
        if (checkTimerRotate) {
            Debug.Log(" ROTATE POINT TO POINT ");
            //timer_pause_move_line = save_timer_pause_ml;
            StartPodFunctionRotateForBlock(angle);
            checkTimerRotate = false;
        }
        checkAndSelectNextRotateAngle(angle);
    }

    //Поворот по часовой/против
    void RotateBlock () {
        if (RotateClockWise) {
            transform.Rotate(0, 0, Time.deltaTime * speedRotateAround);
        } else {
            transform.Rotate(0, 0, Time.deltaTime * -speedRotateAround);
        }
    }



    //======================================== SEND MESSAGE FOR OTHER SCRIPT ===========================

    //Старт под функции движения для блока - MOVE LINE
    private void StartPodFunctionMoveForBlock (Vector2 _endPoint) {
        //Debug.Log(" START POD FUNCTION MOVE ");
        timer_pause_move_line = 0;

        float tmp_speed = 0;
        if (speedMLCollection.Count > 0) {
            //Debug.Log(" THIS ");
            tmp_speed = speedMLCollection[i_speedMoveCollection];
            if (i_speedMoveCollection + 1 == speedMLCollection.Count) { i_speedMoveCollection = 0; } else { i_speedMoveCollection++; }
        } else {
            tmp_speed = speedML;
        }

        float _timer_pause = save_timer_pause_ml;
        if (timer_pause_move_list.Count > 0) {
            _timer_pause = timer_pause_move_list[i_timer_pause_ml_collect];
            if (i_timer_pause_ml_collect + 1 == timer_pause_move_list.Count) { i_timer_pause_ml_collect = 0; } else { i_timer_pause_ml_collect++; }
        }

        this.gameObject.GetComponent<PodChildMoves>().speed = tmp_speed;
        this.gameObject.GetComponent<PodChildMoves>().start_point = start_point;
        this.gameObject.GetComponent<PodChildMoves>().absoluteEndPoint = _endPoint;
        this.gameObject.GetComponent<PodChildMoves>().returnTimePause = _timer_pause;
        this.gameObject.GetComponent<PodChildMoves>().enabled = true;
        if (iElCusPoint_ml + 1 == collection_point.Count) { iElCusPoint_ml = 0; } else { iElCusPoint_ml++; }
    }

    //Старт под функции поворотов для блока - ROTATE POINT TO POINT
    private void StartPodFunctionRotateForBlock (float _angle) {
        timer_pause_move_line = 0;
        float tmp_speed = 0;
        if (speedRotateCollection.Count > 0) {
            //Debug.Log(" THIS ");
            tmp_speed = speedRotateCollection[i_speedRotateCollection];
            if (i_speedRotateCollection + 1 == speedRotateCollection.Count) { i_speedRotateCollection = 0; } else { i_speedRotateCollection++; }
        } else {
            tmp_speed = speedRotate;
        }

        float _timer_pause = save_timer_pause_ro;
        if (timer_pause_rotate_list.Count > 0) {
            _timer_pause = timer_pause_rotate_list[i_timer_pause_ro_collect];
            if (i_timer_pause_ro_collect + 1 == timer_pause_rotate_list.Count) { i_timer_pause_ro_collect = 0; } else { i_timer_pause_ro_collect++; }
        }

        this.gameObject.GetComponent<PodChildMovesRotate>().returnTimePause = _timer_pause;
        this.gameObject.GetComponent<PodChildMovesRotate>().useStartAngle = useStartAngle;
        this.gameObject.GetComponent<PodChildMovesRotate>().startAngle = startAngle;
        this.gameObject.GetComponent<PodChildMovesRotate>().speedRotate = tmp_speed;
        this.gameObject.GetComponent<PodChildMovesRotate>().targetAngle = targetAngle;
        this.gameObject.GetComponent<PodChildMovesRotate>().enabled = true;
        if (iElSCusRotatePoint_ml + 1 == collection_rotate.Count) { iElSCusRotatePoint_ml = 0; } else { iElSCusRotatePoint_ml++; }
    }

    //======================================== CUSTOM CHECKER ===========================

    //== ROTATE ==
    private void checkAndSelectNextRotateAngle (float _angle) {
        //bool reorgAngle = false;
        float _angle2 = _angle;
        if (_angle < 0) { _angle += 360; }
        if (_angle > 360) { _angle -= 360; }
        //Debug.Log(" _angle " + _angle + " targetAngle " + targetAngle);

        if (targetAngle == _angle) {
            //reorgAngle = true;
        } else {
            //reorgAngle = false;
        }

        //Debug.Log(" iElSCusRotatePoint_ml " + iElSCusRotatePoint_ml);

        if (reorgAngle) {
            if (collection_rotate.Count > 1) {
                startAngle = _angle;
                //if (iElSCusRotatePoint_ml + 1 == collection_rotate.Count) { iElSCusRotatePoint_ml = 0; } else { iElSCusRotatePoint_ml++; }

                targetAngle = collection_rotate[iElSCusRotatePoint_ml];
            } else {
                float _a = 0;

                _a = startAngle;

                startAngle = targetAngle;

                targetAngle = _a;
            }
            startTimeRotate = Time.time;
            float tmpSumm = raznicaCalc(startAngle, targetAngle);
            if (360 - tmpSumm < 180) {
                tmpSumm = 360 - tmpSumm;
            }
            Debug.Log(" tmpSumm " + tmpSumm + " _angle " + _angle + " _angle2 " + _angle2);

            journeyLengthRotate = tmpSumm / 360;
            reorgAngle = false;
            StartPodFunctionRotateForBlock(_angle);
        }
    }

    //== MOVE ==
    private void checkBeginAndEndPossition (float end_x, float end_y, string name_function) {


        //Debug.Log(" check BEGIN AND END POSSITION " + iElCusPoint_ml);

        if (name_function == "MovePointToPointLine" && reorg) {
            if (collection_point.Count > 1) {
                start_point = transform.localPosition;
                //if (iElCusPoint_ml + 1 == collection_point.Count) { iElCusPoint_ml = 0; } else { iElCusPoint_ml++; }
                absoluteEndPoint = collection_point[iElCusPoint_ml];
            } else {
                float cx = 0;
                float cy = 0;

                cx = start_point.x;
                cy = start_point.y;

                start_point.x = absoluteEndPoint.x;
                start_point.y = absoluteEndPoint.y;

                absoluteEndPoint.x = cx;
                absoluteEndPoint.y = cy;

            }

            //startTime = Time.time;
            //if (useSpeedHowConst) journeyLength = Vector3.Distance(start_point, absoluteEndPoint);
            reorg = false;

            checkTimer2 = false;
            StartPodFunctionMoveForBlock(new Vector2(end_x, end_y));
            checkTimer2 = false;
        }

    }


    //======================================== CUSTOM CALCULATOR ===========================

    //== MOVE LINE ==

    //Калькулятор для MoveLine
    private void CalculateFunctionForMoveLine () {
        if (collection_point.Count > 1) {
            List<Vector2> tmpColl = new List<Vector2>(collection_point);
            List<float> powerx = new List<float>();
            List<float> powery = new List<float>();
            float power = bendingStrength;
            for (int i = 0; i < collection_point.Count - 1; i++) {
                /*float tmpx = 0;
                float tmpx1 = collection_point[i].x;
                float tmpx2 = collection_point[i + 1].x;
                float razx = raznicaCalc(tmpx1, tmpx2);
                bool boox = howBig(tmpx1, tmpx2);
                if (boox && tmpx1 < 0) { tmpx = tmpx1 - razx; } else if (boox && tmpx1 > 0) { tmpx = tmpx1 + razx; }
                if (!boox && tmpx2 < 0) { tmpx = tmpx2 - razx; } else if (!boox && tmpx2 > 0) { tmpx = tmpx2 + razx; }
                powerx.Add(tmpx);
                Debug.Log("tmpx " + tmpx);*/
            }
            //Нужно создать массив который будет записывать значения мощности <Vector2>, 
            //а мощность определяется так... разница между двумя числами (допустим по X или по Y) 
            //и эту разницу мы прибавляем к наибольшему/наименьшему значению


            //Если нам нужно определять динамически... как пойдет ось 
            //снизу/сверху - можно по конечному... но идеальной логики я тут не вижу, думаю надо сделать еще один массив в котором будут указываться на "общих" промежутках идти ему куда...
            //или сбоков 
            //Debug.Log("СМОТРИ СЮДА");
            //При рассмотрении в случае с одинаковыми значениями между началом и концом, думаю стоит добавить отдельную функцию / или даже без нее
            // которая бы в свою очередь делала "возвратно" поступательные движения
            // на примере имеем в начале по Х 1 и по Х в конце 1
            // значит *2 или на -2* но! точнее (1 * -1 + 2 / 2) - где 1 в случае сли оба значения равны
            // -1 в случа если мы хотим сделать его отрицательным
            // и количество чисел - т.е. 2 делим на сумму этих чисел - 2
            // 2 и 2 - (2 * -1 + (2 / 4) = 
            // ОНО НЕ НУЖНО!!!!!!!!!!!!!

            // думаю лучше всего будет всего от А до Б по определенной из осей а вторая ОСЬ может быть изгибаемой, но она должна учитывать то значение в котором она находится а не убивать его!

            //Debug.Log(" tmpColl " + tmpColl[0]);
            int lenghtMax = countIntermediaPoint * (collection_point.Count - 1);
            int zz = 0;
            float zz2 = 0;
            bool peregib_y = false;
            int ff = 1;
            collection_point.Clear();

            collection_point.Add(tmpColl[0]);

            float tmpx = tmpColl[0].x;
            float tmpy = tmpColl[0].y;
            //if (useMinToMax_Y) {
            //    zz2 = tmpColl[ff].y;
            //    peregib_y = true;
            //}

            for (int i = 1; i < lenghtMax + 1; i++) {
                //Debug.Log(" tmpx " + tmpx);
                if (zz == countIntermediaPoint - 1) {
                    tmpx = tmpColl[ff - 1].x;
                    tmpy = tmpColl[ff - 1].y;
                    if (useVallX) {
                        tmpx += calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].x, tmpColl[ff].x, countIntermediaPoint);
                    }
                    if (useVallY) {
                        tmpy += calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].y, tmpColl[ff].y, countIntermediaPoint);
                    }

                    //if (!useVallX && !useVallY || useVallX && useVallY) {
                    collection_point.Add(tmpColl[ff]);
                    //} else if (useVallX) {
                    //    collection_point.Add(new Vector2 (tmpx, tmpColl[ff].y));
                    //} else if (useVallX) {

                    //}
                    //Debug.Log("zz " + zz + " i " + i + " lenghtMax " + lenghtMax + " ff " + ff + " tmpColl[ff] " + tmpColl[ff]);
                    ff++;
                    zz = 0;
                    zz2 = 0;

                    //if (useMinToMax_Y) {
                    //    zz2 = tmpColl[ff - 1].y;
                    //    peregib_y = true;
                    //}

                    tmpx = tmpColl[ff - 1].x;
                    tmpy = tmpColl[ff - 1].y;
                } else {
                    //Debug.Log("countIntermediaPoint / 2 " + (countIntermediaPoint / 2) + " zz2 " + zz2);
                    if (!peregib_y) { zz2--; } else { zz2++; }

                    if (useVallX) {
                        tmpx = tmpx + (calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].x, tmpColl[ff].x, countIntermediaPoint));
                        //Debug.Log("tmpx " + tmpx);
                    } else {
                        tmpx = Exponential(zz2, power);
                    }

                    if (useVallY) {
                        tmpy = tmpy + (calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].y, tmpColl[ff].y, countIntermediaPoint));
                    } else {
                        tmpy = Exponential(zz2, power);
                    }
                    collection_point.Add(new Vector2(tmpx, tmpy));
                    zz++;
                }
                //if (useMinToMax_Y) {

                //} else {
                if (zz2 == 0) { peregib_y = false; }
                if (zz2 == countIntermediaPoint / 2) { peregib_y = true; }
                if ((zz2 * -1) == countIntermediaPoint / 2) { peregib_y = true; }
                //}
            }

        }
    }

    //Калькулятор шагов для MoveLine
    private float calculateStepForCalculateFunctionMoveLine (float a, float b, float d) {
        float c = 0;
        if (a > 0 && b > 0) {
            c = (raznicaCalc(a, b)) / d;
        } else {
            c = (summCalc(a, b)) / d;
        }
        //Debug.Log(" raznicaCalc(a, b) " + raznicaCalc(a, b));
        if (b < 0 && a > 0) c *= -1f;
        //Debug.Log(" c " + c + " a " + a + " b " + b + " d " + d);
        return c;
    }

    //======================================== CUSTOM FUNCTION ===========================
    //Корректор
    void AutoCorrectZ () {
        if (transform.localPosition.z != 0) { transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0); };
    }

    //Экспонент
    private float Exponential (float x, float power) {
        return (x * x) / (power * 20);
    }

    //Парабола
    private float Parabola (float x) {
        x = (bendingStrength / 4) * x - 1f;
        return x * x;
    }

    //Синус
    private float Sine (float x) {
        x *= 10000;
        //Debug.Log(" 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x) " + Mathf.Sin(2 * Mathf.PI * x) * (bendingStrength * 100));
        return (Mathf.Sin(2 * Mathf.PI * x)) * (bendingStrength * 100);
    }

    //Конвертируем отрицательные значения
    private float convertSign (float _f) {
        if (Mathf.Sign(_f) == -1f) _f *= -1f;
        return _f;
    }

    //Высчитываем разницу
    private float raznicaCalc (float a, float b) {
        float resualt = 0;
        a = convertSign(a);
        b = convertSign(b);
        if (a > b) {
            resualt = a - b;
        } else {
            resualt = b - a;
        }
        return resualt;
    }

    //Высчитываем общую сумму игнорируя минус
    private float summCalc (float a, float b) {
        float result = 0;
        result = (convertSign(a) + convertSign(b));
        return result;
    }

    //Определяем наибольшее значение
    private bool howBig (float a, float b) {
        bool z = false;
        if (convertSign(a) > convertSign(b)) {
            z = true;
        } else {
            z = false;
        }
        return z;
    }
}
