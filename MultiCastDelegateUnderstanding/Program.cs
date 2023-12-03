using System;
using System.Reflection;

namespace MulticastDelegateDemo
{
	public delegate void RectangleDelegate(double Width, double Height);
	public delegate void MathDelegate(int No1, int No2);
	public class Rectangle
	{
		public void GetArea(double Width, double Height)
		{
			Console.WriteLine($"Area is {Width * Height}");
		}
		public void GetPerimeter(double Width, double Height)
		{
			Console.WriteLine($"Perimeter is {2 * (Width + Height)}");
		}


		public static void Add(int x, int y)
		{
			Console.WriteLine($"Addition of {x} and {y} is : {x + y}");
		}
		//Static Method
		public static void Sub(int x, int y)
		{
			Console.WriteLine($"Subtraction of {x} and {y} is : {x - y}");
		}
		//Non-Static Method
		public void Mul(int x, int y)
		{
			Console.WriteLine($"Multiplication of {x} and {y} is : {x * y}");
		}
		//Non-Static Method
		public void Div(int x, int y)
		{
			Console.WriteLine($"Division of {x} and {y} is : {x / y}");
		}
		static void Main(string[] args)
		{
			Rectangle rect = new Rectangle();
			RectangleDelegate rectDelegate = new RectangleDelegate(rect.GetArea);
			// RectangleDelegate rectDelegate = rect.GetArea;

			// binding a method with delegate object
			// In this example rectDelegate is a multicast delegate. 
			// You use += operator to chain delegates together.

			rectDelegate += rect.GetPerimeter;

			Delegate[] InvocationList = rectDelegate.GetInvocationList();
			MethodInfo methodInfo = rectDelegate.Method;
			Console.WriteLine("InvocationList:");
			foreach (var item in InvocationList)
			{
				Console.WriteLine($"  {item}");
			}

			Console.WriteLine();
			Console.WriteLine("Invoking Multicast Delegate:");
			rectDelegate(23.45, 67.89);
			//rectDelegate.Invoke(13.45, 76.89);

			Console.WriteLine();
			Console.WriteLine("Invoking Multicast Delegate After Removing one Pipeline:");
			//Removing a method from delegate object
			rectDelegate -= rect.GetPerimeter;
			rectDelegate.Invoke(13.45, 76.89);


			// Another way of Invoking MultiCast Delegate(static and non-static method)
			//Static Method within the same class can be access directly
			MathDelegate del1 = new MathDelegate(Add);
			//Static Method can be access using class name
			MathDelegate del2 = new MathDelegate(Rectangle.Sub);
			//Non-Static Method must be access through object 
			MathDelegate del3 = new MathDelegate(rect.Mul);
			MathDelegate del4 = new MathDelegate(rect.Div); 
			//In this example del5 is a multicast delegate. 
			//We can use +(plus) operator to chain delegates together and 
			//-(minus) operator to remove a delegate.
			MathDelegate del5 = del1 + del2 + del3 + del4;
			Delegate[] InvocationList2 = del5.GetInvocationList();
			Console.WriteLine("InvocationList:");
			foreach (var item in InvocationList2)
			{
				Console.WriteLine($" {item}");
			}
			Console.WriteLine();
			Console.WriteLine("Invoking Multicast Delegate::");
			del5.Invoke(20, 5);
			Console.WriteLine();
			Console.WriteLine("Invoking Multicast Delegate After Removing one Delegate:");
			del5 -= del2;
			del5(22, 7);

			Console.ReadKey();
		}
	}
}