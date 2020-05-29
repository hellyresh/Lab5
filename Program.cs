using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_Sokolova_Mediator
{
    /// <summary>
    /// Абстрактный класс посредника
    /// </summary>
    abstract class Mediator
    {
        /// <summary>
        /// Процедура для вывода сообщения определенному участнику сделки.
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="colleague">Кому отправляется</param>
        public abstract void Send(string msg, Colleague colleague);
    }

    /// <summary>
    /// Абстрактный класс Colleague
    /// </summary>
    abstract class Colleague
    {
        Mediator mediator;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">посредник для взаимодействия</param>
        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Процедура для отправки уведомления
        /// </summary>
        /// <param name="message">Сообщение одному из участников сделки</param>
        public void Send(string message)
        {
            mediator.Send(message, this);
        }
        ///<summary>
        /// процедура для наследования остальными коллегами(участниками) сделки
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        public abstract void Notify(string message);
    }

    /// <summary>
    /// Класс исполнителя
    /// </summary>
    class Singer : Colleague
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">посредник для взаимодействия</param>
        public Singer(Mediator mediator)
            : base(mediator)
        { }
        /// <summary>
        /// Процедура для вывода сообщения
        /// </summary>
        /// <param name="message">выводимое сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение исполнителю: " + message);
        }
    }
    /// <summary>
    /// Класс приглашенной группы (подтанцовка)
    /// </summary>
    class Group : Colleague
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">посредник для взаимодействия</param>
        public Group(Mediator mediator)
            : base(mediator)
        { }

        /// <summary>
        /// Процедура для уведомления участника сделки
        /// </summary>
        /// <param name="message">выводимое сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение группе: " + message);
        }
    }
    /// <summary>
    /// класс руководителя концертного зала
    /// </summary>
    class Headmaster : Colleague
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="mediator">Посредник для взаимодействия</param>
        public Headmaster(Mediator mediator)
            : base(mediator)
        { }
        /// <summary>
        /// Процедура уведомления участника сделки
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение руководителю концертного зала: " + message);
        }
    }
    /// <summary>
    /// Класс концертного менеджера (посредник)
    /// </summary>
    class ManagerMediator : Mediator
    {
        /// <summary>
        /// Конструктор исполнителя
        /// </summary>
        public Colleague Singer;
        /// <summary>
        /// Конструктор агента исполнителя
        /// </summary>
        public Colleague Group;
        /// <summary>
        /// Конструктор руководителя концертного зала
        /// </summary>
        public Colleague Headmaster;
        /// <summary>
        /// Процедура Вывода сообщения на экран
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="colleague">Кому предназначено сообщение</param>
        public override void Send(string msg, Colleague colleague)
        {
            if (Singer == colleague)
                Headmaster.Notify(msg);
            else if (Group == colleague)
                Headmaster.Notify(msg);
            else if (Headmaster == colleague)
            {
                Singer.Notify(msg);
                Group.Notify(msg);
            }
                
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ManagerMediator mediator = new ManagerMediator();
            Colleague singer = new Singer(mediator);
            Colleague group = new Group(mediator);
            Colleague headmaster = new Headmaster(mediator);
            mediator.Singer = singer;
            mediator.Group = group;
            mediator.Headmaster = headmaster;
            singer.Send("Я известный исполнитель и хочу выступать в новом концертном зале");
            group.Send("Мы - молодая группа, хотим устроить у вас концерт");
            headmaster.Send("В нашем концертном зале места заняты на год вперед.");

            Console.ReadKey();
        }
    }
}
