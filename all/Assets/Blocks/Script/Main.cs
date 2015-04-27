/*
������ �����.
����� Order http://unity3d.ru/
*/

using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	public class Node // ����� ���� 
	{
		public GameObject NodeGO;
		public Vector3 NodePos; //��������� ����
		public Vector3 NodeSupportPos; // ��������� �������� ����
		public int NodeIndex; // ������ ����
		public Node NodeLast; //���������� ����
		public Node NodeNext; // ��������� ����
		public Vector3[] NodeLine; // ����� ����� ���� �� ����������
		public int NodeLineSize;//���������� �����
		public float NodeLineDistanse; // ������ ����� �� ����� �� �����
	}
	
	Quaternion rot = new Quaternion(0,0,0,0);
	
	public Vector3[] Line = new Vector3[1000]; // ������ ����� �����
	public int LineSize = 0; // ������ �������
	
	public Node[] node; // ������ �����
	public int NodeCount = 0; // ������� �����
	public Node NodeLast;// ��������� ����
	public Node NodeCurent;//������� ����
	public Node NodeNext;//��������� ����
	public Vector3 NodeSupportPos; // ������� ������� �����
	public float LineStep = 1f;// ��� �����
	
	   
	public Color c1 = Color.yellow; // ���� �����
    public Color c2 = Color.red;//���� �����
	public int lengthOfLineRenderer; //���������� ��������� �����
	public LineRenderer lineRenderer;//����� ������
	public int lengthOfLineRenderer1;
	public LineRenderer lineRenderer1;	

	bool NodeSet; // ���� ��������� ����
	
	public GameObject CursorGO;// �� �������
	public GameObject SupportGO; // �� ������� �����
	public GameObject NodeGO; // �� ����
	
	public GameObject Line0; // �� �� ������� ����� ������ �����
	public GameObject Line1;
	
	public Vector3 CursorPos = new Vector3(0,0,0); // ������� �������
	
	void Start () 
	{
		
		node = new Node[100]; //���������� ������ �����
		if (CursorGO == null) CursorGO =  (GameObject)Instantiate(Resources.Load("Cursor"), CursorPos,rot); // ����� ������� ���� ��� ���
		Line0 =  (GameObject)Instantiate(Resources.Load("Line"), CursorPos,rot); // ����� �� ��� �����
		Line1 =  (GameObject)Instantiate(Resources.Load("Line"), CursorPos,rot);
		
		lengthOfLineRenderer = 10; ///��������� ������� �����
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
		CursorGO.transform.position = new Vector3(CursorPos.x,CursorPos.y,CursorPos.z); // ������������ �������
		UpdateImput(); // ����� �����
		UpdateNextNode(); // ���������� ���������� ����
		if (NodeSet) // ������� ��������� ����
		{
			NewNode(); //��������� ������ ����
			NodeSet = false; // ���� � ����
		}
		
		if (NodeLast != null ) // ���� ���� ����� ��� ���������� �� ����������� ������� �����
		{
			Vector3 direction = CursorGO.transform.position - NodeCurent.NodePos; // ������ �� ������� ���� � �������
			NodeSupportPos = NodeCurent.NodePos - direction; // ���������� ����� �� ����� �������, ��� �������� ����
			if (SupportGO == null) SupportGO =  (GameObject)Instantiate(Resources.Load("Support"), NodeSupportPos,rot);//����� �������� ���� ���� �� �� ����������
			if (NodeGO == null) NodeGO =  (GameObject)Instantiate(Resources.Load("Node"), NodeSupportPos,rot);// ����� �������� ���� ���� �� �� ����������
			SupportGO.transform.position = NodeSupportPos;//������������ ������� �����
			NodeGO.transform.position = NodeCurent.NodePos;//������������ �������� ����
		}
		Bezie(); // �������� ���������� ����� �����
		UpdateNode(); // ���������� ������� �����
		UpdateLine(); // ������ �����
	}

	public void UpdateImput() // ����� �����
	{
		CursorPos.x += Input.GetAxis("Mouse X")*1; // ���������� �����
		CursorPos.y += Input.GetAxis("Mouse Y")*1;
		if (Input.GetKeyDown(KeyCode.Mouse0)) NodeSet = true; // �� ��� ���� ��������� ����
	}	

	public void Bezie() // ������� ���������� ����� ����� �� ���� ������ ���������, ������� � ���������.
	{
		if (NodeLast == null || NodeCurent == null) return; // �� ������� ���� ����� ����
		/* ������� ������� ������ ���������� ��������� �� �����
		NodeCurent.NodeLineDistanse = Vector3.Distance(NodeCurent.NodePos, NodeLast.NodePos); 
		NodeCurent.NodeLineSize =  (int)Mathf.Round(Mathf.Abs(NodeCurent.NodeLineDistanse)/LineStep);
		if (NodeCurent.NodeLineSize <= 1) NodeCurent.NodeLineSize = 3;
		*/
		
		float t = 0.1f;//��� �������
		float DeltaT;//��� t
		//if (NodeCurent.NodeLineSize > 0) DeltaT = 1/NodeCurent.NodeLineSize; else DeltaT = 0.1f;
		NodeCurent.NodeLineSize = 10;// ���������� ����� �� �����
		DeltaT = 0.1f;//
		NodeCurent.NodeLine = new Vector3[NodeCurent.NodeLineSize];// ������� ������� ����� ����� ����

		for (int i = 1; i < NodeCurent.NodeLineSize ; i++) // ����������� t � ������� ����� �����
		{
			float tP0 = t*t - 2*t + 1;
			float tP1 = 2*t - 2*t*t;
			float tP2 = t*t;
			float X = tP0 * NodeLast.NodePos.x + tP1 * NodeSupportPos.x + tP2 * NodeCurent.NodePos.x;
			float Y = tP0 * NodeLast.NodePos.y + tP1 * NodeSupportPos.y + tP2 * NodeCurent.NodePos.y;
			NodeCurent.NodeLine[i] = new Vector3(X,Y,0);
			t += DeltaT;
		}
		NodeCurent.NodeLine[0] = NodeLast.NodePos; // ��������� ����� 
		NodeCurent.NodeLine[NodeCurent.NodeLineSize-1] = NodeCurent.NodePos; // �������� �����
		DrawLines(NodeCurent); // ��������� � �����
	}
	

	
	public void UpdateNextNode() //���� �� �����
	{
		if (NodeNext == null) return;// �� ������� ���� ���� ���
		NodeNext.NodePos = new Vector3(CursorPos.x,CursorPos.y,CursorPos.z); // ������������ ���������� ����
	}
	
	public void NewNode() //��������� ������ ����
	{
		NodeLast = NodeCurent; // ��������� ���� �������������� ��������
		if (NodeNext != null) NodeCurent = NodeNext; else NodeCurent = new Node();//����  ��������� ���� ���� �� �������� ���� �������������� ���������, ����� ������� ��������� �� ����
		NodeNext = new Node();// ��������� ��� ���������� ����
		NodeNext.NodeLast = NodeCurent; // ��������� ���������  ���� ������� 
		NodeCurent.NodeNext = NodeNext; //��������� ������� ���� �������
		//NodeCurent.NodeGO = (GameObject)Instantiate(Resources.Load("Cursor"), CursorPos,rot);
		NodeCurent.NodePos = CursorPos; 
		NodeCurent.NodeSupportPos = NodeSupportPos;
		NodeCurent.NodeIndex = NodeCount;
		node[NodeCount] = NodeCurent; // ��������� ������ ����
		NodeCount++;//�������������� ������� ����
		
	}	
	
	public void UpdateNode()// ���������� ����� , � ������ ����� ����������� ������� ����� ���������� ����� ����������� �� �����
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
	
	public void UpdateLine () // ������ �����
	{
		lengthOfLineRenderer = LineSize;
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		for (int i = 0; i < LineSize; i++)
		{
			lineRenderer.SetPosition(i, Line[i]);
		}		
	}	
	
	public void DrawLines(Node node) // ������ ����� � ���������
	{
		for (int i = 0; i < node.NodeLineSize; i++)
		{
			if ( i-1 >= 0)	Debug.DrawLine (node.NodeLine[i-1], node.NodeLine[i], Color.red);
			lineRenderer1.SetPosition(i, node.NodeLine[i]);
		}
	}
}
