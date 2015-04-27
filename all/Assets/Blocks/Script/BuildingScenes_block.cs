using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScenes_block : MonoBehaviour {

    public StyleLevelBlock selectStyleLevelBlock;
    public List<ListConfigBlock> SelectCustomBlock = new List<ListConfigBlock>();
    private List<GameObject> customBlockArray = new List<GameObject>();
    public bool bonusOnlyBonusBlock;

    //public List<string> customBonusArray = new List<string>();
    public List<ListBonus> SelectBonusList = new List<ListBonus>();
    private List<string> customBonusArray = new List<string>();
    //public Liss<string> customBonusOnlyBonusBlock = new List<string>();


    //создать список с уже присвоенными объектами
    //затем создать два листа приватных и переопределять их свойства при сравнении

    private string _nameTag;


    //bomb

    private bool isCustom = false;
    //private bool isCustomBonus = false;


    [System.Serializable]
    public class SetObject {
        //Префаб цели
        public GameObject EnemyPrefab;
        //Родитель объекта
        public Object ParentBlock;
    }

    [System.Serializable]
    public class CreateAround {
        //Нужно ли это нам
        public bool isCreate;
        //Расстояние до игрока
        public float Distance = 2;
        //Угол занимаемый врагами (360 - вся окружность)
        public float Angle = 90;
        //Количество врагов
        public int count = 10;
    }

    [System.Serializable]
    public class CreateLine {
        //Нужно ли это нам
        public bool isCreate;
        public bool useAbsolutePoint;
        public Vector2 beginPoint;
        public bool useRelativePoint;
        public Vector2 beginRelativePoint;
        public Vector2 relativeEndPoint;
        public bool accuratePointStepSettingsChecker;
        //public float stepX;
        //public float stepY;
        public float accuratePointStepSettings;
        public int count = 10;
        public bool useBlurX;
        public float blurX;
        public bool useBlurY;
        public float blurY;
        public bool useCustomX;
        public bool useMaxCutomX;
        public List<float> blurXcustom;
        public bool useCustomY;
        public bool useMaxCutomY;
        public List<float> blurYcustom;
        public bool smoothX;
        public float smoothXPower;
        public bool smoothY;
        public float smoothYPower;
        public float okrugl_smooth_ext;
        public bool for_hard_domik;
        public bool for_hard_other1;
        public bool for_hard_zmeika;
        public bool for_hard_other2;
        public bool for_hard_other3;
    }
    private float raznicaXtmp;
    private float raznicaYtmp;

    [System.Serializable]
    public class SetOfProperties {
        //Тип
        //public enum type block;

        //Поведение
        public bool rotate;

        //Характеристики
        public float speed;

        //Данный набор передаем при создании
    }

    [System.Serializable]
    public class PrefabCollectionBonus {
        public GameObject bombPrefab;
        public GameObject lifePrefab;
        public GameObject multiple5Prefab;
        public GameObject multiple3Prefab;
        public GameObject gunPrefab;
        public GameObject smallBallPrefab;
        public GameObject ballNormalPrefab;
        public GameObject speedUPPrefab;
        public GameObject speedDownPrefab;
        public GameObject bigShieldPrefab;
        public GameObject smallSheildPrefab;
    }

    [System.Serializable]
    public class PrefabCollectionBlock {
        public GameObject t_3_section;
        public GameObject activator_active;
        public GameObject activator_pasive;
        public GameObject bonus_black;
        public GameObject bonus_wite;
        public GameObject exploid_black;
        public GameObject exploid_white;
        public GameObject full_armor;
        public GameObject half_armor;
        public GameObject simple;
        public GameObject up;
        public GameObject very_simple;
        public GameObject very_simple_half;
        public GameObject very_simple_half_4;
    }

    // Инициализация сериализации
    public SetObject gp = new SetObject();
    public CreateAround ab = new CreateAround();
    public CreateLine line = new CreateLine();
    public PrefabCollectionBonus collectBonusPrefab = new PrefabCollectionBonus();
    public PrefabCollectionBlock collectBlockPrefab = new PrefabCollectionBlock();

    void Awake () {
        EnterLinksArrayEnum();

        ConfigBlock.tmpStyleBlockStatic = selectStyleLevelBlock.ConfigSelection.ToString();
    }

    // Use this for initialization
    void Start () {
        //Определяем начальную точку
        Vector3 point = transform.position;
        //Переводим <a href="http://poqxert.ru/page/matematika-unity3d-urok-15-vector-otrazhenie-rasstojanie-proekcija-i-ugol" class="perelink">угол</a> в радианы
        ab.Angle = ab.Angle * Mathf.Deg2Rad;
        if (ab.isCreate) {
            ArondCreate(ab.Angle, point);
        }
        if (line.isCreate) {
            LineCreate(point);
        }
    }

    private void EnterLinksArrayEnum () {
        Debug.Log(SelectBonusList.Count);
        int i = 0;
        string tmpS;
        //for (i = 0; i < SelectBonusList.Count; i++) {
        //	customBonusArray.Add();
        //}
        foreach (ListBonus nodeList in SelectBonusList) {
            //Debug.Log(nodeList.ConfigSelection);
            //			customBonusArray[i] = 
            tmpS = (nodeList.ConfigSelection).ToString();
            customBonusArray.Add(tmpS);
            i++;
        }
        //int y = 0;
        //foreach(string strList in customBonusArray) {
        //	Debug.Log (customBonusArray[y]);
        //	y++;
        //}

        //i = 0;
        tmpS = "";
        foreach (ListConfigBlock nodeList in SelectCustomBlock) {
            tmpS = nodeList.ConfigSelection + "";
            if (tmpS == "mega_3_section" || tmpS == "t_3_section" || tmpS == "3_section") {
                customBlockArray.Add(collectBlockPrefab.t_3_section);
            }
            ;
            if (tmpS == "mega_activator_active" || tmpS == "activator_active") {
                customBlockArray.Add(collectBlockPrefab.activator_active);
            }
            ;
            if (tmpS == "mega_activator_pasive" || tmpS == "activator_pasive") {
                customBlockArray.Add(collectBlockPrefab.activator_pasive);
            }
            ;
            if (tmpS == "mega_bonus_black" || tmpS == "bonus_black") {
                customBlockArray.Add(collectBlockPrefab.bonus_black);
            }
            ;
            if (tmpS == "mega_bonus_wite" || tmpS == "bonus_wite") {
                customBlockArray.Add(collectBlockPrefab.bonus_wite);
            }
            ;
            if (tmpS == "mega_exploid_black" || tmpS == "exploid_black") {
                customBlockArray.Add(collectBlockPrefab.exploid_black);
            }
            ;
            if (tmpS == "mega_exploid_white" || tmpS == "exploid_white") {
                customBlockArray.Add(collectBlockPrefab.exploid_white);
            }
            ;
            if (tmpS == "mega_full_armor" || tmpS == "full_armor") {
                customBlockArray.Add(collectBlockPrefab.full_armor);
            }
            ;
            if (tmpS == "mega_half_armor" || tmpS == "half_armor") {
                customBlockArray.Add(collectBlockPrefab.half_armor);
            }
            ;
            if (tmpS == "mega_simple" || tmpS == "simple") {
                customBlockArray.Add(collectBlockPrefab.simple);
            }
            ;
            if (tmpS == "mega_up" || tmpS == "up") {
                customBlockArray.Add(collectBlockPrefab.up);
            }
            ;
            if (tmpS == "mega_very_simple" || tmpS == "very_simple") {
                customBlockArray.Add(collectBlockPrefab.very_simple);
            }
            ;
            if (tmpS == "mega_very_simple_half" || tmpS == "very_simple_half") {
                customBlockArray.Add(collectBlockPrefab.very_simple_half);
            }
            ;
            if (tmpS == "mega_very_simple_half_4" || tmpS == "very_simple_half_4") {
                customBlockArray.Add(collectBlockPrefab.very_simple_half_4);
            }
            ;
            //i++;
        }
    }

    private bool checkCustomBlock (int _index) {
        //Debug.Log (_index);
        if (customBlockArray[_index] != null) {
            return true;
        } //exploid_black

        return false;
    }

    private bool checkCustomBonus (int _index) {
        if (customBonusArray[_index] != "") {
            return true;
        }
        return false;
    }

    private void ArondCreate (float _angle, Vector3 _point) {
        for (int i = 1; i <= ab.count; i++) {
            //Сверяем нету ли в нем условии
            if (customBlockArray.Count > i - 1)
                isCustom = checkCustomBlock(i - 1);

            string _typeCustomBonusString = null;

            if (customBonusArray.Count > i - 1) {
                //Debug.Log ("customBonusArray.Count " + customBonusArray.Count);
                if (checkCustomBonus(i - 1)) {
                    //Debug.Log ("i " + (i - 1));
                    _typeCustomBonusString = customBonusArray[i - 1];
                }
            }

            //Debug.Log(isCustom);
            //Рассчитываем координату Y для врага
            float _y = transform.position.y + Mathf.Cos(_angle / ab.count * i) * ab.Distance;
            //Рассчитываем координату X для врага
            float _x = transform.position.x + Mathf.Sin(_angle / ab.count * i) * ab.Distance;
            _point.x = _x;
            _point.y = _y;

            //Создаём врага
            Object _enemy = gp.EnemyPrefab;
            if (isCustom) {
                _enemy = customBlockArray[i - 1];
                _nameTag = customBlockArray[i - 1].tag;
            } else if (!isCustom) {
                _enemy = gp.EnemyPrefab;
                _nameTag = gp.EnemyPrefab.tag;
            }

            createEnemy(_point, _enemy, isCustom, _typeCustomBonusString);
            isCustom = false;
            _typeCustomBonusString = null;
        }
    }

    private void createEnemy (Vector3 _point, Object _enemy, bool _isCustom, string _typeCustomBonus) {
        GameObject newObj;
        //Debug.Log(_point);
        newObj = (GameObject) Instantiate(_enemy, _point, Quaternion.identity);
        newObj.transform.parent = transform;

        newObj.gameObject.tag = _nameTag;



        //if (_typeCustomBonus != null) {
        newObj.GetComponent<BonusDrop>().onScript = true;



        if (bonusOnlyBonusBlock && _enemy.name == "bonus_black" || bonusOnlyBonusBlock && _enemy.name == "bonus_white" || !bonusOnlyBonusBlock)
            //Debug.Log(_enemy.name);
            newObj.GetComponent<BonusDrop>().SetBonusEnabled(_typeCustomBonus);
        //}

    }

    /** 
 * 	Подсчет шага по X и Y для Line
 **/
    private float CalculateStep (bool y) {
        float _tmpX = ChekerAbsoluteRelativePoint(false);
        float _tmpY = ChekerAbsoluteRelativePoint(true);

        float linebegintmpx = line.beginPoint.x;
        float linebegintmpy = line.beginPoint.y;

        float linerelativeendpointxtmp = line.relativeEndPoint.x;
        float linerelativeendpointytmp = line.relativeEndPoint.y;

        bool endminx = false;
        bool beginminx = false;
        bool endravnx = false;
        bool endminy = false;
        bool beginminy = false;
        bool endravny = false;

        if (linerelativeendpointxtmp < 0) {
            if (linebegintmpx > linerelativeendpointxtmp) {
                endminx = true;
            } else {
                endminx = false;
            }
        }

        if (linebegintmpx > 0) {
            if (linebegintmpx > linerelativeendpointxtmp) {
                beginminx = true;
            } else {
                beginminx = false;
            }
        }

        if (linerelativeendpointytmp < 0) {
            if (linebegintmpy > linerelativeendpointytmp) {
                endminy = true;
            } else {
                endminy = false;
            }
        }

        if (linebegintmpy > 0) {
            if (linebegintmpy > linerelativeendpointytmp) {
                beginminy = true;
            } else {
                beginminy = false;
            }
        }

        //Debug.Log("endminy" + endminy);

        if (linebegintmpx == linerelativeendpointxtmp) {
            endravnx = true;
        }

        if (linebegintmpy == linerelativeendpointytmp) {
            endravny = true;
        }

        //if (!beginminx) {
        if (linebegintmpx < 0) {
            linebegintmpx *= -1f;
        }
        ;

        //if (endminx) {
        if (linerelativeendpointxtmp < 0) {
            linerelativeendpointxtmp *= -1f;
        }

        /*}
} else {
    linerelativeendpointxtmp *= -1f;
}

Debug.Log ("linerelativeendpointxtmp" + linerelativeendpointxtmp);
*/

        if (linebegintmpy < 0) {
            linebegintmpy *= -1f;
        }
        ;
        if (linerelativeendpointytmp < 0) {
            linerelativeendpointytmp *= -1f;
        }

        //Debug.Log ("linebegintmpy" + linebegintmpy);

        raznicaXtmp = 0;
        raznicaYtmp = 0;

        if (beginminx && endminx) {
            if (linebegintmpx > linerelativeendpointxtmp) {
                raznicaXtmp = linebegintmpx - (linerelativeendpointxtmp * -1f);
            } else {
                raznicaXtmp = (linerelativeendpointxtmp * -1f) - linebegintmpx;
            }
            Debug.Log("raznicaXtmp" + raznicaXtmp); // 1 to -1
            raznicaXtmp *= -1f;
        }

        if (beginminx && !endminx) {
            if (linebegintmpx > linerelativeendpointxtmp) {
                raznicaXtmp = linebegintmpx - linerelativeendpointxtmp;
            } else {
                raznicaXtmp = linerelativeendpointxtmp - linebegintmpx;
            }
            Debug.Log("raznicaXtmp" + raznicaXtmp); //2 to 1
            raznicaXtmp *= -1f;
        }

        if (!beginminx && !endminx) {
            if (linebegintmpx < linerelativeendpointxtmp) {
                raznicaXtmp = linebegintmpx - linerelativeendpointxtmp; // 1 to 2
            } else {
                if (line.relativeEndPoint.x < 0 && line.beginPoint.x < 0) {
                    raznicaXtmp = (linerelativeendpointxtmp * 1f) - linebegintmpx; // -2 to -1
                } else if (line.relativeEndPoint.x > 0 && line.beginPoint.x < 0) {
                    raznicaXtmp = (linerelativeendpointxtmp * -1f) - linebegintmpx; // -1 to 1
                } else if (line.relativeEndPoint.x > 0 && line.beginPoint.x > 0) {
                    raznicaXtmp = (linerelativeendpointxtmp) - linebegintmpx; // -1 to 1
                }
            }
            raznicaXtmp *= -1f;
            Debug.Log("raznicaXtmp" + raznicaXtmp); // -2 to -1 //-1 to 1 //1 to 2
        }

        if (!beginminx && endminx) {
            if (linebegintmpx < linerelativeendpointxtmp) {
                raznicaXtmp = linebegintmpx - linerelativeendpointxtmp;
            } else {
                raznicaXtmp = linerelativeendpointxtmp - linebegintmpx;
            }
            raznicaXtmp *= -1f;
            Debug.Log("raznicaXtmp" + raznicaXtmp); // -1 to -2
        }
        //Debug.Log ("raznicaXtmp" + raznicaXtmp);


        /*if (endminy) {
    if (linebegintmpy > linerelativeendpointytmp) {
        raznicaYtmp = linebegintmpy - linerelativeendpointytmp;
    } else {
        raznicaYtmp = linerelativeendpointytmp - linebegintmpy;
    }
} else if (!endminy){
    if (linebegintmpy < linerelativeendpointytmp) {
        raznicaYtmp = linebegintmpy - (linerelativeendpointytmp * -1);
    } else {
        raznicaYtmp = (linerelativeendpointytmp * -1) - linebegintmpy;
    }
}*/

        if (beginminy && endminy) {
            if (linebegintmpy > linerelativeendpointytmp) {
                raznicaYtmp = linebegintmpy - (linerelativeendpointytmp * -1f);
            } else {
                raznicaYtmp = (linerelativeendpointytmp * -1f) - linebegintmpy;
            }
            Debug.Log("raznicaYtmp" + raznicaYtmp); // 1 to -1
            raznicaYtmp *= -1f;
        }

        if (beginminy && !endminy) {
            if (linebegintmpy > linerelativeendpointytmp) {
                raznicaYtmp = linebegintmpy - linerelativeendpointytmp;
            } else {
                raznicaYtmp = linerelativeendpointytmp - linebegintmpy;
            }
            Debug.Log("raznicaYtmp" + raznicaYtmp); //2 to 1
            raznicaYtmp *= -1f;
        }

        if (!beginminy && !endminy) {
            if (linebegintmpy < linerelativeendpointytmp) {
                raznicaYtmp = linebegintmpy - linerelativeendpointytmp; // 1 to 2
            } else {
                if (line.relativeEndPoint.y < 0 && line.beginPoint.y < 0) {
                    raznicaYtmp = (linerelativeendpointytmp * 1f) - linebegintmpy; // -2 to -1
                } else if (line.relativeEndPoint.y > 0 && line.beginPoint.y < 0) {
                    raznicaYtmp = (linerelativeendpointytmp * -1f) - linebegintmpy; // -1 to 1
                } else if (line.relativeEndPoint.y > 0 && line.beginPoint.y > 0) {
                    raznicaYtmp = (linerelativeendpointytmp) - linebegintmpy; // -1 to 1
                }
            }
            raznicaYtmp *= -1f;
            Debug.Log("raznicaYtmp" + raznicaYtmp); // -2 to -1 //-1 to 1 //1 to 2
        }

        if (!beginminy && endminy) {
            if (linebegintmpy < linerelativeendpointytmp) {
                raznicaYtmp = linebegintmpy - linerelativeendpointytmp;
            } else {
                raznicaYtmp = linerelativeendpointytmp - linebegintmpy;
            }
            raznicaYtmp *= -1f;
            Debug.Log("raznicaYtmp" + raznicaYtmp); // -1 to -2
        }

        //Debug.Log ("raznicaYtmp" + raznicaYtmp);

        if (endravnx) {
            raznicaXtmp = 0;
        }

        if (endravny) {
            raznicaYtmp = 0;
        }

        if (endminx) {
            raznicaXtmp *= -1f;
        }
        ;
        if (endminy) {
            raznicaYtmp *= -1f;
        }
        ;
        //if (linerelativeendpointxtmp < 0) { linerelativeendpointxtmp *= -1f; };

        if (!y) {
            float _stepX;
            _stepX = raznicaXtmp / line.count;

            //Debug.Log ("_stepX" + _stepX);
            return _stepX;
        } else if (y) {
            float _stepY = 0;
            _stepY = raznicaYtmp / line.count;

            //Debug.Log ("_stepY" + _stepY);
            /*if (line.beginPoint.y < 0 && line.relativeEndPoint.y < 0) {
    _stepY = (((-line.beginPoint.y - line.relativeEndPoint.y) * -1f) / line.count);
} else if (line.beginPoint.y < 0) {
    _stepY = ((-line.beginPoint.y + line.relativeEndPoint.y) / line.count);
} else if (line.relativeEndPoint.y < 0) {
    _stepY = ((line.beginPoint.y - line.relativeEndPoint.y) / line.count);
} else {
    _stepY = ((line.beginPoint.y + line.relativeEndPoint.y) / line.count);
}*/

            return _stepY;
        }

        return 0;
    }

    private float ChekerAbsoluteRelativePoint (bool y) {
        float _tmpX;
        float _tmpY;

        if (!line.useRelativePoint) {
            _tmpX = line.beginPoint.x;
            _tmpY = line.beginPoint.y;
        } else {
            _tmpX = line.beginRelativePoint.x - transform.position.x;
            _tmpY = line.beginRelativePoint.y - transform.position.y;
        }

        if (!y) {
            return _tmpX;
        } else {
            return _tmpY;
        }

        return 0;
    }

    private List<Vector2> SetStepList (float _stepX, float _stepY) {
        List<Vector2> pointStep = new List<Vector2>();

        float _tmpX = ChekerAbsoluteRelativePoint(false);
        float _tmpY = ChekerAbsoluteRelativePoint(true);

        //Debug.Log("_tmpX " + _tmpX + " line.relativeEndPoint.x " + line.relativeEndPoint.x);

        float raznicaX = line.relativeEndPoint.x;
        float raznicaY = line.relativeEndPoint.y;
        //Сделать проверку на отрицательность и высчитать значение абсолютное
        /*
if (_stepX < 0 && line.relativeEndPoint.x < 0) {
    raznicaX = (_stepX - line.relativeEndPoint.x) * -1f;
    Debug.Log("raznicaX " + raznicaX);
} else if (_stepX > 0 && line.relativeEndPoint.x < 0) {
    raznicaX = ((_stepX * -1f) - line.relativeEndPoint.x) * -1f;
    Debug.Log("raznicaX " + raznicaX);
} else if (_stepX < 0 && line.relativeEndPoint.x > 0) { 
    raznicaX = ((_stepX * -1f) + line.relativeEndPoint.x);
    Debug.Log("raznicaX " + raznicaX);
} else if (_stepX > 0 && line.relativeEndPoint.x > 0) {
    raznicaX = _stepX + line.relativeEndPoint.x;
    Debug.Log("raznicaX " + raznicaX);
}*/


        /*
//Сделать проверку на отрицательность и высчитать значение абсолютное
if (_stepY < 0 && line.relativeEndPoint.y < 0) {
    raznicaY = (_stepY - line.relativeEndPoint.y) * -1f;
    Debug.Log("raznicaY " + raznicaY);
} else if (_stepY > 0 && line.relativeEndPoint.y < 0) {
    raznicaY = ((_stepY * -1f) - line.relativeEndPoint.y) * -1f;
    Debug.Log("raznicaY " + raznicaY);
} else if (_stepY < 0 && line.relativeEndPoint.y > 0) { 
    raznicaY = ((_stepY * -1f) + line.relativeEndPoint.y);
    Debug.Log("raznicaY " + raznicaY);
} else if (_stepY > 0 && line.relativeEndPoint.y > 0) {
    raznicaY = _stepY + line.relativeEndPoint.y;
    Debug.Log("raznicaY " + raznicaY);
}*/

        //raznicaY = _stepY + line.relativeEndPoint.y;

        //pointStep.Add(new Vector2(_tmpX, _tmpY));

        //Debug.Log("_tmpX " + _tmpX);

        float massStepX = 0;
        float massStepY = 0;
        //Debug.Log ("massStepY " + massStepY);

        float maxX = line.relativeEndPoint.x;
        float maxY = line.relativeEndPoint.y;
        //Debug.Log ("maxY " + maxY);

        bool minmaxX = false;
        bool minmaxY = false;
        //Debug.Log ("minmaxY " + minmaxY);

        if (maxX < 0)
            minmaxX = true;
        if (maxY < 0)
            minmaxY = true;

        for (int i = 1; i <= line.count; i++) {
            /*if (_tmpX < 0) {
    if (_tmpX < raznicaX) { //откатываем 
        _tmpX += _stepX;
        Debug.Log ("_tmpX < 0 : " + _tmpX + " _tmpX < raznicaX " + raznicaX);
    };
} else if (_tmpX > 0) {
    if (_tmpX > raznicaX) { //откатываем 
        _tmpX += _stepX;
        Debug.Log ("_tmpX > 0 : " + _tmpX + " _tmpX > raznicaX " + raznicaX);
    };
}*/
            _tmpX += _stepX;
            //Debug.Log (_tmpY < 0);
            _tmpY += _stepY;
            /*if (minmaxY) {
    if (massStepY > maxY) {
        _tmpY += _stepY;
    }
} else if (!minmaxY) {
    if (massStepY < maxY) {
        _tmpY += _stepY;
    }
}*/

            massStepY += _stepY;
            //Debug.Log ("massStepY++ " + massStepY);
            //_tmpY += _stepY;

            pointStep.Add(new Vector2(_tmpX, _tmpY));
            //Debug.Log (pointStep[i-1]);
        }

        return pointStep;
    }

    private void LineCreate (Vector3 _point) {
        //Vector2 tmpVectorBuilding = line.relativeEndPoint;
        List<Vector2> pointStep = new List<Vector2>();

        float _stepX = CalculateStep(false);
        float _stepY = CalculateStep(true);
        //Получить stepX и stepY засчет вычисления процента между начальной позицией и конечной, а так же изначально заданного шага и кол-ва блоков


        if (line.useMaxCutomX || line.useBlurX) {
            CreateBlurLine(false);
        }
        //CreateBlurLine(false);

        if (line.useMaxCutomY || line.useBlurY) {
            CreateBlurLine(true);
        }

        if (line.accuratePointStepSettingsChecker) {
            //if (_stepX > 0.1f || _stepX < -0.1f) { 
            _stepX *= line.accuratePointStepSettings;
            //};
            //if (_stepY > 0.1f || _stepY < -0.1f) { 
            _stepY *= line.accuratePointStepSettings;
            //};
        }

        //Debug.Log ("_stepX " + _stepX);
        //Debug.Log ("_stepY " + _stepY);

        pointStep = SetStepList(_stepX, _stepY);

        for (int i = 1; i <= line.count; i++) {
            //Сверяем нету ли в нем условии
            if (customBlockArray.Count > i - 1)
                isCustom = checkCustomBlock(i - 1);

            string _typeCustomBonusString = null;

            if (customBonusArray.Count > i - 1) {
                //Debug.Log ("customBonusArray.Count " + customBonusArray.Count);
                if (checkCustomBonus(i - 1)) {
                    //Debug.Log ("i " + (i - 1));
                    _typeCustomBonusString = customBonusArray[i - 1];
                }
            }

            //if (line.useAbsolutePoint) {
            //Debug.Log ("pointStep[i].x" + pointStep[i - 1].x);
            //Debug.Log ("pointStep[i].y" + (pointStep[i - 1]).y);
            //Рассчитываем координаты
            _point.x = (pointStep[i - 1]).x;
            _point.y = (pointStep[i - 1]).y;

            //} else if (line.useRelativePoint) {


            //}
            if (line.useBlurX || line.useCustomX || line.useMaxCutomX) {
                if (_point.x < 0) {
                    //_point.x += (line.blurXcustom[i - 1] * -1f);
                } else {
                    _point.x += line.blurXcustom[i - 1];
                    //Debug.Log("_point.x " + _point.x);
                }
            }

            if (line.useBlurY || line.useCustomY || line.useMaxCutomY) {
                if (_point.y < 0) {
                    //_point.y += (line.blurXcustom[i - 1] * -1f);
                } else {
                    _point.y += line.blurYcustom[i - 1];
                    //Debug.Log("_point.y " + _point.y);
                }
            }

            //Создаём врага
            Object _enemy = gp.EnemyPrefab;
            if (isCustom) {
                _enemy = customBlockArray[i - 1];
                _nameTag = customBlockArray[i - 1].tag;
            } else if (!isCustom) {
                _enemy = gp.EnemyPrefab;
                _nameTag = gp.EnemyPrefab.tag;
            }



            createEnemy(_point, _enemy, isCustom, _typeCustomBonusString);
            isCustom = false;
            _typeCustomBonusString = null;
        }
    }

    private void CreateBlurLine (bool _y) {
        int numberX = 0;
        int numberY = 0;

        bool testTwoX = false;
        bool testTwoY = false;

        testTwoX = FindTwoAndMoreNumeric(false);
        testTwoY = FindTwoAndMoreNumeric(true);

        List<int> collectRepeatNumericBlurX = new List<int>();
        List<float> collectNumericSymbolBlurX = new List<float>();
        if (testTwoX) collectRepeatNumericBlurX = FindCollectionNumericForCollisionLine(false);
        if (testTwoX) collectNumericSymbolBlurX = FindCollectionNumericSymbolForCollisionLine(false);

        List<int> collectRepeatNumericBlurY = new List<int>();
        List<float> collectNumericSymbolBlurY = new List<float>();
        if (testTwoY) collectRepeatNumericBlurY = FindCollectionNumericForCollisionLine(true);
        if (testTwoY) collectNumericSymbolBlurY = FindCollectionNumericSymbolForCollisionLine(true);

        if (!_y) {
            if (line.useMaxCutomX) {
                numberX = FindMaxForBlueArray(false, 0);
                Debug.Log("!!!!!!!!!CreateBlurLine -! numberX: " + numberX);
            }
        } else {
            if (line.useMaxCutomY) {
                numberY = FindMaxForBlueArray(true, 0);
                Debug.Log("!!!!!!!!!CreateBlurLine -! numberY: " + numberY);
            }
        }

        /* Если метод отрабатывает но массив равен НУЛЮ пересоздаем его пустым равным количеству элементов для дальнейшенго заполнения */
        if (line.blurXcustom.Count == 0) {
            for (int z = 0; z < line.count; z++) {
                line.blurXcustom.Add(0); // тогда присваиваем ей размер line.count и Х так же
            }
        }

        if (line.blurYcustom.Count == 0) {
            for (int z = 0; z < line.count; z++) {
                line.blurYcustom.Add(0); // тогда присваиваем ей размер line.count и Х так же
            }
        }
        ;

        int tmp_end_i = 0;
        if (!_y) {
            tmp_end_i = line.blurXcustom.Count;
        } else {
            tmp_end_i = line.blurYcustom.Count;
        }

        //for (int i = 0; i < line.count; i++) {
        if (!_y) {
            if (testTwoX && !line.useBlurX) {
                FillingCustomBlurArrayExt(false, collectRepeatNumericBlurX, collectNumericSymbolBlurX);
            } else {
                if (line.useMaxCutomX) {
                    FillingCustomBlurArray(false, numberX, line.blurXcustom[numberX]);
                } else {
                    FillingCustomBlurArray(false, Mathf.RoundToInt(line.count / 2), line.blurX);
                }
            }
        } else {
            if (testTwoY && !line.useBlurY) {
                //for (int z = 0; z < collectRepeatNumericBlurY.Count; z++) {
                //    float tmpMax = 0;
                //    if (z + 1 < collectNumericSymbolBlurY.Count) {
                //        tmpMax = collectRepeatNumericBlurY[z + 1];
                //    } else {
                //        tmpMax = collectRepeatNumericBlurY.Count;
                //    }
                //Debug.Log("FillingCustomBlurArrayExt");
                FillingCustomBlurArrayExt(true, collectRepeatNumericBlurY, collectNumericSymbolBlurY);
                //}
            } else {
                if (line.useMaxCutomY) {
                    FillingCustomBlurArray(true, numberY, line.blurYcustom[numberY]);
                } else {
                    FillingCustomBlurArray(true, Mathf.RoundToInt(line.count / 2), line.blurY);
                }
            }
        }
        //}
    }


    /**
    * Заполняем массив для Line
    **/
    private void FillingCustomBlurArray (bool _y, float numelement, float maxCorrect) {
        //numelement += 1f;
        //maxCorrect -= 1f;

        bool tmpsmoothX = false;
        if (line.smoothX) tmpsmoothX = true;
        
        bool tmpsmoothY = false;
        if (line.smoothY) tmpsmoothY = true;

        //float differenceXbegin = numelement / line.count;
        float differenceBegin = maxCorrect / (numelement);
        float differenceEnd = 0;

        if (line.count % 2 == 0) {
            differenceEnd = (maxCorrect / (line.count - numelement - 1f));
        } else {
            differenceEnd = (maxCorrect / (line.count - numelement - 1f));
        }
        //Debug.Log(line.count - numelement);
        Debug.Log("FillingCustomBlurArray y: " + _y + " numelement " + numelement + " maxCorrect " + maxCorrect);
        Debug.Log("differencebegin " + differenceBegin + " differenceEnd: " + differenceEnd + " numelement " + numelement + " line.count " + line.count);

        int i = 0;
        //numelement -= 1f;
        //maxCorrect += 1f;

        if (!_y) {
            foreach (float itemX in line.blurXcustom) {
                if (numelement > i) {
                    line.blurXcustom[i] = differenceBegin * i;

                    if (tmpsmoothX)
                        line.blurXcustom[i] = smoothLine(_y, line.blurXcustom[i], true, i, numelement);

                } else if (numelement < i && i + 1 != line.count) {
                    line.blurXcustom[i] = maxCorrect - (differenceEnd * (i - numelement));

                    if (tmpsmoothX)
                        line.blurXcustom[i] = smoothLine(_y, line.blurXcustom[i], false, i, numelement);

                } else if (numelement == i) {
                    line.blurXcustom[i] = maxCorrect;
                } else if (i + 1 == line.count) {
                    //и так нуль
                    line.blurXcustom[i] = 0f;
                }
                Debug.Log(line.blurXcustom[i]);
                i++;
            }
        } else {
            foreach (float itemY in line.blurYcustom) {
                //Debug.Log("differenceBegin * i " + differenceBegin * i);
                if (numelement > i) {
                    line.blurYcustom[i] = differenceBegin * i;

                    if (tmpsmoothY) line.blurYcustom[i] = smoothLine(_y, line.blurYcustom[i], true, i, numelement);

                } else if (numelement < i && i + 1 != line.count) {
                    line.blurYcustom[i] = maxCorrect - (differenceEnd * (i - numelement));

                    if (tmpsmoothY) line.blurYcustom[i] = smoothLine(_y, line.blurYcustom[i], false, i, numelement);

                } else if (numelement == i) {
                    line.blurYcustom[i] = maxCorrect;
                } else if (i + 1 == line.count) {
                    //и так нуль
                    line.blurYcustom[i] = 0f;
                }
                Debug.Log(line.blurYcustom[i]);
                i++;
            }
        }
    }

    /**
    * Заполняем массив для Line
    **/
    private void FillingCustomBlurArrayExt (bool _y, List<int> numelement, List<float> numericelement) {
        //numelement += 1f;
        //maxCorrect -= 1f;
        bool tmpsmoothX = false;
        if (line.smoothX)
            tmpsmoothX = true;
        bool tmpsmoothY = false;
        if (line.smoothY)
            tmpsmoothY = true;

        //Найти средние числа между спискам номеров элементов - numelement

        List<float> garmoshka = new List<float>();

        int i = 0;
        int sum_element = numelement.Count;

        Debug.Log("sum_element " + sum_element);

        //bool returnMinus = false;
        for (int z = 0; z < sum_element; z++) {
            float promesh_elements = 0;
            float point = 0;
            float lenght_otr = 0;
            //int begin_pos = 0;
            bool last_cicle = false;

            //if (z > 0 && (line.blurYcustom[i] != null || line.blurYcustom[i] != 0)) i--;

            if (z == sum_element - 1) last_cicle = true;
            Debug.Log("z " + (z) + " sum_element " + (sum_element - 1));

            if (!last_cicle) {
                //if ((line.blurYcustom[i] == 0 || line.blurYcustom[i] == null) && i == 0) {

                //} else {
                    if (numelement[z] > numelement[z + 1]) {
                        promesh_elements = numelement[z] - numelement[z + 1];
                    } else {
                        promesh_elements = numelement[z + 1] - numelement[z];
                    }

                    float tmp1 = numericelement[z];
                    float tmp2 = numericelement[z + 1];
                    bool minusReverse = false;
                    if (tmp2 < 0) minusReverse = true;
                    if (tmp1 < 0) tmp1 *= -1f;
                    if (tmp2 < 0) tmp2 *= -1f;
                    point = tmp1 + tmp2;
                    if (minusReverse) point *= -1f;

                    lenght_otr = point / promesh_elements;
                //}
            } else {
                promesh_elements = line.blurYcustom.Count - numelement[z];
                float tmp1 = numericelement[z];
                if (tmp1 < 0) tmp1 *= -1f;
                point = tmp1;
                lenght_otr = point / promesh_elements;
            }
            Debug.Log("line.blurYcustom " + " [" + i + "] ");
            Debug.Log("point " + point + " lenght_otr " + lenght_otr);

            //promesh_elements -= 2;



            //Debug.Log("promesh_elements " + promesh_elements);

            if (z != 0) {

            }

            float summ_promesh_elements = i + promesh_elements;
            if (last_cicle) { summ_promesh_elements = line.blurYcustom.Count; }
            Debug.Log("summ_promesh_elements " + summ_promesh_elements);
            
            

            if (!_y) {
                 foreach (float itemX in line.blurXcustom) {
                     //Debug.Log("FOREACH X?");
                     if (i < summ_promesh_elements) {
                         if (i != 0 && (line.blurXcustom[i] != null || line.blurXcustom[i] != 0)) {
                             if (lenght_otr + line.blurXcustom[i - 1] > 0.01f || lenght_otr + line.blurXcustom[i - 1] < 0.01f) {
                                 line.blurXcustom[i] = lenght_otr + line.blurXcustom[i - 1];
                             } else {
                                 //line.blurXcustom[i] = 0;
                                 line.blurXcustom[i] = lenght_otr + line.blurXcustom[i - 1];
                             } //bool _y, float _editNum, bool _raznica, int i, float _numelement
                         }
                         Debug.Log("line.blurXcustom " + " [" + i + "] " + line.blurXcustom[i]);
                         i++;
                     }
                 }
                 if (z == 0) {
                     i++;
                 }
            } else {
                foreach (float itemY in line.blurYcustom) {
                    if (i < summ_promesh_elements) {
                        if (i != 0 && (line.blurYcustom[i] != null || line.blurYcustom[i] != 0)) {
                            if (lenght_otr + line.blurYcustom[i - 1] > 0.01f || lenght_otr + line.blurYcustom[i - 1] < 0.01f) {
                                line.blurYcustom[i] = lenght_otr + line.blurYcustom[i - 1];
                            } else {
                                //line.blurYcustom[i] = 0;
                                line.blurYcustom[i] = lenght_otr + line.blurYcustom[i - 1];
                            }
                        }
                        //Debug.Log("lenght_otr + line.blurYcustom[i - 1]" + lenght_otr + line.blurYcustom[i - 1]);
                        //Debug.Log("line.blurYcustom " + " [" + i + "] " + line.blurYcustom[i]);
                        i++;
                    }
                }
                if (z == 0) {
                    i++;
                }
            }
        }

        if (tmpsmoothX) {
            smoothLineExt(false, numelement, numericelement);
        }
        if (tmpsmoothY) {
            smoothLineExt(true, numelement, numericelement);
        }
    }

    /**
    * Размытие 
    */
    private float smoothLine (bool _y, float _editNum, bool _raznica, int i, float _numelement) {
        float tmpSMooth = 0f;
        if (!_y) {
            tmpSMooth = line.smoothXPower;
        } else {
            tmpSMooth = line.smoothYPower;
        }

        if (_raznica) {
            _editNum = _editNum + (_editNum * ((_numelement - i) * tmpSMooth) / 100f);
            Debug.Log("(_numelement - i)" + (_numelement - i));
        } else {
            _editNum = _editNum + (_editNum * ((i - _numelement) * tmpSMooth) / 100f);
            Debug.Log("(i - _numelement)" + (i - _numelement));
        }


        return _editNum;
    }

    private void smoothLineExt (bool _y, List<int> numelement, List<float> numericelement) {
        float tmpSMooth = 0f;
        if (!_y) {
            tmpSMooth = line.smoothXPower;
        } else {
            tmpSMooth = line.smoothYPower;
        }

        int sum_element = numelement.Count;
        int zzz = 0;
        float tmpX = 0;
        //float continiusX = 0;
        float tmpY = 0;
        //float continiusY = 0;

        Debug.Log("sum_element " + sum_element);

        //bool returnMinus = false;

        Debug.Log("zzz " + zzz);
        if (line.for_hard_domik || line.for_hard_other3 || line.for_hard_other2) zzz = 0;

        for (int z = 0; z < sum_element; z++) {
            bool last_cicle = false;
            float promesh_elements = 0;

            if (z == sum_element - 1) last_cicle = true;
            Debug.Log("z " + (z) + " sum_element " + (sum_element - 1));

            if (!last_cicle) {
                if (numelement[z] > numelement[z + 1]) {
                    promesh_elements = numelement[z] - numelement[z + 1];
                } else {
                    promesh_elements = numelement[z + 1] - numelement[z];
                }
            }


            if (!_y) {
                tmpX = promesh_elements / 2f;
                tmpX = 0;
                bool plus_x = false;

                for (int x = 0; x < promesh_elements; x++) {
                    zzz++;
                    if (x != 0 && x == promesh_elements) {
                        Debug.Log("CHTO TO NE TAK" + " x " + x + " promesh_elements " + promesh_elements);
                    } else {
                        if (line.okrugl_smooth_ext > 1f || line.okrugl_smooth_ext < -1f) {
                            line.blurXcustom[zzz] = line.blurXcustom[zzz] + (line.blurXcustom[zzz] * ((tmpX * line.okrugl_smooth_ext) * tmpSMooth) / 100f);
                        } else {
                            line.blurXcustom[zzz] = line.blurXcustom[zzz] + (line.blurXcustom[zzz] * ((tmpX) * tmpSMooth) / 100f);
                        }
                    }


                    if (x > promesh_elements / 2) {
                        //tmpX = 0f;// / 2f;
                        plus_x = true;
                    }
                    if (line.for_hard_other1) {
                        if (plus_x) { tmpX++; } else { tmpX--; }
                    } else if (line.for_hard_zmeika) {
                        tmpX--;
                    } else if (line.for_hard_domik) {
                        if (zzz > promesh_elements) { tmpX = promesh_elements - zzz; } else { tmpX = zzz - promesh_elements; }
                    }
                    Debug.Log("tmpX " + tmpX + " promesh_elements " + promesh_elements + " zzz " + zzz);

                }
            } else if(_y) {
                //tmpY = promesh_elements;// / 2f;
                //tmpY = 0;
                bool plus_x = false;


                for (int x = 0; x < promesh_elements; x++) {
                    //if (!line.for_hard_domik) 
                    if (line.for_hard_other1) {
                        if (plus_x) { tmpY++; } else { tmpY--; }
                    } else if (line.for_hard_zmeika) {
                        tmpY--;
                    } else if (line.for_hard_domik) {
                        if (zzz >= promesh_elements) { tmpY = promesh_elements - zzz; } else { tmpY = zzz - promesh_elements; }
                        //if (tmpY == 0) { Debug.Log("ALARM!!!!!!!"); }
                    } else if (line.for_hard_other2) {
                        tmpY = zzz - promesh_elements;
                    } else if (line.for_hard_other3) {
                        tmpY = promesh_elements - zzz;
                    }                    
                    
                    zzz++;

                    if (x > promesh_elements / 2) {
                        //tmpY = 0f;// / 2f;
                        plus_x = true;
                    }



                    if (x != 0 && x == promesh_elements) {
                        Debug.Log("CHTO TO NE TAK" + " x " + x + " promesh_elements " + promesh_elements);
                    } else {
                        if (line.okrugl_smooth_ext > 1f || line.okrugl_smooth_ext < -1f) {
                            line.blurYcustom[zzz] = line.blurYcustom[zzz] + (line.blurYcustom[zzz] * ((tmpY * line.okrugl_smooth_ext) * tmpSMooth) / 100f);
                        } else {
                            line.blurYcustom[zzz] = line.blurYcustom[zzz] + (line.blurYcustom[zzz] * ((tmpY) * tmpSMooth) / 100f);
                        }
                        Debug.Log("line.blurYcustom[zzz] + (line.blurYcustom[zzz] * ((tmpY * line.okrugl_smooth_ext) * tmpSMooth) / 100f); " + (line.blurYcustom[zzz] + (line.blurYcustom[zzz] * ((tmpY) * tmpSMooth) / 100f)));
                    }
                    


                    Debug.Log("tmpY " + tmpY + " promesh_elements " + promesh_elements + " zzz " + zzz);
                    //if (line.for_hard_domik) zzz++;
                }

                if (line.for_hard_domik) {
                    //line.blurYcustom[0]
                }
            }
        }
        
    }



    /**
    * Запоминаем номера где есть числа 
    **/
    private List<int> FindCollectionNumericForCollisionLine (bool _y) {
        List<int> tmp_a = new List<int>();
        if (!_y) {
            int i = 0;
            foreach (float blurXitem in line.blurXcustom) {
                if (blurXitem != 0) {
                    i++;
                    tmp_a.Add(i);
                } else {
                    i++;
                }
            }
        } else {
            int i = 0;
            foreach (float blurYitem in line.blurYcustom) {
                if (blurYitem != 0) {
                    i++;
                    tmp_a.Add(i);
                } else {
                    i++;
                }
            }
        }
        return tmp_a;
    }

    /**
    * Запоминаем сами числа 
    **/
    private List<float> FindCollectionNumericSymbolForCollisionLine (bool _y) {
        List<float> tmp_a = new List<float>();
        if (!_y) {
            int i = 0;
            foreach (float blurXitem in line.blurXcustom) {
                if (blurXitem != 0) {
                    i++;
                    tmp_a.Add(blurXitem);
                } else {
                    i++;
                }
            }
        } else {
            int i = 0;
            foreach (float blurYitem in line.blurYcustom) {
                if (blurYitem != 0) {
                    i++;
                    tmp_a.Add(blurYitem);
                } else {
                    i++;
                }
            }
        }
        return tmp_a;
    }

    /**
    * Ищем подтверждение если их два и больше 
    **/
    private bool FindTwoAndMoreNumeric (bool _y) {
        if (!_y) {
            int i = 0;
            foreach (float blurXitem in line.blurXcustom) {
                if (blurXitem != 0)
                    i++;
                if (i > 1)
                    return true;
            }
        } else {
            int i = 0;
            foreach (float blurYitem in line.blurYcustom) {
                if (blurYitem != 0)
                    i++;
                if (i > 1)
                    return true;
            }
        }
        return false;
    }


    /**
    * Простой поиск первого числа от заданой позиции в массивах
    **/
    private int FindMaxForBlueArray (bool _y, int begin_i) {
        if (!_y) {
            int i = 0;
            foreach (float blurXitem in line.blurXcustom) {
                if (blurXitem != 0 && begin_i < i)
                    return i;
                i++;
            }
        } else {
            int i = 0;
            foreach (float blurYitem in line.blurYcustom) {
                if (blurYitem != 0 && begin_i < i)
                    return i;
                i++;
            }
        }
        return 0;
    }

    private static float Exponential (float x) {
        return x * x;
    }
    private static float Parabola (float x) {
        x = 2f * x - 1f;
        return x * x;
    }
    private static float Sine (float x) {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x);
    } 

}
