using System;
using System.Collections.Generic;
using System.Text;


namespace Translate
{
    public class Translate
    {
        private static Exception Incorrectentry = new Exception("Неправильная запись");
        private static Exception irrationalfraction = new Exception("Ответ это иррациональная дробь");
        static private int[] CoNveRt(string a, int G)  //Перевод чисел из строки в int
                                                       //В том числе и букв A,B и другие которые переводятся как 10(одной цифрой можно сказать), 11 и т.д
        {
            int[] b = new int[100];
            for (int i = 0; i < a.Length; i++)
            {
                if ((a[i] >= 'A') && (a[i] <= 'Z'))
                {
                    b[i] = Convert.ToInt32(a[i]) - 'A' + 10;
                    if (b[i] >= G) { throw (Incorrectentry); }
                }
                else
                {
                    if ((((Convert.ToInt32(a[i]) < '0')) || ((Convert.ToInt32(a[i]) > '9') && (Convert.ToInt32(a[i]) < 'A')) || (Convert.ToInt32(a[i]) > 'Z')) && (Convert.ToInt32(a[i]) != '.') && (Convert.ToInt32(a[i]) != ','))
                    {
                        throw (Incorrectentry); //if который строчкой выше проверяет все знаки, которые могут быть в записи числа
                    }
                    else { b[i] = Convert.ToInt32(a[i]) - '0'; }
                }
                if (b[i] >= G)
                {
                    throw (Incorrectentry);
                }
            }
            return b;
        }
        static private int in10(int b, int[] N1, int k) //Перевод целого числа из b-ичной в десятичную
        {
            int N, S = 1;
            N = 0;
            int[] a = new int[k];
            for (int i = 0; i < k; i++)
            {
                a[i] = N1[k - i - 1];
                N += a[i] * S;
                S *= b;
            }

            return N;
        }
        static private string inB(int b, int N1) //Перевод целого числа из десятичного в b-ичный
        {
            int i = 0;
            int[] a = new int[100];
            while (N1 != 0)
            {
                a[i] = N1 % b;
                N1 /= b;
                i++;
            }
            ;
            i--;
            string C = "";
            for (; i >= 0; i--)
            {
                if (a[i] >= 10) { C += Convert.ToChar(a[i] + 55); }
                else
                {
                    C += a[i];
                }
            }
            return C;
        }


        static private string inBDouble(int b, double N2) //Перевод дробной части числа из десятичной в b-ичной
        {
            int i = 0;
            int[] a = new int[100];
            string C = "";
            do
            {
                double k = N2 * b;
                a[i] = Convert.ToInt32(Math.Floor(k));
                N2 = N2 * b - a[i];
                i++;
            } while (N2 != 0);
            i--;
            for (int j = 0; j <= i; j++)
            {

                if (a[j] >= 10) { C += Convert.ToChar(a[j] + 55); }
                else
                {
                    C += a[j];
                }
            }
            return C;
        }

        static private double in10Double(int b, int[] N2) //Перевод дробной части из b-ичной в десятичную
        {
            double N = 0, S = (1.0) / b;
            for (int i = 0; i < N2.Length; i++)
            {
                N = N + N2[i] * S;
                S /= b;
            }
            return N;
        }



        static public string NS(int a, int b, string N1) //Перевод из одной системы исчисления в другую, из a в b с.с.
                                                         //N1 число, которое нужно перевести 
        {
            double N5;
            int[] N3 = new int[100]; //Строка в которой будет хранится целая часть числа, учитывая буквы
            int[] N10 = new int[100];//Строка в которой будет хранится дробная часть числа, учитывая буквы
            int k = 0; //Номер знака, после которого начинается дробная часть числа
            for (int i = 0; i < N1.Length; i++)
            {
                k++;
                if ((N1[i] == '.') || (N1[i] == ',')) { k--; break; }
            }
            string NeN = ""; //Строка в которой будет хранится дробная часть числа
            for (int i = k + 1; i < N1.Length; i++)
            {
                NeN += N1[i];
                if ((N1[i] == '.') || (N1[i] == ',')) //Проверка на множественные запятые или точки
                {
                    throw (Incorrectentry);
                }
            }
            string N = ""; //Строка в которой будет хранится целая часть числа
            for (int i = 0; i < k; i++)
            {
                N += N1[i];
            }
            N3 = CoNveRt(N, a); // Переводит буквы в числа для целой части
            int[] N4 = CoNveRt(NeN, a); //Переводит буквы в числа для дробной части
            string C, B; //Строки C - строка числа до дробной части, B - строка числа после дробной части
            int N2 = in10(a, N3, k); //Переводит целую часть числа в десятичную с.с.
            C = inB(b, Convert.ToInt32(N2)); //Переводит целую часть числа в нужную с.с.
            N5 = in10Double(a, N4); //Переводит дробную часть числа в десятичную с.с.
            B = inBDouble(b, N5); //Переводит дробную часть числа в нужную с.с.
            if (C == "") { C += 0; } //Используется для чисел по типу 0.15 (где целая часть ровна нулю)
            string A = C + "." + B; //Объединяет целую и дробную часть в одну строку
            if (A.Length > 30)
            { //Проверка на иррациональность
                throw (irrationalfraction);
            } //Проверка
            Console.WriteLine(A); //Выводит результат перевода из одной с.с. в другую (Если выводить не нужно, то можно просто убрать это)
            return A; //Возвращает
        }


    }
}