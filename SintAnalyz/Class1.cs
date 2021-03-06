﻿using System;

namespace SintAnalyz
{
    class Sint
    {
        public string Code;
        public void ErrorMessage(string S1, string S2) => Console.WriteLine("Синтаксическая ошибка между " + S1 + " и " + S2);
        public void Analyz(string Code, string[] Term, bool Error, bool Loop)
        {
            string[] Text = Code.Split(' ');

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
                if (int.TryParse(Text[T], out _)) Text[T] = "CONST";

                else
                {
                    //проверка идентификатора и его правильности
                    char[] Word = Text[T].ToCharArray();
                    if (Word[0] >= 'A' && Word[0] <= 'Z' || Word[0] >= 'a' && Word[0] <= 'z')
                    {
                        if (Word.Length == 1)
                        {
                            Text[T] = "IND";
                            continue;
                        }
                        for (int i = 1; i < Word.Length; i++)
                            if (Word[i] >= 'A' && Word[i] <= 'Z'
                                || Word[i] >= 'a' && Word[i] <= 'z'
                                || Word[i] >= '0' && Word[i] <= '9')
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
    }
}
