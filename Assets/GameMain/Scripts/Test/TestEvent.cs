using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;


namespace StarForce

{
	///<summary>
	///�����¼�
	///<summary>

	public class TestEvent :MonoBehaviour
	{
		public int a;
        private void Start()
        {
			a = 10;
        }

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.A))
				GameEntry.Event.Subscribe(TestEventArgs.EventId, OnTestFire);

			if (Input.GetKeyDown(KeyCode.B))
			{
				GameEntry.Event.Fire(this, TestEventArgs.Create(111));
				Debug.Log("1 " + a);
				GameEntry.Event.FireNow(this, TestEventArgs.Create(222));
                Debug.Log("2 " + a);
            }

			if(Input.GetKeyDown(KeyCode.C))
				GameEntry.Event.Unsubscribe(TestEventArgs.EventId, OnTestFire);
        }

        private void OnTestFire(object sender, GameEventArgs e)
        {
            TestEventArgs ne = (TestEventArgs)e;
			Debug.Log(ne.Testint);
			a++;
            Debug.Log("3 " + a);
        }
    }

	
}

public sealed class TestEventArgs : GameEventArgs
{
    public override int Id 
		{ get { return EventId; } }

	public static readonly int EventId = typeof(TestEventArgs).GetHashCode();
	
	//�¼������Ĳ������Զ���
	public int Testint
	{ get; 
		set ; }

	//���
    public override void Clear()
    {
		Testint = default;
    }

	//����
	public static TestEventArgs Create(int inputTestint)
	{
		TestEventArgs ne = ReferencePool.Acquire<TestEventArgs>();
		ne.Testint = inputTestint;
		return ne;
	}
}