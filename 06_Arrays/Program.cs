using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_Arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Foreach Döngüsü
            //Foreach(1;2;3;4)
            //1: değişken türü
            // 2: değişken adı
            // 3: in anahtar kelimesi
            // 4: Liste,Koleksiyon,Array

            //string[] cities = { "milano", "roma", "budapeşte", "ankara", "istanbul", "variova" };

            //foreach (string city in cities)
            //{
            //    Console.WriteLine(city);
            //}

            //int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //foreach (int i in numbers)
            //{
            //    Console.WriteLine(i);
            //}

            //foreach (int number in numbers)
            //{
            //    if (number%2 == 0)
            //    {
            //        Console.WriteLine(number);
            //    }
            //}
            //int total = 0;
            //foreach (int number in numbers)
            //{
            //    total += number;
            //}
            //Console.WriteLine(total);
            //List<int> numbers = new List<int>()
            //{
            //    1,2,3,4,5,8
            //};
            //foreach (int number in numbers)
            //{
            //    Console.WriteLine(number);
            //}

            //string word = "Merhaba";
            //foreach (var item in word)
            //{
            //    Console.WriteLine(item);
            //}




            #endregion
            #region Örnek Sınav Sistemi Uygulaması
            Console.WriteLine("***** C# Eğitim Kampı Sınav Uygulaması *****");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            // Sınıftaki öğrenci sayısını kullanıcıdan alma
            Console.WriteLine("-----------------------------");
            Console.Write("Sınıfınızda Kaç Öğrenci Var: ");
            int studentCount = int.Parse(Console.ReadLine());
            Console.WriteLine("-----------------------------");

            string[] studentNames = new string[studentCount];
            double[] studentExamAvg = new double[studentCount];





            #endregion
            Console.Read();
        }
    }
}
