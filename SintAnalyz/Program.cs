using System;

namespace SintAnalyz
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Term = { "WHILE", "(", ")", "IND", "CONST", "DO", ":=", ";",
                            "<", ">", "<=", ">=", "=", "<>",
                            "+", "-", "*", "/", "div", "mod" };
            bool Error = false, Loop = false;

            while (!Error)
            {

                Console.WriteLine("Введите код программы! \n");
                Sint sint = new Sint();
                sint.Code = Console.ReadLine();
                sint.Analyz(sint.Code, Term, Error, Loop);

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

