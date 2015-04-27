using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingScenes_block_v2_0 : MonoBehaviour {

    /**
     * Способы построения:
     * Геометрическими фигурами по формулам: круг, квадрат, элипс, ромб
     * Линий: прямая, экспонент, парабола, синус, косинус, тангенс, катангенс
     * Способ выстраивания: разворот родителя и подобъектов и трансформация родителя.
     * 
     * 
     * 
     **/

    /**
     * Объявленеи объектов 
     **/

    /**
     * Коллекция префабов
     **/

    //Бонусы
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

    //Блоки
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

    /**
     * Определение параметров для блоков 
     **/
    //Селектор стилей объектов
    public StyleLevelBlock selectStyleLevelBlock;

    //Селектор произвольных блоков
    public List<ListConfigBlock> SelectCustomBlock = new List<ListConfigBlock>();
    private List<GameObject> customBlockArray = new List<GameObject>();

    public List<ListBonus> SelectBonusList = new List<ListBonus>();
    private List<string> customBonusArray = new List<string>();


    //Бонус только для бонусных блоков
    public bool bonusOnlyBonusBlock;

    //Прочие параметры
    private string _nameTag;
    private bool isCustom = false;

    //==================Конец параметров==================

    //Сериализация материала/скина
    [System.Serializable]
    public class SetObject {
        //Материал из чего создается объект по умолчанию
        public GameObject EnemyPrefab;
        //Родитель объекта
        //public Object ParentBlock; - не нужен так как родитель и создает данный набор объектов
    }

    //Сериализация создания объектов по кругу от родителя
    [System.Serializable]
    public class CreateAround {
        //Расстояние до игрока
        [Range(0.1f, 50)] 
        public float Distance = 2;
        //Угол занимаемый врагами (360 - вся окружность)
        [Range(0, 360)]
        public float Angle = 90;
        //Количество объектов
        [Range(1, 200)] 
        public int count = 10;
    }

    //Линия
    [System.Serializable]
    public class CreateLine {
        [Range(1, 200)]
        public int count = 10;

        public Vector2 point1;

        public bool useCoordinates;
        public Vector2 point2;

        public bool useAngle;
        [Range(0, 360)]
        public float Angle = 90;
        [Range(0.1f, 20)]
        public float distance = 1f;

        //Переключатель эффектов
        public FunctionBuildingScenes function;
        //+
        public bool useEffectForX;
        public bool useEffectForY;
        [Range(0.1f, 2f)]
        public float bendingStrength = 0.1f;
        public bool reverse = false;

        public bool useSmoothX;
        [Range(-100, 100)]
        public float smoothXPower;
        public bool useSmoothY;
        [Range(-100, 100)]
        public float smoothYPower;
    }
    

    //Параметры для дочерних объектов
    [System.Serializable]
    public class SetOfProperties {
        //Тип
        //public enum type block;

        //Поведение
        //public bool rotate;

        //Характеристики
        //public float speed;

        //Данный набор передаем при создании
    }



    /**
     * Инициализируем сериализацию
     **/
    public SetObject gp = new SetObject();
    
    public bool aroundIsCreate = false;
    public CreateAround ab = new CreateAround();

    public bool lineIsCreate = false;
    public CreateLine line = new CreateLine();
    
    public PrefabCollectionBonus collectBonusPrefab = new PrefabCollectionBonus();
    public PrefabCollectionBlock collectBlockPrefab = new PrefabCollectionBlock();

    /**
     * Операции
     */

    void Awake () {
        EnterLinksArrayEnum();
        ConfigBlock.tmpStyleBlockStatic = selectStyleLevelBlock.ConfigSelection.ToString();
    }

    void Start () {
        //Определяем начальную точку
        Vector3 point = transform.position;
        
        //Переводим в радианы
        ab.Angle = ab.Angle * Mathf.Deg2Rad;

        //Определяем способ создания блоков (Кругом, || линией)
        if (aroundIsCreate) { ArondCreate(ab.Angle, point);
        } else if (lineIsCreate) { LineCreate(point); }
    }

    /**
     * Операции по созданию
     */

    //Кругом - расчет
    private void ArondCreate (float _angle, Vector3 _point) {
        //Цикл - по количеству блоков
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

            //Рассчитываем координату Y для наследников
            float _y = transform.position.y + Mathf.Cos(_angle / ab.count * i) * ab.Distance;
            _point.y = _y;

            //Рассчитываем координату X для наследников
            float _x = transform.position.x + Mathf.Sin(_angle / ab.count * i) * ab.Distance;
            _point.x = _x;
            

            //Создаём наследников
            Object _enemy = gp.EnemyPrefab;
            
            if (isCustom) {
                _enemy = customBlockArray[i - 1];
                _nameTag = customBlockArray[i - 1].tag;
            } else if (!isCustom) {
                _enemy = gp.EnemyPrefab;
                _nameTag = gp.EnemyPrefab.tag;
            }

            //Передаем следующей функции на реализацию
            createEnemy(_point, _enemy, isCustom, _typeCustomBonusString);
            
            //Отрубаем и зануляем перед новым циклом
            isCustom = false;
            _typeCustomBonusString = null;
        }
    }

    //Создание объекта на сцене
    private void createEnemy (Vector3 _point, Object _enemy, bool _isCustom, string _typeCustomBonus) {
        //Debug.Log("_point " + _point);
        //Инициализируем объект
        GameObject newObj;
        
        //Запихиваем на сцену
        newObj = (GameObject) Instantiate(_enemy, _point, Quaternion.identity);
        
        //Задаем настройки и и теги
        newObj.transform.parent = transform;
        newObj.gameObject.tag = _nameTag;

        //Устанавливаем бонусы по необходимости
        newObj.GetComponent<BonusDrop>().onScript = true;
        if (bonusOnlyBonusBlock && _enemy.name == "bonus_black" || bonusOnlyBonusBlock && _enemy.name == "bonus_white" || !bonusOnlyBonusBlock)
        newObj.GetComponent<BonusDrop>().SetBonusEnabled(_typeCustomBonus);
    }
       
    //Линий - расчет
    private void LineCreate (Vector3 _point) {
        float _distance = 0;
        //float _vector = 0;
        float _stepx = 0;
        float _stepy = 0;
        float _angle = Mathf.Deg2Rad * line.Angle;
        Vector2 _point1 = line.point1;
        Vector2 _point2 = line.point2;

        _point = _point1;

        if (line.useAngle) {
            _distance = line.distance;
            _point2 = new Vector2((_point.x + (Mathf.Cos(_angle) * (_distance))), (_point.y + (Mathf.Sin(_angle) * (_distance))));
            //_point2 = new Vector2((_point.x + (_angle * (_distance * line.count))), (_point.y + (_angle * (_distance * line.count))));
            line.point2 = _point2;
        } else if (line.useCoordinates && (!line.useAngle /* && ... other ... */)) {
            _distance = Vector2.Distance(_point1, _point2);
            Debug.Log("_distance " + _distance);
        } else if (!line.useCoordinates) {
            _distance = line.distance;
            if (_point2.x != 0 && _point2.y != 0) { line.point2 = _point2 *= _distance; } else { line.point2.x = line.point2.x = _point2.x = _point2.y = _distance; }
            Debug.Log("_distance " + _distance);
        }

        _stepx = calculateStepForLine(_point1.x, _point2.x, _stepx);
        _stepy = calculateStepForLine(_point1.y, _point2.y, _stepy);

        Debug.Log(_stepx);
        Debug.Log(_stepy);

        float procentXforEffects = calculateProcentTwoNumeric(_stepx, _stepy);
        float procentYforEffects = calculateProcentTwoNumeric(_stepy, _stepx);
        Debug.Log(" % " + procentXforEffects);
        Debug.Log(" % " + procentYforEffects);

        float pointPeregib = line.count / 2;
        float duble_i = 0;

        for (int i = 1; i <= line.count; i++) {
            if (!line.reverse) { if (pointPeregib < i) { duble_i++; } else { duble_i--; } } else { if (pointPeregib < i) { duble_i--; } else { duble_i++; } }

            //Модернизируем в соответствии с условиями
            //if swith case effects +
            if (line.useEffectForX) _point.x = ManualCallFunction(duble_i);
            if (line.useEffectForY) _point.y = ManualCallFunction(duble_i);
            if (line.useEffectForX && line.useEffectForY) {
                _point.x = ManualCallFunction(duble_i) * procentXforEffects / 100f;
                Debug.Log(" _point.x " + _point.x);
                _point.y = ManualCallFunction(duble_i) * procentXforEffects / 100f;
            }
            //Debug.Log("tmps " + tmps);

            //Debug.Log("DO SMOOTH _point.x " + _point.x);
            if (line.useSmoothX)
                _point.x = smoothLine(false, _point.x, true, (int) convertSign(duble_i), Mathf.RoundToInt(line.count / 2));
            if (line.useSmoothY)
                _point.y = smoothLine(false, _point.y, true, (int) convertSign(duble_i), Mathf.RoundToInt(line.count / 2));
            //Debug.Log("SMOOTH _point.x " + _point.x);


            string _typeCustomBonusString = null;

            if (customBonusArray.Count > i - 1) {
                if (checkCustomBonus(i - 1)) {
                    _typeCustomBonusString = customBonusArray[i - 1];
                }
            }

            //Создаём потомка
            Object _enemy = gp.EnemyPrefab;
            if (isCustom) {
                _enemy = customBlockArray[i - 1];
                _nameTag = customBlockArray[i - 1].tag;
            } else if (!isCustom) {
                _enemy = gp.EnemyPrefab;
                _nameTag = gp.EnemyPrefab.tag;
            }

            //Отправляем на создание
            createEnemy(_point, _enemy, isCustom, _typeCustomBonusString);

            //Обнуляем перед новым циклом
            isCustom = false;
            //_typeCustomBonusString = null;

            //Определяем параметры
            _point = new Vector2(_point.x + _stepx, _point.y + _stepy);
            //Debug.Log(_point);
        }
    }

    private float calculateProcentTwoNumeric (float x, float y) {
        float z = 0;
        x = convertSign(x);
        y = convertSign(y);
        float summ = x + y;
        z = x / summ * 100f;
        return z;
    }

    //Конвертируем отрицательные значения
    private float convertSign (float _f) {
        if (Mathf.Sign(_f) == -1f) _f *= -1f;
        return _f;
    }
    //Конвертируем с условием
    private float convertSign (float _usl2, float _step) {
        if (_usl2 < 0 && _step >= 0) {
            _step *= -1f;
        }
        return _step;
    }
    //Часть функции для линии
    private float calculateStepForLine (float _point1, float _point2, float _step) {
        if (_point1 != _point2) {
            _step = (convertSign(_point2) / line.count) + (convertSign(_point1) / line.count);
            _step = convertSign(_point2, _step);
        } else {
            _step = 0;
        }
        return _step;
    }
    /** 
     * Функции расчета геометрии
     **/
    //"Ручной" вызов функции
    private float ManualCallFunction (float duble_i) {
        float x = 0;
        if (line.function.ConfigSelection.ToString() == "Parabola") x = Parabola(duble_i);
        if (line.function.ConfigSelection.ToString() == "Exponential") x = Exponential(duble_i);
        if (line.function.ConfigSelection.ToString() == "Sine") x = Sine(duble_i);
        return x;
    }

    //Экспонент
    private float Exponential (float x) {
        return (x * x) / (line.bendingStrength * 20);
    }

    //Парабола
    private float Parabola (float x) {
        x = (line.bendingStrength / 4) * x - 1f;
        return x * x;
    }

    //Синус
    private float Sine (float x) {
        x *= 10000;
        Debug.Log(" 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x) " + Mathf.Sin(2 * Mathf.PI * x) * (line.bendingStrength * 100));
        return (Mathf.Sin(2 * Mathf.PI * x)) * (line.bendingStrength * 100);
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
            //Debug.Log("(_numelement - i)" + (_numelement - i));
        } else {
            _editNum = _editNum + (_editNum * ((i - _numelement) * tmpSMooth) / 100f);
            //Debug.Log("(i - _numelement)" + (i - _numelement));
        }


        return _editNum;
    }

    /**
     * Кастомные блоки и кастомные бонусы
     **/

    //Построение ссылок на ENUM бонусы
    private void EnterLinksArrayEnum () {
        //Debug.Log(SelectBonusList.Count);
        int i = 0;
        string tmpS;

        foreach (ListBonus nodeList in SelectBonusList) {
            //Debug.Log(nodeList.ConfigSelection);
            tmpS = (nodeList.ConfigSelection).ToString();
            customBonusArray.Add(tmpS);
            i++;
        }

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

    //Проверка на то что блок кастомный
    private bool checkCustomBlock (int _index) {
        if (customBlockArray[_index] != null) {
            return true;
        }

        return false;
    }

    //Проверка на то что блок бонусный
    private bool checkCustomBonus (int _index) {
        if (customBonusArray[_index] != "") {
            return true;
        }
        return false;
    }


} 
