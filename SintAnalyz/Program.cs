using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SintAnalyz
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Term = { "WHILE", "(", ")", "IND", "CONST", "DO", ":=", ";",
                "<", ">", "<=", ">=", "=", "<>", 
                "+", "-", "*", "/", "div", "mod" };
            bool Error = false, Loop;

            void ErrorMessage (string S1, string S2)
            {
                Console.WriteLine("Синтаксическая ошибка между " + S1 + " и " + S2);
            }

            while (!Error)
            {

                Console.WriteLine("Введите код программы! \n");
                string[] Text = Console.ReadLine().Split(' ');

                //преобразование кода
                for (int T = 0; T < Text.Length; T++)
                {
                    Loop = false;
                    //проверка на служебное
                    for (int Te = 0; Te < Term.Length; Te++)
                        if (T < Text.Length)
                            if (Text[T] != Term[Te])
                                continue;
                            else Loop = true;

                    if (Loop) continue;
                    //проверка на число
                    if (int.TryParse(Text[T], out _))
                        Text[T] = "CONST";
                    else
                    {
                        //проверка идентификатора и его правильности
                        char[] Word = Text[T].ToCharArray();
                        if ((int)Word[0] >= 65 && (int)Word[0] <= 90 || (int)Word[0] >= 97 && (int)Word[0] <= 122)
                        {
                            if (Word.Length == 1)
                            {
                                Text[T] = "IND";
                                continue;
                            }
                            for (int i = 1; i < Word.Length; i++)
                                if ((int)Word[i] >= 65 && (int)Word[i] <= 90
                                    || (int)Word[i] >= 97 && (int)Word[i] <= 122
                                    || (int)Word[i] >= 48 && (int)Word[i] <= 57)
                                    Text[T] = "IND";
                        }
                        else
                        {
                            Console.WriteLine("Ошибка именования лексемы: " + Text[T]);
                            Error = true;
                        }
                    }
                }

                //синтаксический анализ
                if (!Error)
                {
                    int T = 0;
                    if (Text[T] == "WHILE" && Text[T + 1] == "(")
                    {
                        T += 2;
                        if (Text[T] == "IND" || Text[T] == "CONST")
                        {
                            T++;
                            if (Text[T] == "<" || Text[T] == ">" || Text[T] == "<=" || Text[T] == ">=" || Text[T] == "=" || Text[T] == "<>"
                                || Text[T] == ")")
                            {
                                if (Text[T] != ")")
                                {
                                    if (Text[T + 1] != "IND" && Text[T + 1] != "CONST")
                                        ErrorMessage(Text[T], Text[T + 1]);
                                    else if (Text[T + 2] != ")")
                                        ErrorMessage(Text[T + 1], Text[T + 2]);
                                    T += 3;
                                }
                                else T++;
                                if (Text[T] == "DO")
                                {
                                    T++;
                                    if (Text[T] == "IND")
                                    {
                                        T++;
                                        if (Text[T] == ":=")
                                        {
                                            T++;
                                            if (Text[T] == "IND" || Text[T] == "CONST")
                                            {
                                                T++;
                                                if (Text[T] == "+" || Text[T] == "-" || Text[T] == "*" || Text[T] == "/" || Text[T] == "div" || Text[T] == "mod"
                                                    || Text[T] == ";")
                                                {
                                                    if (Text[T] != ";")
                                                    {
                                                        if (Text[T + 1] != "IND" && Text[T + 1] != "CONST")
                                                            ErrorMessage(Text[T], Text[T + 1]);
                                                        else if (Text[T + 2] != ";")
                                                            ErrorMessage(Text[T + 1], Text[T + 2]);
                                                    }
                                                    else Console.WriteLine("Синтаксический анализ успешно завершён!");
                                                }
                                                else ErrorMessage(Text[T - 1], Text[T]);
                                            }
                                            else ErrorMessage(Text[T - 1], Text[T]);
                                        }
                                        else ErrorMessage(Text[T - 1], Text[T]);
                                    }
                                    else ErrorMessage(Text[T - 1], Text[T]);
                                }
                                else ErrorMessage(Text[T - 1], Text[T]);
                            }
                            else ErrorMessage(Text[T - 1], Text[T]);
                        }
                        else ErrorMessage(Text[T - 1], Text[T]);
                    }
                    else ErrorMessage(Text[T], Text[T + 1]);

                }
            }
            Console.ReadLine();
        }
    }
}

//WHILE ( N ) DO AB := -18 + A ;
//WHILE ( A = B ) DO E := F div G ;
//WHILE ( C <> D ) DO E := F div G ;
//WHILE ( A ) DO N := 15 mod 2 ;
//WHILE ( BNK < -3 ) DO N := 15 mod 2 ;

