using System.ComponentModel;
using System.Data;
using System.Net.Security;

namespace RPGText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            // 캐릭터 생성 
            Character character = new Character();
            // 인벤토리 생성
            Inventory inventory = new Inventory();

            Item Armor1 = new Item("수련자의 갑옷", " 수련에 도움을 주는 갑옷입니다. ", 0, 5, 1000);
            Item Armor = new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 2000);
            Item SpartaArmor = new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 3500);
            Item Spear = new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 1500);
            Item Sword = new Item("낡은 검", " 쉽게 볼 수 있는 낡은 검 입니다.", 2, 0, 600);
            Item axe = new Item("청동 도끼", " 어디선가 사용됐던거 같은 도끼입니다.", 7, 0, 1800);
            Item sujin = new Item("수진이의 사랑", "이 분의 사랑만 있으면 공격력이 올라갑니다.",100,0,50000);

            inventory.AddItem(Armor);
            inventory.AddItem(Spear);
            inventory.AddItem(Sword);
            inventory.AddItem(axe);
            inventory.AddItem(Armor1);
            inventory.AddItem(SpartaArmor);
            inventory.AddItem(sujin);




            while (true)
            {

                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    // 상태 보기
                    case 1:
                        ShowStatus.DisplayStatus(character);
                        break;

                    // 인벤토리 
                    case 2:
                        Inventory.ShowItems(character);
                        break;

                    // 상점 
                    case 3:Store.ShowStore(inventory, character);
                        break;

                    default: Console.WriteLine("다시 입력해주세요! "); break;
                }
            }

        }
    }

    public class Character
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }

        public Character()
        {
            Level = 1;
            Name = "sujin";
            Job = "전사";
            Attack = 10;
            Defense = 5;
            Health = 100;
            Gold = 1500;
        }



    }

    public class ShowStatus
    {
        public static bool DisplayStatus(Character character)
        {
            Console.WriteLine("\n**상태 보기**");
            Console.WriteLine("캐릭터의 상태가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Lv. {character.Level}");
            Console.WriteLine($"{character.Name} ( {character.Job} )");
            Console.WriteLine($"공격력 : {character.Attack}");
            Console.WriteLine($"방어력 : {character.Defense}");
            Console.WriteLine($"체 력 : {character.Health}");
            Console.WriteLine($"Gold : {character.Gold} G");


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = int.Parse(Console.ReadLine());

            return input == 0;

        }

    }

    public class Item
    {
        public string Name { get; set; }  // 아이템 이름 
        public string Description { get; set; }  // 아이템 설명 
        public int AttackPower { get; set; }  // 공격력 
        public int DefensePower { get; set; }   // 방어력 
        public int Gold { get; } // 가격

        public bool Purchase { get; set; }

        public Item(string name, string description, int attack, int defense, int gold)
        {
            Name = name;
            Description = description;
            AttackPower = attack;
            DefensePower = defense;
            Gold = gold;
            Purchase = false;
        }
    }

    public class Inventory : Character
    {
        public static List<Item> items = new List<Item>(); // 모든 아이템
        public static List<Item> equippedItems = new List<Item>(); // 장착된 아이템
        public static List<Item> PurchaseItems = new List<Item>(); // 구매한 아이템


        public Inventory()
        {
            // 처음 초기화 
        }

        public static void ShowItems(Character character)
        {
            Console.WriteLine("\n**인벤토리**");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            // 보유한 아이템 띄우기 
            for (int i = 0; i < PurchaseItems.Count; i++)
            {
                string equipped = equippedItems.Contains(PurchaseItems[i]) ? "[E]" : "";
                string Statistics = PurchaseItems[i].AttackPower > 0 ? $"공격력: {PurchaseItems[i].AttackPower}" :
                      PurchaseItems[i].DefensePower > 0 ? $"방어력: {PurchaseItems[i].DefensePower}" : "";
                Console.WriteLine($"{i + 1}. {equipped} {PurchaseItems[i].Name} | {Statistics} |  {PurchaseItems[i].Description}");

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = int.Parse(Console.ReadLine());


            if (input == 1)
            {
                ShowEquippedItems(character);
            }
            else if (input == 0)
            {
                // 나가기
            }
            else
            {
                Console.WriteLine("**잘못된 입력입니다**");
            }

        }

        // 1번 누르면 장착 관리 들어가서 

        public static void ShowEquippedItems(Character character)
        {

            Console.WriteLine("\n**인벤토리 - 장착관리**");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine();
            // 보유한 아이템 목록 띄우기 
            int index = 1;
            for (int i = 0; i < PurchaseItems.Count; i++)
            {
                if (PurchaseItems[i].Purchase)
                {
                    string equipped =equippedItems.Contains(PurchaseItems[i]) ? "[E]" : "";
                    string Statistics = PurchaseItems[i].AttackPower > 0 ? $"공격력: {PurchaseItems[i].AttackPower}" :
                      PurchaseItems[i].DefensePower > 0 ? $"방어력: {PurchaseItems[i].DefensePower}" : "";
                    Console.WriteLine($"{index}. {equipped} {PurchaseItems[i].Name} | {Statistics} | {PurchaseItems[i].Description}");
                    index++;
                }
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 0: return; 
                default:
                    // 사용자 입력에 따라 선택된 아이템 인덱스 계산
                    int selectedItemIndex = input - 1;
                    if (selectedItemIndex >= 0 && selectedItemIndex < PurchaseItems.Count)
                    {
                        EquipItem(selectedItemIndex, character);
                    }
                    else
                    {
                        Console.WriteLine("**잘못된 입력입니다**");
                    }
                    break;

            }

            static void EquipItem(int input, Character c)
            {
                // index 값이 유효하고 아이템 리스트 크기보다 작은지 
                if (input >= 0 && input < items.Count)
                { 

                    // 선택된 아이템 확인 
                    Item selectedItem = PurchaseItems[input];
                    bool isEquipped = equippedItems.Contains(selectedItem);

                    if (!isEquipped) // 아이템이 이미 장착되어 있는지 확인
                    {
                        // 장착되지 않았다면 추가 
                        equippedItems.Add(selectedItem);
                        Console.WriteLine($"아이템 '{selectedItem.Name}'을(를) 장착했습니다.");
                        c.Attack += selectedItem.AttackPower;
                        c.Defense += selectedItem.DefensePower; 
                        UpdateCharacter(c, selectedItem);
                    }
                    else
                    {
                        equippedItems.Remove(selectedItem);
                        Console.WriteLine($"아이템 '{selectedItem.Name}'을(를) 장착 해제했습니다.");
                        c.Attack -= selectedItem.AttackPower; 
                        c.Defense -= selectedItem.DefensePower;
                        UpdateCharacter(c, selectedItem);
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

        }

        // 아이템 장착 및 해제 메소드 


        // 아이템 추가 메소드 
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void AddEPItem(Item item)
        {
            PurchaseItems.Add(item);
            equippedItems.Add(item);
        }

        // 캐릭터 업데이트 메소드 
        public static void UpdateCharacter(Character character, Item item)
        {

            bool set = false;  // 아이템 착용 , 미착용
            //만약에 추가가 됐다면 실행

            ShowStatus.DisplayStatus(character);

        }
    }



    public class Store : Character
    {
        public string Name { get; set; }  // 아이템 이름 
        public string Description { get; set; }  // 아이템 설명 
        public int AttackPower { get; set; }  // 공격력 
        public int DefensePower { get; set; }   // 방어력 
        public int Gold { get; set; } // 가격 
        public Store(string name, string des, int attack, int defense, int gold)
        {
            Name = name;
            Description = des;
            AttackPower = attack;
            DefensePower = defense;
            Gold = gold;
        }


        public static void ShowStore(Inventory inventory, Character character)
        {
            Console.WriteLine("**상점**");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{character.Gold} G");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < Inventory.items.Count; i++)
            {
                string status = Inventory.items[i].Purchase ? "구매완료" : $"{Inventory.items[i].Gold} G";
                string Statistics = Inventory.items[i].AttackPower > 0 ? $"공격력: {Inventory.items[i].AttackPower}" :
                      Inventory.items[i].DefensePower > 0 ? $"방어력: {Inventory.items[i].DefensePower}" : "";
                Console.WriteLine($"- {Inventory.items[i].Name} | {Statistics} | {Inventory.items[i].Description} | {status}");
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = int.Parse(Console.ReadLine());

            if (input == 1)
            {
                PurchasedMogi(character, inventory);
            }
            else if(input == 2)
            {
                Resale(character, inventory);
            }
            else
            {
                // 나가기 
            }


        }

        static void PurchasedMogi(Character character, Inventory inventory)
        {
         
            for (int i = 0; i < Inventory.items.Count; i++)
            {
                string status = Inventory.items[i].Purchase ? "구매완료" : $"{Inventory.items[i].Gold} G";
                string Statistics = Inventory.items[i].AttackPower > 0 ? $"공격력: {Inventory.items[i].AttackPower}" :
                      Inventory.items[i].DefensePower > 0 ? $"방어력: {Inventory.items[i].DefensePower}" : "";
                Console.WriteLine($"- {i + 1}. {Inventory.items[i].Name} | {Statistics} | {Inventory.items[i].Description} | {status}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("구매할 아이템의 번호를 누르세요.");
            Console.Write(">> ");
            Console.WriteLine();
            Console.WriteLine();
            int input = int.Parse(Console.ReadLine());

            // input 값 확인하기 
            if (Inventory.items.Count >= input && 1<=input )
            {
                Item selectedItem = Inventory.items[input - 1];
                if (selectedItem.Purchase)
                {
                    // 아이템 구매
                    Console.WriteLine("이미 구매한 아이템 입니다!");
                }
                else if(character.Gold >= selectedItem.Gold)
                {
                    // 재화 감소
                    character.Gold -= selectedItem.Gold;
                    selectedItem.Purchase = true;
                    Inventory.PurchaseItems.Add(selectedItem);  // 인벤토리에 추가 
                    Console.WriteLine("구매를 완료했습니다!");
                }
                else if(character.Gold < selectedItem.Gold)
                {
                    Console.WriteLine("골드가 부족합니다..");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                } 
            }
        }

        static void Resale(Character character,Inventory inventory)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine($"보유 골드 {character.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            int index = 1;
            for (int i = 0; i < Inventory.PurchaseItems.Count; i++)
            {
                if (Inventory.PurchaseItems[i].Purchase)
                {
                    string equipped = Inventory.equippedItems.Contains(Inventory.PurchaseItems[i]) ? "[E]" : "";
                    string Statistics = Inventory.PurchaseItems[i].AttackPower > 0 ? $"공격력: {Inventory.PurchaseItems[i].AttackPower}" :
                      Inventory.PurchaseItems[i].DefensePower > 0 ? $"방어력: {Inventory.PurchaseItems[i].DefensePower}" : "";
                    Console.WriteLine($"{index}. {equipped} {Inventory.PurchaseItems[i].Name} | {Statistics} | {Inventory.PurchaseItems[i].Description}");
                    index++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("판매할 아이템의 번호를 눌러주세요.");
            Console.Write(">>");
            int input = int.Parse(Console.ReadLine());
       

            switch (input)
            {
                case 0: return;
                default:
                    // 사용자 입력에 따라 선택된 아이템 인덱스 계산
                    int selectedItemIndex = input - 1;
                    Item selectedItem = Inventory.PurchaseItems[selectedItemIndex];
                    if (selectedItemIndex >= 0 && selectedItemIndex < Inventory.PurchaseItems.Count)
                    {
                        Console.WriteLine($"'{selectedItem.Name}'을(를) {selectedItem.Gold * 0.85} G에 판매하였습니다.");
                     
                        // 장착중이면 해제
                        if (Inventory.equippedItems.Contains(selectedItem))
                        {
                            Inventory.equippedItems.Remove(selectedItem);
                        }
                        // 아이템 판매 및 골드 추가
                        character.Gold += (int)(selectedItem.Gold * 0.85);
                        Inventory.PurchaseItems.Remove(selectedItem);

                    }
                    else
                    {
                        Console.WriteLine("**잘못된 입력입니다**");
                    }
                    break;

            }
            // 뭐여

        }


    }

}