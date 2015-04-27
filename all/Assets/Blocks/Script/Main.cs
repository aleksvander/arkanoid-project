/*
Кривые Безье.
Автор Order http://unity3d.ru/
*/

using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	public class Node // класс узла 
	{
		public GameObject NodeGO;
		public Vector3 NodePos; //кордината узла
		public Vector3 NodeSupportPos; // кордината опорного узла
		public int NodeIndex; // индекс узла
		public Node NodeLast; //предидущий узел
		public Node NodeNext; // следующий узел
		public Vector3[] NodeLine; // точки линии узла до предидущей
		public int NodeLineSize;//количество точек
		public float NodeLineDistanse; // длинна линии от точки до точки
	}
	
	Quaternion rot = new Quaternion(0,0,0,0);
	
	public Vector3[] Line = new Vector3[1000]; // массив точек линии
	public int LineSize = 0; // размер массива
	
	public Node[] node; // массив узлов
	public int NodeCount = 0; // счетчик узлов
	public Node NodeLast;// последний узел
	public Node NodeCurent;//текущий узел
	public Node NodeNext;//следующий узел
	public Vector3 NodeSupportPos; // розиция опорной точки
	public float LineStep = 1f;// шаг линии
	
	   
	public Color c1 = Color.yellow; // цвет линии
    public Color c2 = Color.red;//цвет линии
	public int lengthOfLineRenderer; //количество сегментов линии
	public LineRenderer lineRenderer;//линия рендер
	public int lengthOfLineRenderer1;
	public LineRenderer lineRenderer1;	

	bool NodeSet; // флаг установки узла
	
	public GameObject CursorGO;// ГО курсора
	public GameObject SupportGO; // ГО опорной точки
	public GameObject NodeGO; // ГО узла
	
	public GameObject Line0; // ГО на котором будет рендер линии
	public GameObject Line1;
	
	public Vector3 CursorPos = new Vector3(0,0,0); // позиция курсора
	
	void Start () 
	{
		
		node = new Node[100]; //инициируем массив узлов
		if (CursorGO == null) CursorGO =  (GameObject)Instantiate(Resources.Load("Cursor"), CursorPos,rot); // спаун курсора если его нет
		Line0 =  (GameObject)Instantiate(Resources.Load("Line"), CursorPos,rot); // спаун ГО для линий
		Line1 =  (GameObject)Instantiate(Resources.Load("Line"), CursorPos,rot);
		
		lengthOfLineRenderer = 10; ///инициация рендера линий
		lineRenderer = Line0.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
		
		lengthOfLineRenderer1 = 10;
		lineRenderer1 = Line1.gameObject.AddComponent<LineRenderer>();
        lineRenderer1.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer1.SetColors(c2, c2);
        lineRenderer1.SetWidth(0.2F, 0.2F);
        lineRenderer1.SetVertexCount(lengthOfLineRenderer);
	}

	void Update () 
	{
		CursorGO.transform.position = new Vector3(CursorPos.x,CursorPos.y,CursorPos.z); // передвижение курсора
		UpdateImput(); // опрос ввода
		UpdateNextNode(); // обновление следующего узла
		if (NodeSet) // условие установки узла
		{
			NewNode(); //установка нового узла
			NodeSet = false; // флаг в ложь
		}
		
		if (NodeLast != null ) // если мало точек для построения не расчитывает опорную точку
		{
			Vector3 direction = CursorGO.transform.position - NodeCurent.NodePos; // вектор от ткущего узла к курсору
			NodeSupportPos = NodeCurent.NodePos - direction; // зеркальная точка от этого вектора, для опорного узла
			if (SupportGO == null) SupportGO =  (GameObject)Instantiate(Resources.Load("Support"), NodeSupportPos,rot);//спаун опорного узла если он не установлен
			if (NodeGO == null) NodeGO =  (GameObject)Instantiate(Resources.Load("Node"), NodeSupportPos,rot);// спаун текущего узла если он не установлен
			SupportGO.transform.position = NodeSupportPos;//передвижение опорной точки
			NodeGO.transform.position = NodeCurent.NodePos;//передвижение текущего узла
		}
		Bezie(); // алгоритм построения Безье линии
		UpdateNode(); // обновление массива линии
		UpdateLine(); // рисуем линию
	}

	public void UpdateImput() // опрос ввода
	{
		CursorPos.x += Input.GetAxis("Mouse X")*1; // координаты мышки
		CursorPos.y += Input.GetAxis("Mouse Y")*1;
		if (Input.GetKeyDown(KeyCode.Mouse0)) NodeSet = true; // по ЛКМ флаг установки узла
	}	

	public void Bezie() // считает координаты линии безье по трем точкам последней, текущей и следующей.
	{
		if (NodeLast == null || NodeCurent == null) return; // не входить если точек мало
		/* пытался сделать расчет количества сегментов на линию
		NodeCurent.NodeLineDistanse = Vector3.Distance(NodeCurent.NodePos, NodeLast.NodePos); 
		NodeCurent.NodeLineSize =  (int)Mathf.Round(Mathf.Abs(NodeCurent.NodeLineDistanse)/LineStep);
		if (NodeCurent.NodeLineSize <= 1) NodeCurent.NodeLineSize = 3;
		*/
		
		float t = 0.1f;//шаг функции
		float DeltaT;//шаг t
		//if (NodeCurent.NodeLineSize > 0) DeltaT = 1/NodeCurent.NodeLineSize; else DeltaT = 0.1f;
		NodeCurent.NodeLineSize = 10;// количество точек на линию
		DeltaT = 0.1f;//
		NodeCurent.NodeLine = new Vector3[NodeCurent.NodeLineSize];// задание массива точек линии узла

		for (int i = 1; i < NodeCurent.NodeLineSize ; i++) // подставляем t в функцию линии Безье
		{
			float tP0 = t*t - 2*t + 1;
			float tP1 = 2*t - 2*t*t;
			float tP2 = t*t;
			float X = tP0 * NodeLast.NodePos.x + tP1 * NodeSupportPos.x + tP2 * NodeCurent.NodePos.x;
			float Y = tP0 * NodeLast.NodePos.y + tP1 * NodeSupportPos.y + tP2 * NodeCurent.NodePos.y;
			NodeCurent.NodeLine[i] = new Vector3(X,Y,0);
			t += DeltaT;
		}
		NodeCurent.NodeLine[0] = NodeLast.NodePos; // начальная точка 
		NodeCurent.NodeLine[NodeCurent.NodeLineSize-1] = NodeCurent.NodePos; // конечная точка
		DrawLines(NodeCurent); // отрисовка в юнити
	}
	

	
	public void UpdateNextNode() //узел на мышке
	{
		if (NodeNext == null) return;// не входить если узла нет
		NodeNext.NodePos = new Vector3(CursorPos.x,CursorPos.y,CursorPos.z); // передвижение следующего узла
	}
	
	public void NewNode() //установка нового узла
	{
		NodeLast = NodeCurent; // Последний узел приравнивается текущему
		if (NodeNext != null) NodeCurent = NodeNext; else NodeCurent = new Node();//если  следующий узел есть то текущему узлу приварнивается слудующий, иначе текущий создается за ново
		NodeNext = new Node();// экземпляр для следующего узла
		NodeNext.NodeLast = NodeCurent; // заполняем следующий  узел данными 
		NodeCurent.NodeNext = NodeNext; //заполняем текуший узел данными
		//NodeCurent.NodeGO = (GameObject)Instantiate(Resources.Load("Cursor"), CursorPos,rot);
		NodeCurent.NodePos = CursorPos; 
		NodeCurent.NodeSupportPos = NodeSupportPos;
		NodeCurent.NodeIndex = NodeCount;
		node[NodeCount] = NodeCurent; // заполняем массив улов
		NodeCount++;//инкрементируем счетчик злов
		
	}	
	
	public void UpdateNode()// обновление узлов , в массив линии заполняются массивы узлов содержащих точки расчитанные по Безье
	{
		LineSize = 0;
		for (int i = 0; i < NodeCount; i++)
		{
			for (int j = 0; j < node[i].NodeLineSize; j++)
			{
				Line[LineSize] = new Vector3();
				Line[LineSize] = node[i].NodeLine[j];
				if(node[i+1] != null) LineSize++;
			}	
		}
	}
	
	public void UpdateLine () // рисуем линию
	{
		lengthOfLineRenderer = LineSize;
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		for (int i = 0; i < LineSize; i++)
		{
			lineRenderer.SetPosition(i, Line[i]);
		}		
	}	
	
	public void DrawLines(Node node) // рисуем линию в редакторе
	{
		for (int i = 0; i < node.NodeLineSize; i++)
		{
			if ( i-1 >= 0)	Debug.DrawLine (node.NodeLine[i-1], node.NodeLine[i], Color.red);
			lineRenderer1.SetPosition(i, node.NodeLine[i]);
		}
	}
}
