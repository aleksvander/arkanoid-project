using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveBlock : MonoBehaviour {

    [Range(0, 500)]
	public float speed;
    [Range(0, 500)]
    public List<float> speedCollection = new List<float>();
    private int i_speedCollection = 0;
    public bool useSpeedHowConst;



	//Повороты
	[System.Serializable]
	public class RotationBlockPeremen {
        [Range(0, 500)]
        public float speedROA;
        [Range(0, 500)]
        public List<float> speedROACollection = new List<float>();

	    public bool isRotate;
        public bool RotateClockWise;
	}

	//Движение по прямой
	[System.Serializable]
	public class MoveLine {
		//Использовать MoveLine
        public bool yesMove;

        public MoveLineX MoveLine_ML;

        [System.Serializable]
        public class MoveLineX {
            //Скорость для MoveLine
            [Range(0, 500)]
            public float speed;
            [Range(0, 500)]
            public List<float> speedCollection = new List<float>();

            //Пауза для MoveLine
            public bool firstNoPauseML;
            [Range(0, 500)]
            public float timer_pause_move_line;
            [Range(0, 500)]
            public List<float> timer_pause_move_list = new List<float>();

            //Координаты для MoveLine
            public bool beginPointItIsStartPoint;
            public Vector2 start_point;
            public Vector2 absoluteEndPoint;
            public List<Vector2> collection_point = new List<Vector2>();
        }

        //Использовать ли Rotate PnP
        public bool useRotate = false;

        public RotateX Rotate_RO;

        [System.Serializable]
        public class RotateX {
            //Использовать начальный угол как свой
            public bool useStartAngle;

            //Начальный угол
            [Range(0, 360)]
            public float startAngle;
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

            //Текущий конечный угол
            [Range(0, 360)]
            public float targetAngle = 90.0f;
            //Коллекция "углов"
            [Range(0, 360)]
            public List<float> collection_rotate = new List<float>();
        }
	}

    //
    private float save_timer_pause_ml;
    private float save_timer_pause_ro;
    private float save_timer_pause_roa;
    private int i_timer_pause_ml_collect = 0;
    private int i_timer_pause_ro_collect = 0;
    private int i_timer_pause_roa_collect = 0;

    [System.Serializable]
    public class FM {
        public bool useFM = false;
        public bool useVallX = false;
        public bool useVallY = false;
        //public bool useMinToMax_X;
        //public bool useMinToMax_Y;
        [Range(0.1f, 2f)]
        public float bendingStrength = 0.1f;
        [Range(0, 50)]
        public int countIntermediaPoint;
        //public List<Vector2> collection_point_iz = new List<Vector2>();
        //public List<Vector2> tmp_collection_point_iz = new List<Vector2>();
	}

    [System.Serializable]
    public class ChildMove {
        // == MOVE LINE ==

        public bool useChildMove;

        public MoveLineX MoveLine;

        [System.Serializable]
        public class MoveLineX {
            //?? это не работает... и его я не прописывал...
            //[Range(0, 20f)]
            //public List<float> timeStartDelay = new List<float>();
            public bool moveLine;

            //Скорость
            public SpeedX speed;
            [System.Serializable]
            public class SpeedX {
                public bool useSpeedHowConst;
                //Скорость для MoveLine
                [Range(0.01f, 100)]
                public float speedMoveLine;
                //Разная скорость для moveline
                [Range(0.01f, 100)]
                public List<float> speedMoveLineCollection = new List<float>();
                //Коллекция скорости для MoveLine
                [Range(0.01f, 100)]
                public List<float> speedMoveLineListCollection = new List<float>();
            }

            //паузы MoveLine
            public PauseX pause;
            [System.Serializable]
            public class PauseX {
                public bool firstNoPauseML;
                [Range(0, 500)]
                public float timer_pause_move_line;
                [Range(0, 500)]
                public List<float> timer_pause_move_list = new List<float>();
            }

            public POINTX point;
            [System.Serializable]
            public class POINTX {
                public bool beginPointItIsStartPoint;
                public Vector2 start_point;
                public Vector2 absoluteEndPoint;
                public bool use_relativeEndPoint;
                public List<Vector2> collection_point = new List<Vector2>();
            }

            public FMX functionMove;
            [System.Serializable]
            public class FMX {
                public bool useFM = false;
                public bool useVallX = false;
                public bool useVallY = false;
                [Range(0.1f, 2f)]
                public float bendingStrength = 0.1f;
                [Range(0, 50)]
                public int countIntermediaPoint;
            }
        }

        public RotateX Rotate;

        [System.Serializable]
        public class RotateX {

            // == ROTATE ==
            public bool useRotate;

            // == ROA ==
            public bool RotateAround;
            public bool firstNoPauseROA;
            [Range(200, 0.1f)]
            public float speedRotateAround = 60;
            [Range(200, 0.1f)]
            public List<float> speedRotateAroundCollection = new List<float>();
            [Range(200, 0.1f)]
            public List<float> speedRotateAroundListCollection = new List<float>();

            //Скорость
            [Range(100, 0.1f)]
            public float speedRotate = 60;
            [Range(100, 0.1f)]
            public List<float> speedRotateCollection = new List<float>();
            [Range(100, 0.1f)]
            public List<float> speedRotateListCollection = new List<float>();

            //Пауза для Rotate
            public bool firstNoPauseRO;
            [Range(0, 500)]
            public float timer_pause_rotate;
            [Range(0, 500)]
            public List<float> timer_pause_rotate_list = new List<float>();

            public bool RotateClockWise;

            public bool useStartAngle;
            [Range(0, 360)]
            public float startAngle;


            [Range(0, 360)]
            public float targetAngle = 90.0f;
            [Range(0, 360)]
            public List<float> collection_rotate = new List<float>();
        }
    }

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
    //private float startTime;
    private float startTimeRotate;
    private bool checkTimer = false;
    private bool checkTimer2 = true;
    private bool checkTimerRotate = true;

    //Переменные для временных расчетов
    private float journeyLength;
    private float journeyLengthRotate;
    private Quaternion startrotation;

	//Перечисление Сериализации
	public RotationBlockPeremen propertyRotate;
	public MoveLine propertyMoveLine;
    public FM FunctionMove;
    public ChildMove SetChildMove;
    
	
	// Use this for initialization
	void Start () {
        //Параметры для moveline
        //Счетчик
        if (propertyMoveLine.MoveLine_ML.beginPointItIsStartPoint) iElCusPoint_ml++;

        startSetAllSettings();

        propertyMoveLine.MoveLine_ML.timer_pause_move_line = 0;
        //journeyLength = Vector3.Distance(propertyMoveLine.start_point, propertyMoveLine.absoluteEndPoint);

        //Функции для moveline
        if (propertyMoveLine.MoveLine_ML.collection_point.Count > 0 && FunctionMove.useFM) CalculateFunctionForMoveLine();


        //Параметры для поворотов
        //Временной счетчик
        startTimeRotate = Time.time;

        journeyLengthRotate = summCalc(propertyMoveLine.Rotate_RO.startAngle, propertyMoveLine.Rotate_RO.targetAngle) / 360;
        //startrotation = transform.rotation;

        if (propertyMoveLine.Rotate_RO.useStartAngle) {
            //Debug.Log(" transform.rotation.z " + transform.eulerAngles.z);
            propertyMoveLine.Rotate_RO.startAngle = transform.eulerAngles.z;
            propertyMoveLine.Rotate_RO.useStartAngle = false;
        }


		//Debug.Log(transform.position.x);
        if (propertyMoveLine.MoveLine_ML.beginPointItIsStartPoint) { 
			if (transform.position.x != 0 && transform.position.y != 0) {
                propertyMoveLine.MoveLine_ML.start_point = transform.position; 
			} else {
				if (this.gameObject.GetComponent<BuildingScenes_block>().line.useAbsolutePoint) {
                    propertyMoveLine.MoveLine_ML.start_point = this.gameObject.GetComponent<BuildingScenes_block>().line.beginPoint;
				} else if (this.gameObject.GetComponent<BuildingScenes_block>().line.useRelativePoint) {
                    propertyMoveLine.MoveLine_ML.start_point = new Vector2(this.gameObject.GetComponent<BuildingScenes_block>().line.beginRelativePoint.x - transform.position.x, 
					                                            this.gameObject.GetComponent<BuildingScenes_block>().line.beginRelativePoint.y - transform.position.y);
				}
			}
            if (propertyMoveLine.MoveLine_ML.start_point.x == 0 && propertyMoveLine.MoveLine_ML.start_point.y == 0) {

			}
		}
        //Переключатели
        //MoveLine
        checkTimer2 = true;

        //RotatePointToPoint
        checkTimerRotate = true;
        //Debug.Log(" MOVE BLOCK START ");
	}

    private void startSetAllSettings () {
        setStartPause();
    }

    private void setStartPause () {
        if (propertyMoveLine.MoveLine_ML.timer_pause_move_list.Count > 0) {
            save_timer_pause_ml = propertyMoveLine.MoveLine_ML.timer_pause_move_list[0];
        } else {
            save_timer_pause_ml = propertyMoveLine.MoveLine_ML.timer_pause_move_line;
        }

        if (propertyMoveLine.MoveLine_ML.firstNoPauseML) { propertyMoveLine.MoveLine_ML.timer_pause_move_line = 0; } else { propertyMoveLine.MoveLine_ML.timer_pause_move_line = save_timer_pause_ml; }

        if (propertyMoveLine.Rotate_RO.timer_pause_rotate_list.Count > 0) {
            save_timer_pause_ro = propertyMoveLine.Rotate_RO.timer_pause_rotate_list[0];
        } else {
            save_timer_pause_ro = propertyMoveLine.Rotate_RO.timer_pause_rotate;
        }

        if (propertyMoveLine.Rotate_RO.firstNoPauseRO) { propertyMoveLine.Rotate_RO.timer_pause_rotate = 0; } else { propertyMoveLine.Rotate_RO.timer_pause_rotate = save_timer_pause_ro; }
    }
	
	// Update is called once per frame
	void Update () {
        if (propertyMoveLine.MoveLine_ML.timer_pause_move_line < 0) {
            if (propertyMoveLine.yesMove) MovePointToPointLine();
        } else {
            propertyMoveLine.MoveLine_ML.timer_pause_move_line = propertyMoveLine.MoveLine_ML.timer_pause_move_line - Time.deltaTime * 10;
        }

        if (propertyMoveLine.Rotate_RO.timer_pause_rotate < 0) {
            if (propertyMoveLine.useRotate) RotatePointToPoint();
        } else {
            propertyMoveLine.Rotate_RO.timer_pause_rotate = propertyMoveLine.Rotate_RO.timer_pause_rotate - Time.deltaTime * 10;
        }

        if (propertyRotate.isRotate) RotateBlock();

        if (SetChildMove.useChildMove) SetAllChildSettingsMove();
	}

	private void MovePointToPointLine() {
		//Debug.Log("ВСЕ ХРЕНЬ!!!!!!!!!! ИСПОЛЬЗУЙ УРОКИ ДВИЖЕНИЯ ПО ТОЧКАМ!!! ЛЮБОЙ ТИПА РПГ!!! ЗАТЕМ СОЗДАЙ К ЭТОЙ ХРЕНИ КОНТРОЛЛЕР!!!! А ЗАТЕМ ДУМАЙ ПО ПОВОДУ ТОГО КАК ИСКРЕВЛЯТЬ ТРАНСЛЭЙТ!!!");

        if (propertyMoveLine.MoveLine_ML.beginPointItIsStartPoint) { propertyMoveLine.MoveLine_ML.start_point = transform.position; propertyMoveLine.MoveLine_ML.beginPointItIsStartPoint = false; }

        //Определяем последнюю точку Массив или?
        Vector2 _endPoint = new Vector2();
        if (propertyMoveLine.MoveLine_ML.collection_point.Count > 1) {
            _endPoint = propertyMoveLine.MoveLine_ML.collection_point[iElCusPoint_ml];
        } else {
            _endPoint = propertyMoveLine.MoveLine_ML.absoluteEndPoint;
        }

        //Скрипты определения времени/дистанции - служат для изменения позиции, перенесены в подфункцию
        //float distCovered = (Time.time - startTime) * speed;
        //float fracJourney = distCovered / journeyLength;
        //transform.position = Vector3.Lerp(propertyMoveLine.start_point, _endPoint, fracJourney / 60);
       
        //Запуск при первом проходе функции по движению
        if (checkTimer2) {
            //timer_pause_move_line = save_timer_pause_ml;
            StartPodFunctionMoveForBlock (_endPoint);
            checkTimer2 = false;
        }

        //Проверка и переключатель для линейного движения
        checkBeginAndEndPossition(_endPoint.x, _endPoint.y, "MovePointToPointLine");
        
        //Корректировка отклонении
		AutoCorrectZ();
	}

    private void RotatePointToPoint () {

        float distCoveredAngle = (Time.time - startTimeRotate) * propertyMoveLine.Rotate_RO.speedRotate;
        //Debug.Log(" distCoveredAngle " + distCoveredAngle);
        float fracJourneyRotate = distCoveredAngle / journeyLengthRotate;
        float angle = Mathf.LerpAngle(propertyMoveLine.Rotate_RO.startAngle, propertyMoveLine.Rotate_RO.targetAngle, fracJourneyRotate / 60);
        //Debug.Log("angle" + angle);
        /*transform.eulerAngles = new Vector3(0, 0, angle);*/
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
        if (propertyRotate.RotateClockWise) {
			transform.Rotate(0, 0, Time.deltaTime * speed);
		} else {
			transform.Rotate(0, 0, Time.deltaTime * -speed);
		}
	}



    //======================================== SEND MESSAGE FOR OTHER SCRIPT ===========================
    
    //Старт под функции движения для блока - MOVE LINE
    private void StartPodFunctionMoveForBlock (Vector2 _endPoint) {
        //Debug.Log(" START POD FUNCTION MOVE ");
        propertyMoveLine.MoveLine_ML.timer_pause_move_line = 0;

        float tmp_speed = 0;
        if (propertyMoveLine.MoveLine_ML.speedCollection.Count > 0) {
            //Debug.Log(" THIS ");
            tmp_speed = propertyMoveLine.MoveLine_ML.speedCollection[i_speedMoveCollection];
            if (i_speedMoveCollection + 1 == propertyMoveLine.MoveLine_ML.speedCollection.Count) { i_speedMoveCollection = 0; } else { i_speedMoveCollection++; }
        } else {
            tmp_speed = propertyMoveLine.MoveLine_ML.speed;
        }

        float _timer_pause = save_timer_pause_ml;
        if (propertyMoveLine.MoveLine_ML.timer_pause_move_list.Count > 0) {
            _timer_pause = propertyMoveLine.MoveLine_ML.timer_pause_move_list[i_timer_pause_ml_collect];
            if (i_timer_pause_ml_collect + 1 == propertyMoveLine.MoveLine_ML.timer_pause_move_list.Count) { i_timer_pause_ml_collect = 0; } else { i_timer_pause_ml_collect++; }
        }

        this.gameObject.GetComponent<podFunctionMoveForBlocks>().speed = tmp_speed;
        this.gameObject.GetComponent<podFunctionMoveForBlocks>().start_point = propertyMoveLine.MoveLine_ML.start_point;
        this.gameObject.GetComponent<podFunctionMoveForBlocks>().absoluteEndPoint = _endPoint;
        this.gameObject.GetComponent<podFunctionMoveForBlocks>().returnTimePause = _timer_pause;
        this.gameObject.GetComponent<podFunctionMoveForBlocks>().enabled = true;
        if (iElCusPoint_ml + 1 == propertyMoveLine.MoveLine_ML.collection_point.Count) { iElCusPoint_ml = 0; } else { iElCusPoint_ml++; }
    }

    //Старт под функции поворотов для блока - ROTATE POINT TO POINT
    private void StartPodFunctionRotateForBlock (float _angle) {
        propertyMoveLine.MoveLine_ML.timer_pause_move_line = 0;
        float tmp_speed = 0;
        if (propertyMoveLine.Rotate_RO.speedRotateCollection.Count > 0) {
            //Debug.Log(" THIS ");
            tmp_speed = propertyMoveLine.Rotate_RO.speedRotateCollection[i_speedRotateCollection];
            if (i_speedRotateCollection + 1 == propertyMoveLine.Rotate_RO.speedRotateCollection.Count) { i_speedRotateCollection = 0; } else { i_speedRotateCollection++; }
        } else {
            tmp_speed = propertyMoveLine.Rotate_RO.speedRotate;
        }

        float _timer_pause = save_timer_pause_ro;
        if (propertyMoveLine.Rotate_RO.timer_pause_rotate_list.Count > 0) {
            _timer_pause = propertyMoveLine.Rotate_RO.timer_pause_rotate_list[i_timer_pause_ro_collect];
            if (i_timer_pause_ro_collect + 1 == propertyMoveLine.Rotate_RO.timer_pause_rotate_list.Count) { i_timer_pause_ro_collect = 0; } else { i_timer_pause_ro_collect++; }
        }

        this.gameObject.GetComponent<podFunctionRotateForBlock>().returnTimePause = _timer_pause;
        this.gameObject.GetComponent<podFunctionRotateForBlock>().useStartAngle = propertyMoveLine.Rotate_RO.useStartAngle;
        this.gameObject.GetComponent<podFunctionRotateForBlock>().startAngle = propertyMoveLine.Rotate_RO.startAngle;
        this.gameObject.GetComponent<podFunctionRotateForBlock>().speedRotate = tmp_speed;
        this.gameObject.GetComponent<podFunctionRotateForBlock>().targetAngle = propertyMoveLine.Rotate_RO.targetAngle;
        this.gameObject.GetComponent<podFunctionRotateForBlock>().enabled = true;
        if (iElSCusRotatePoint_ml + 1 == propertyMoveLine.Rotate_RO.collection_rotate.Count) { iElSCusRotatePoint_ml = 0; } else { iElSCusRotatePoint_ml++; }
    }

    //======================================== CUSTOM CHECKER ===========================

    //== ROTATE ==
    private void checkAndSelectNextRotateAngle (float _angle) {
        //bool reorgAngle = false;
        float _angle2 = _angle;
        if (_angle < 0) { _angle += 360; }
        if (_angle > 360) { _angle -= 360; }
        //Debug.Log(" _angle " + _angle + " propertyMoveLine.targetAngle " + propertyMoveLine.targetAngle);

        if (propertyMoveLine.Rotate_RO.targetAngle == _angle) {
            //reorgAngle = true;
        } else {
            //reorgAngle = false;
        }

        //Debug.Log(" iElSCusRotatePoint_ml " + iElSCusRotatePoint_ml);

        if (reorgAngle) {
            if (propertyMoveLine.Rotate_RO.collection_rotate.Count > 1) {
                propertyMoveLine.Rotate_RO.startAngle = _angle;
                //if (iElSCusRotatePoint_ml + 1 == propertyMoveLine.collection_rotate.Count) { iElSCusRotatePoint_ml = 0; } else { iElSCusRotatePoint_ml++; }

                propertyMoveLine.Rotate_RO.targetAngle = propertyMoveLine.Rotate_RO.collection_rotate[iElSCusRotatePoint_ml];
            } else {
                float _a = 0;

                _a = propertyMoveLine.Rotate_RO.startAngle;

                propertyMoveLine.Rotate_RO.startAngle = propertyMoveLine.Rotate_RO.targetAngle;

                propertyMoveLine.Rotate_RO.targetAngle = _a;
            }
            startTimeRotate = Time.time;
            float tmpSumm = raznicaCalc(propertyMoveLine.Rotate_RO.startAngle, propertyMoveLine.Rotate_RO.targetAngle);
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
    /*private void checkBeginAndEndPossitionCommect () {
        /*float procentStepX = 0;
        float procentStepY = 0;

        procentStepX = (transform.position.x / end_x) * 100;
        procentStepY = (transform.position.y / end_y) * 100;

        //Debug.Log("procentStepX " + procentStepX + " reorg " + reorg);



        if (transform.position.x == end_x && transform.position.y == end_y) {
            if (Mathf.Round(end_x) == Mathf.Round(transform.position.x) && Mathf.Round(end_y) == Mathf.Round(transform.position.y)) {
                //reorg = true;
            }
        } else {
            //reorg = false;
        }

        //Debug.Log("Mathf.Round(end_x) " + (transform.position.x == end_x && transform.position.y == end_y));
    }*/

    private void checkBeginAndEndPossition (float end_x, float end_y, string name_function) {


        //Debug.Log(" check BEGIN AND END POSSITION " + iElCusPoint_ml);

        if (name_function == "MovePointToPointLine" && reorg) {
            if (propertyMoveLine.MoveLine_ML.collection_point.Count > 1) {
                propertyMoveLine.MoveLine_ML.start_point = transform.position;
                //if (iElCusPoint_ml + 1 == propertyMoveLine.collection_point.Count) { iElCusPoint_ml = 0; } else { iElCusPoint_ml++; }
                propertyMoveLine.MoveLine_ML.absoluteEndPoint = propertyMoveLine.MoveLine_ML.collection_point[iElCusPoint_ml];
            } else {
                float cx = 0;
                float cy = 0;

                cx = propertyMoveLine.MoveLine_ML.start_point.x;
                cy = propertyMoveLine.MoveLine_ML.start_point.y;

                propertyMoveLine.MoveLine_ML.start_point.x = propertyMoveLine.MoveLine_ML.absoluteEndPoint.x;
                propertyMoveLine.MoveLine_ML.start_point.y = propertyMoveLine.MoveLine_ML.absoluteEndPoint.y;

                propertyMoveLine.MoveLine_ML.absoluteEndPoint.x = cx;
                propertyMoveLine.MoveLine_ML.absoluteEndPoint.y = cy;

            }

            //startTime = Time.time;
            //if (useSpeedHowConst) journeyLength = Vector3.Distance(propertyMoveLine.start_point, propertyMoveLine.absoluteEndPoint);
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
        if (propertyMoveLine.MoveLine_ML.collection_point.Count > 1) {
            List<Vector2> tmpColl = new List<Vector2>(propertyMoveLine.MoveLine_ML.collection_point);
            List<float> powerx = new List<float>();
            List<float> powery = new List<float>();
            float power = FunctionMove.bendingStrength;
            for (int i = 0; i < propertyMoveLine.MoveLine_ML.collection_point.Count - 1; i++) {
                /*float tmpx = 0;
                float tmpx1 = propertyMoveLine.collection_point[i].x;
                float tmpx2 = propertyMoveLine.collection_point[i + 1].x;
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
            int lenghtMax = FunctionMove.countIntermediaPoint * (propertyMoveLine.MoveLine_ML.collection_point.Count - 1);
            int zz = 0;
            float zz2 = 0;
            bool peregib_y = false;
            int ff = 1;
            propertyMoveLine.MoveLine_ML.collection_point.Clear();

            propertyMoveLine.MoveLine_ML.collection_point.Add(tmpColl[0]);

            float tmpx = tmpColl[0].x;
            float tmpy = tmpColl[0].y;
            //if (FunctionMove.useMinToMax_Y) {
            //    zz2 = tmpColl[ff].y;
            //    peregib_y = true;
            //}

            for (int i = 1; i < lenghtMax + 1; i++) {
                //Debug.Log(" tmpx " + tmpx);
                if (zz == FunctionMove.countIntermediaPoint - 1) {
                    tmpx = tmpColl[ff - 1].x;
                    tmpy = tmpColl[ff - 1].y;
                    if (FunctionMove.useVallX) {
                        tmpx += calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].x, tmpColl[ff].x, FunctionMove.countIntermediaPoint);
                    }
                    if (FunctionMove.useVallY) {
                        tmpy += calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].y, tmpColl[ff].y, FunctionMove.countIntermediaPoint);
                    }

                    //if (!FunctionMove.useVallX && !FunctionMove.useVallY || FunctionMove.useVallX && FunctionMove.useVallY) {
                    propertyMoveLine.MoveLine_ML.collection_point.Add(tmpColl[ff]);
                    //} else if (FunctionMove.useVallX) {
                    //    propertyMoveLine.collection_point.Add(new Vector2 (tmpx, tmpColl[ff].y));
                    //} else if (FunctionMove.useVallX) {

                    //}
                    //Debug.Log("zz " + zz + " i " + i + " lenghtMax " + lenghtMax + " ff " + ff + " tmpColl[ff] " + tmpColl[ff]);
                    ff++;
                    zz = 0;
                    zz2 = 0;

                    //if (FunctionMove.useMinToMax_Y) {
                    //    zz2 = tmpColl[ff - 1].y;
                    //    peregib_y = true;
                    //}

                    tmpx = tmpColl[ff - 1].x;
                    tmpy = tmpColl[ff - 1].y;
                } else {
                    //Debug.Log("FunctionMove.countIntermediaPoint / 2 " + (FunctionMove.countIntermediaPoint / 2) + " zz2 " + zz2);
                    if (!peregib_y) { zz2--; } else { zz2++; }

                    if (FunctionMove.useVallX) {
                        tmpx = tmpx + (calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].x, tmpColl[ff].x, FunctionMove.countIntermediaPoint));
                        //Debug.Log("tmpx " + tmpx);
                    } else {
                        tmpx = Exponential(zz2, power);
                    }

                    if (FunctionMove.useVallY) {
                        tmpy = tmpy + (calculateStepForCalculateFunctionMoveLine(tmpColl[ff - 1].y, tmpColl[ff].y, FunctionMove.countIntermediaPoint));
                    } else {
                        tmpy = Exponential(zz2, power);
                    }
                    propertyMoveLine.MoveLine_ML.collection_point.Add(new Vector2(tmpx, tmpy));
                    zz++;
                }
                //if (FunctionMove.useMinToMax_Y) {

                //} else {
                if (zz2 == 0) { peregib_y = false; }
                if (zz2 == FunctionMove.countIntermediaPoint / 2) { peregib_y = true; }
                if ((zz2 * -1) == FunctionMove.countIntermediaPoint / 2) { peregib_y = true; }
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

    //======================================== CUSTOM CALCULATOR + SEND MESSAGE ===========================

    //Установка параметров для наследников
    private void SetAllChildSettingsMove () {
        int i = 0;
        int zz = 0;
        float tmp_speedMk = 0;
        float tmp_speedRO = 0;
        float tmp_speedRAO = 0;
        int i_speedMC = 0;
        int i_speedRO = 0;
        int i_speedRAO = 0;

        foreach (Transform child in transform) {
            //if (SetChildMove.MoveLine.timeStartDelay.Count > 0) {
            //    child.gameObject.GetComponent<ChildMoves>().timeStartDelay = SetChildMove.MoveLine.timeStartDelay[zz];
            //} else {
            //    child.gameObject.GetComponent<ChildMoves>().timeStartDelay = 0f;
            //}

            if (SetChildMove.MoveLine.speed.speedMoveLineCollection.Count > 0) {
                //Debug.Log(" THIS ");
                tmp_speedMk = SetChildMove.MoveLine.speed.speedMoveLineCollection[i_speedMC];
                if (i_speedMC + 1 == SetChildMove.MoveLine.speed.speedMoveLineCollection.Count) { i_speedMC = 0; } else { i_speedMC++; }
            } else {
                tmp_speedMk = SetChildMove.MoveLine.speed.speedMoveLine;
            }

            /*if (SetChildMove.speedRotateAroundCollection.Count > 0) {
                //Debug.Log(" THIS ");
                tmp_speedRAO = SetChildMove.speedRotateAroundCollection[i_speedRAO];
                if (tmp_speedRAO + 1 == SetChildMove.speedRotateAroundCollection.Count) { i_speedRAO = 0; } else { i_speedRAO++; }
            } else {
                tmp_speedRAO = SetChildMove.speedRotateAround;
            }*/

            if (SetChildMove.Rotate.speedRotateCollection.Count > 0) {
                //Debug.Log(" THIS ");
                tmp_speedRO = SetChildMove.Rotate.speedRotateCollection[i_speedRO];
                if (i_speedRO + 1 == SetChildMove.Rotate.speedRotateCollection.Count) { i_speedRO = 0; } else { i_speedRO++; }
            } else {
                tmp_speedRO = SetChildMove.Rotate.speedRotate;
            }
            //Параметры булевые
            child.gameObject.GetComponent<ChildMoves>().useMove = SetChildMove.MoveLine.moveLine;

            //Скорость ML + RO + ROA
            //булевые
            child.gameObject.GetComponent<ChildMoves>().useSpeedHowConst = SetChildMove.MoveLine.speed.useSpeedHowConst;
            child.gameObject.GetComponent<ChildMoves>().useRotate = SetChildMove.Rotate.useRotate;
            child.gameObject.GetComponent<ChildMoves>().RotateAround = SetChildMove.Rotate.RotateAround;

            //С плавающей точкой
            child.gameObject.GetComponent<ChildMoves>().speedML = tmp_speedMk;
            child.gameObject.GetComponent<ChildMoves>().speedRotate = tmp_speedRO;
            child.gameObject.GetComponent<ChildMoves>().speedRotateAround = tmp_speedRAO;

            //Коллекция скорости ML + RO + ROA
            child.gameObject.GetComponent<ChildMoves>().speedMLCollection = SetChildMove.MoveLine.speed.speedMoveLineListCollection;
            child.gameObject.GetComponent<ChildMoves>().speedRotateCollection = SetChildMove.Rotate.speedRotateCollection;
            child.gameObject.GetComponent<ChildMoves>().speedRotateAroundCollection = SetChildMove.Rotate.speedRotateAroundCollection;
            
            //Временная пауза
            //Булевые
            child.gameObject.GetComponent<ChildMoves>().firstNoPauseML = SetChildMove.MoveLine.pause.firstNoPauseML;
            child.gameObject.GetComponent<ChildMoves>().firstNoPauseRO = SetChildMove.Rotate.firstNoPauseRO;
            child.gameObject.GetComponent<ChildMoves>().firstNoPauseROA = SetChildMove.Rotate.firstNoPauseROA;

            //С плавающей точкой
            child.gameObject.GetComponent<ChildMoves>().timer_pause_move_line = SetChildMove.MoveLine.pause.timer_pause_move_line;
            child.gameObject.GetComponent<ChildMoves>().timer_pause_rotate = SetChildMove.Rotate.timer_pause_rotate;
            //child.gameObject.GetComponent<ChildMoves>().????? = SetChildMove.??????????;

            //Коллекции
            child.gameObject.GetComponent<ChildMoves>().timer_pause_move_list = SetChildMove.MoveLine.pause.timer_pause_move_list;
            child.gameObject.GetComponent<ChildMoves>().timer_pause_rotate_list = SetChildMove.Rotate.timer_pause_rotate_list;
            //child.gameObject.GetComponent<ChildMoves>().???? = SetChildMove.??????????;
            
            
            //Параметры позиции и булевые
            //булевые
            child.gameObject.GetComponent<ChildMoves>().beginPointItIsStartPoint = SetChildMove.MoveLine.point.beginPointItIsStartPoint;
            // == ROA ==
            child.gameObject.GetComponent<ChildMoves>().RotateClockWise = SetChildMove.Rotate.RotateClockWise;
            
            //булевые - релативная позиция
            child.gameObject.GetComponent<ChildMoves>().use_relativeEndPoint = SetChildMove.MoveLine.point.use_relativeEndPoint;

            //с плавающей - начало
            child.gameObject.GetComponent<ChildMoves>().start_point = SetChildMove.MoveLine.point.start_point;

            //с плавающей - конец
            child.gameObject.GetComponent<ChildMoves>().absoluteEndPoint = SetChildMove.MoveLine.point.absoluteEndPoint;

            //коллекции
            child.gameObject.GetComponent<ChildMoves>().collection_point = new List<Vector2>(SetChildMove.MoveLine.point.collection_point);

            //!параметры функции булевые
            child.gameObject.GetComponent<ChildMoves>().useFM = SetChildMove.MoveLine.functionMove.useFM;
            child.gameObject.GetComponent<ChildMoves>().useVallX = SetChildMove.MoveLine.functionMove.useVallX;
            child.gameObject.GetComponent<ChildMoves>().useVallY = SetChildMove.MoveLine.functionMove.useVallY;
            //!параметры функции плавающие
            child.gameObject.GetComponent<ChildMoves>().bendingStrength = SetChildMove.MoveLine.functionMove.bendingStrength;
            child.gameObject.GetComponent<ChildMoves>().countIntermediaPoint = SetChildMove.MoveLine.functionMove.countIntermediaPoint;
            

            //параметры RO
            //булевые
            child.gameObject.GetComponent<ChildMoves>().useStartAngle = SetChildMove.Rotate.useStartAngle;

            //с плавающей точкой
            child.gameObject.GetComponent<ChildMoves>().startAngle = SetChildMove.Rotate.startAngle;
            child.gameObject.GetComponent<ChildMoves>().targetAngle = SetChildMove.Rotate.targetAngle;

            //Коллекции
            child.gameObject.GetComponent<ChildMoves>().collection_rotate = new List<float>(SetChildMove.Rotate.collection_rotate);

            //включить
            child.gameObject.GetComponent<ChildMoves>().enabled = true;
            
            //счетчик внутрений
            //if (zz + 1 == SetChildMove.MoveLine.timeStartDelay.Count) { zz = 0; } else { zz++; }
            i++;
        }
        if (gameObject.GetComponent<BuildingScenes_block_v2_0>().aroundIsCreate) {
            if (gameObject.GetComponent<BuildingScenes_block_v2_0>().ab.count == i) {
                SetChildMove.useChildMove = false;
            }
        }
        if (gameObject.GetComponent<BuildingScenes_block_v2_0>().lineIsCreate) {
            if (gameObject.GetComponent<BuildingScenes_block_v2_0>().line.count == i) {
                SetChildMove.useChildMove = false;
            }
        }
    }


    //======================================== CUSTOM FUNCTION ===========================
    //Корректор
    void AutoCorrectZ () {
        if (transform.position.z != 0) { transform.position = new Vector3(transform.position.x, transform.position.y, 0); };
    }

    //Экспонент
    private float Exponential (float x, float power) {
        return (x * x) / (power * 20);
    }

    //Парабола
    private float Parabola (float x) {
        x = (FunctionMove.bendingStrength / 4) * x - 1f;
        return x * x;
    }

    //Синус
    private float Sine (float x) {
        x *= 10000;
        //Debug.Log(" 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x) " + Mathf.Sin(2 * Mathf.PI * x) * (FunctionMove.bendingStrength * 100));
        return (Mathf.Sin(2 * Mathf.PI * x)) * (FunctionMove.bendingStrength * 100);
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
