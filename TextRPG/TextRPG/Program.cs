using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Intro intro = new Intro();
        TownMenu town = new TownMenu();

        intro.StartScene();
        intro.Naming();
        intro.JobSelect();

        town.Name = intro.name;
        town.Job = intro.job;

        town.Town();
    }
}

class Intro()
{
    public string loadingText = ">세계를 불러오는 중...<";

    public void StartScene()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.SetCursorPosition(0, 0);
            if (i % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(loadingText);
            }
            else
            {
                Console.WriteLine(new string(' ', loadingText.Length + 8));
            }
            Thread.Sleep(500);
        }
        Console.SetCursorPosition(0, 1);
        Console.Write("\t> Press Any Key <");
        Console.ReadKey();
        Console.Clear();

        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Title = "모험의 시작";
        Console.WriteLine("\t\\\\ 텍스트 RPG 게임 //");
        Thread.Sleep(1000);

        Console.WriteLine("\t \\\\ 스파르타 던전 //");
        Thread.Sleep(1000);
    }

    public string name;
    public void Naming()
    {
        Console.WriteLine("\n당신의 이름을 입력해주세요.\n");
        name = Console.ReadLine();

        //네, 아니오 선택지
        Console.WriteLine($"\n당신의 이름은 정말 {name}입니까?");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n1. 네\n2. 아니오");
        Console.ForegroundColor = ConsoleColor.Gray;

        string input = Console.ReadLine();
        if (int.TryParse(input, out int number))
        {
            switch (number)
            {
                case 1:
                    Console.Clear();
                    break;

                case 2:
                    Console.Clear();
                    Naming();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Console.Clear();

                    Naming();
                    break;
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1000);
            Console.Clear();

            Naming();
        }
    }

    public string job;
    public void JobSelect()
    {
        Console.WriteLine("\n당신의 직업을 선택해주세요.");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n1. 전사\n2. 도적");
        Console.ForegroundColor = ConsoleColor.Gray;

        string input1 = Console.ReadLine();
        if (int.TryParse(input1, out int number1))
        {
            switch (number1)
            {
                case 1:
                    job = "전사";
                    break;

                case 2:
                    job = "도적";
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    Console.Clear();

                    JobSelect();
                    break;
            }
            //네, 아니오 선택지
            Console.WriteLine($"\n정말 {job}을(를) 고르시겠습니까?");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n1. 네\n2. 아니오");
            Console.ForegroundColor = ConsoleColor.Gray;

            string input2 = Console.ReadLine();
            if (int.TryParse(input2, out int number2))
            {
                switch (number2)
                {
                    case 1:
                        Console.Clear();
                        break;

                    case 2:
                        Console.Clear();
                        JobSelect();
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        Console.Clear();

                        JobSelect();
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
                Console.Clear();

                JobSelect();
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1000);
            Console.Clear();

            JobSelect();
        }
    }
}

public class TownMenu
{
    private List<Item> inventory = new List<Item>();
    private Item equippedItem = null;

    public string Name { get; set; }
    public string Job { get; set; }

    public int Level { get; set; } = 1;
    public int HP { get; set; } = 100;
    public int ATK { get; set; } = 10;
    public int DEF { get; set; } = 5;
    public int Gold { get; set; } = 1000;

    public void Town()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[ 마을 ]\n");
            Console.ResetColor();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n정비를 하고 떠날 수 있습니다.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n0. 종료");
            Console.ResetColor();
            Console.Write("\n>> ");

            switch (Console.ReadLine())
            {
                case "1": ShowStatus(); break;
                case "2": ShowInventory(); break;
                case "3": ShowShop(); break;
                case "0": return;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void ShowStatus()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[ 상태 보기 ]\n");
        Console.ResetColor();

        Console.WriteLine($"Lv. {Level}\n{Name} ({Job})\nHP: {HP}\nATK: {ATK}\nDEF: {DEF}\nGold: {Gold} G");

        ReturnToMenu();
    }
    private void ShowShop()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[ 상점 ]\n");
        Console.ResetColor();

        var shopItems = new List<Item>
        {
            new Item("회복약", "체력을 소량 회복합니다.", 50),
            new Item("철검", "초보자용 검입니다.", 200),
        };

        for (int i = 0; i < shopItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {shopItems[i]}");
        }

        Console.WriteLine("0. 돌아가기");
        Console.Write("\n구매할 아이템 번호 입력 >> ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= shopItems.Count)
        {
            var selectedItem = shopItems[choice - 1];

            if (Gold >= selectedItem.Price)
            {
                Gold -= selectedItem.Price;
                inventory.Add(selectedItem);
                Console.WriteLine($"\n{selectedItem.Name}을(를) 구매했습니다!");
            }
            else
            {
                Console.WriteLine("\n골드가 부족합니다.");
            }
        }

        Thread.Sleep(1000);
    }

    private void ReturnToMenu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n0. 돌아가기");
        Console.ResetColor();
        Console.ReadKey();
    }

    private void ShowInventory()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 인벤토리 ]\n");
            Console.ResetColor();

            if (inventory.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    string equippedTag = (inventory[i] == equippedItem) ? "[E] " : "";
                    Console.WriteLine($"{i + 1}. {equippedTag}{inventory[i].Name} - {inventory[i].Description}");
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 돌아가기");
            Console.ResetColor();
            Console.Write("\n>> ");

            string input = Console.ReadLine();
            if (input == "0") break;
            else if (input == "1") ManageEquipment();
        }
    }
    private void ManageEquipment()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[ 장착 관리 ]\n");
            Console.ResetColor();

            if (inventory.Count == 0)
            {
                Console.WriteLine("보유 중인 아이템이 없습니다.");
                Thread.Sleep(1000);
                return;
            }

            for (int i = 0; i < inventory.Count; i++)
            {
                string equippedTag = (inventory[i] == equippedItem) ? "[E] " : "";
                Console.WriteLine($"{i + 1}. {equippedTag}{inventory[i].Name} - {inventory[i].Description}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n0. 돌아가기");
            Console.ResetColor();
            Console.Write("\n장착/해제할 아이템 번호 입력 >> ");

            string input = Console.ReadLine();
            if (input == "0") break;

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= inventory.Count)
            {
                var selectedItem = inventory[choice - 1];

                if (equippedItem == selectedItem)
                {
                    equippedItem = null;
                    Console.WriteLine($"\n{selectedItem.Name}을(를) 장착 해제했습니다.");
                }
                else
                {
                    equippedItem = selectedItem;
                    Console.WriteLine($"\n{selectedItem.Name}을(를) 장착했습니다.");
                }

                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
            }
        }
    }

}

public class Item
{
    public string Name {get;}
    public string Description {get;}
    public int Price {get;}

    public Item(string name, string description, int price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Description} (가격: {Price} G)";
    }
}


