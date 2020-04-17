using System;
namespace lab08_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher publ = new Publisher("Наука и жизнь", "nauka@mail.ru", 1234, new DateTime(2014, 12, 14));
            Book b2 = new Book("Толстой Л.Н.", "Война и мир", publ, 1234, 2013, 101, true);
            b2.TakeItem();
            Magazine mag1 = new Magazine("О природе", 5, "Земля и мы", 2014, 1235, true);
            mag1.TakeItem();
            mag1.Subs();
            mag1.Print();
            Console.WriteLine("\n Тестирование полиморфизма");
            b2.ReturnSrok();
            Item it;
            it = b2;
            it.TakeItem();
            it.Return();
            it.Print();
            it = mag1;
            it.TakeItem();
            it.Return();
            it.Print();

        }
    }
    interface IPubs
    {
        void Subs();
        bool IfSubs { get; set; }
    }
    interface IPr
    {
        void SetPrice(double price);
        double PriceBook(int s);
    }
    class Publisher
    {
        struct LicenseNumber
        {
            public int licenseNumber;
            public DateTime data;

            public override string ToString()
            {
                string bs = String.Format("Лицензия N-{0} от {1} г.", licenseNumber, data.Year);
                return bs;
            }
        }

        public string Name { get; set; }
        public string EmailAdress { get; set; }
        private LicenseNumber LinNumber;

        public Publisher(string name, string emailAdress, int lnumber, DateTime data)
        {
            this.Name = name;
            this.EmailAdress = emailAdress;
            this.LinNumber.licenseNumber = lnumber;
            this.LinNumber.data = data;
        }

        public override string ToString()
        {
            string bs = String.Format(": {0}, электронный адрес: {1}, {2}", this.Name, this.EmailAdress, this.LinNumber);
            return bs;
        }
    }
    abstract class Item
    {
        protected long invNumber;
        protected bool taken;
        public bool IsAvaible()
        {
            if (taken)
                return true;
            else
                return false;
        }
        public long GetInvNumber()
        {
            return invNumber;
        }
        private void Take()
        {
            taken = false;
        }
        public abstract void Return();
        public virtual void Print()
        {
            Console.WriteLine("Состояние единицы хранения:\n Инвентарный номер: {0}\n Наличие: {1}", invNumber, taken);
        }
        public Item(long invNumber, bool taken)
        {
            this.invNumber = invNumber;
            this.taken = taken;
        }
        public Item()
        {
            this.taken = true;
        }
        public void TakeItem()
        {
            if (this.IsAvaible())
            {
                this.Take();
            }
        }
    }
    class Book : Item, IPr
    {
        public bool IfSubs { get; set; }
        public void Subs()
        {
            IfSubs = true;
        }
        public string Author { get; set; }
        public string Title { get; set; }
        //public string Publisher { get; set; }
        public Publisher Publ { get; set; }
        public int Pages { get; set; }
        public int Year { get; set; }
        public static double Price { get; set; }
        public bool returnSrok { get; private set; }

        public void SetBook(string author, string title, Publisher publisher, int pages, int year)
        {
            this.Author = author;
            this.Title = title;
            this.Publ = publisher;
            this.Pages = pages;
            this.Year = year;
        }

        public void SetPrice(double price)
        {
            Book.Price = price;
        }

        public override string ToString()
        {
            string bs = String.Format("\nКнига:\n Автор: {0}\n Название: {1}\n Год издания: {2}\n {3} стр.\n Стоимость аренды: {4}\n Издательство{5}", Author, Title, Year, Pages, Price, Publ.ToString());
            return bs;
        }

        public override void Print()
        {
            Console.WriteLine(this);
            base.Print();
        }

        public double PriceBook(int s)
        {
            double cust = s * Price;
            return cust;
        }

        public Book(string author, string title, Publisher publisher, int pages, int year, long invNumber, bool taken) : base(invNumber, taken)
        {
            this.Author = author;
            this.Title = title;
            this.Publ = publisher;
            this.Pages = pages;
            this.Year = year;
        }

        public Book() { }

        static Book()
        {
            Price = 9;
        }

        public Book(string author, string title)
        {
            this.Author = author;
            this.Title = title;
        }

        public void ReturnSrok()
        {
            returnSrok = true;
        }

        public override void Return()
        {
            if (returnSrok == true)
            {
                taken = true;
            }
            else
            {
                taken = false;
            }
        }
    }
    class Magazine : Item, IPubs
    { 
        public bool IfSubs { get; set; }
        public void Subs()
        {
            IfSubs = true;
        }
        public override string ToString()
        {
            string bs = String.Format("\nЖурнал:\n Том: {0}\n Номер {1}\n Название: {2} \n " +
                "Год выпуска: {3} \n Статус подписки: {4}",
                Volume, Number, Title, Year, IfSubs);
            return bs;
        }
        public string Volume { get; set; } // том
        public int Number { get; set; } // номер
        public string Title { get; set; } // название
        public int Year { get; set; } // год выпуска 
        public Magazine(string volume, int number, string title, int year, long invNumber, bool taken) : base(invNumber, taken)
        {
            this.Volume = volume;
            this.Number = number;
            this.Title = title;
            this.Year = year;
        }
        public Magazine()
        { }

        public override void Print()
        {
            Console.WriteLine(ToString());
            base.Print();
        }

        public override void Return() // операция "вернуть"
        {
            taken = true;
        }
    }
}



