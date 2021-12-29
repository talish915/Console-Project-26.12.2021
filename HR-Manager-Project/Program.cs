using System;

namespace HR_Manager_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(  "test");
            do
            {
                Console.ResetColor();
                Console.WriteLine("-------------------------Human Resource Manager---------------------------");
                Console.WriteLine("Yerine Yetirmek Isdediyniz Emeliyyatin Qarsisindaki Nomreni Daxil Edin:");
                Console.WriteLine("1 - Departamentlerin Siyahisi");
                Console.WriteLine("2 - Yeni Departament Yaratmaq");
                Console.WriteLine("3 - Departamentde Deyisiklik Etmek");
                Console.WriteLine("4 - Iscilerin Siyahisi");
                Console.WriteLine("5 - Departamentdeki Iscilerin Siyahisi");
                Console.WriteLine("6 - Isci Elave Etmek");
                Console.WriteLine("7 - Isci Uzerinde Deyisiklik Etmek");
                Console.WriteLine("8 - Departamentden Isci Silmek");
                Console.Write("Daxil Edin:");
                string choose = Console.ReadLine();
                int chooseNum;
                int.TryParse(choose, out chooseNum);
                switch (chooseNum)
                {
                    case 1:
                        Console.Clear();
                        
                        break;
                    case 2:
                        Console.Clear();
                        
                        break;
                    case 3:
                        Console.Clear();
                        
                        break;
                    case 4:
                        Console.Clear();
                        
                        break;
                    case 5:
                        Console.Clear();
                        
                        break;
                    case 6:
                        Console.Clear();
                        
                        break;
                    case 7:
                        Console.Clear();
                        
                        break;
                    case 8:
                        Console.Clear();
                        
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Duzgun Daxil Edin!");
                        break;
                }

            } while (true);
        }

    }
}
