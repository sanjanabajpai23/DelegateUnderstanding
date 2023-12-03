using System;
using System.Reflection;

namespace DelegatesDemo
{
	public delegate void WorkPerformedHandler(int hours, WorkType workType);
	public delegate void DoSomeWorkHandler(string name);

	class Program
	{
		static void Main(string[] args)
		{
			WorkPerformedHandler del1 =
						new WorkPerformedHandler(Manager_WorkPerformed);
			del1(10, WorkType.Golf);

			WorkPerformedHandler del3 =
						new WorkPerformedHandler(Employee_WorkPerformed);
			del3(10, WorkType.Golf);
			//del1.Invoke(50, WorkType.GotoMeetings);

			DoSomeWorkHandler del2 = new DoSomeWorkHandler(DoneSomeWork);
			DoSomeWork(del2);

			MethodInfo Method = del1.Method;
			object Target = del1.Target;
			Delegate[] InvocationList = del1.GetInvocationList();

			Console.ReadKey();
		}

		public static void Manager_WorkPerformed(int workHours, WorkType wType)
		{
			Console.WriteLine("Work Performed by Event Handler");
			Console.WriteLine($"Work Hours: {workHours}, Work Type: {wType}");
		}

		public static void Employee_WorkPerformed(int workHours, WorkType wType)
		{
			Console.WriteLine("Work Performed by Event Handler");
			Console.WriteLine($"Work Hours: {workHours}, Work Type: {wType}");
		}

		public static void DoSomeWork(DoSomeWorkHandler del1)
		{
            Console.WriteLine("Calling method at run time");
			del1("Payal");	
        }

		public static void DoneSomeWork(string name)
		{
            Console.WriteLine("Name is : " + name);
        }
	}

	public enum WorkType
	{
		Golf,
		GotoMeetings,
		GenerateReports
	}
}