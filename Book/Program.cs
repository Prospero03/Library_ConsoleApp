
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

 
namespace Library
{
    class Program
    {
        private static List<Book> books;

        static void Main(string[] args)
        {
            Console.WriteLine("Меню");
            Console.WriteLine("add - добавление книги");
            Console.WriteLine("list - список добавленных книг");
            Console.WriteLine("find_name - поиск книги по по названию книги");
            Console.WriteLine("find_author - поиск книги по имени автора");
            Console.WriteLine("find_page - поиск книги по количеству страниц");
            Console.WriteLine("delete - удаление книги");
            Console.WriteLine("exit - выход из программы");
            Console.WriteLine("");


            books = !File.Exists("lib.xml") ? new List<Book>() : DeserializeFromXML();
            bool flag = true;
            while (flag)
            {
                switch (Console.ReadLine())
                {
                    case "add":
                        Console.WriteLine("Что бы добавить книгу введите через запятую\n" +
                                          "Введите имя автора, Название книги, Год Издания , Количество страниц");
                        string readLine = Console.ReadLine();
                        if (readLine != null)
                        {
                            string[] temp = readLine.Split(',');

                            if (temp[0] == null || temp[0] == "")
                            {
                                temp[0] = "Неизвестен";
                            }
                            if (temp[1] == null || temp[1] == "")
                            {
                                temp[1] = "Неизвестна";
                            }
                            if (temp[2] == null || temp[2] == "")
                            {
                                temp[2] = "Неизвестно";
                            }
                            if (temp[3] == null || temp[3] == "")
                            {
                                temp[3] = "Неизвестно";
                            }

                            Add(temp[0], temp[1], temp[2], temp[3]);
                            
                        }
                        break;

                    case "exit":
                        //Выводим Список Книг
                        flag = false;
                        break;

                    case "list":
                        //Выводим Список Книг
                        List();
                        break;

                    case "delete":
                        Console.WriteLine("Введите название книги которую вы хотите удалить");
                        Delete(Console.ReadLine());
                        break;

                    case "find_name":
                        Console.WriteLine("Введите название книги, которую вы хотите найти");
                        Console.WriteLine("Если книга неизвестна, введите 'Неизвестна'");
                        FindName(Console.ReadLine());
                        break;

                    case "find_author":
                        Console.WriteLine("Введите автора книги, которую вы хотите найти");
                        Console.WriteLine("Если автор неизвестен, введите 'Неизвестен'");
                        FindAuthor(Console.ReadLine());
                        break;

                    case "find_page":
                        Console.WriteLine("Введите количество страниц в книге, которую вы хотите найти");
                        Console.WriteLine("Если количество страницы неизвестно, введите 'Неизвестно'");
                        FindPage(Console.ReadLine());
                        break;

                    case "save":
                        //Сохранение XML
                        SerializeToXML(books);
                        break;

                    case "load":
                        books = DeserializeFromXML();
                        break;

                    default:
                        Console.WriteLine("Нет такой команды");
                        break;
                }
            }
        }

        private static void Delete(string name)
        {
            foreach (Book book in books.Where(b => b.Name == name))
            {
                Console.WriteLine("Удаление Книги:");
                Console.WriteLine("Название: {0}, Автор: {1}, Год Издания: {2}, Количество Страниц: {3}", book.Name, book.Author, book.Year, book.PageCount);
                books.Remove(book);
                break;
            }
        }

        private static void FindName(string name)
        {
            foreach(Book book in books.Where(b => b.Name == name))
            {
                Console.WriteLine("Найдена Книга:");
                Console.WriteLine("Название: {0}, Автор: {1}, Год Издания: {2}, Количество Страниц: {3}", book.Name, book.Author, book.Year, book.PageCount);
                break;
            }
        }

        private static void FindAuthor(string author)
        {
            foreach(Book book in books.Where(b => b.Author == author))
            {
                Console.WriteLine("Найдена Книга:");
                Console.WriteLine("Название: {0}, Автор: {1}, Год Издания: {2}, Количество Страниц: {3}", book.Name, book.Author, book.Year, book.PageCount);
                break;
            }
        }

        private static void FindPage(string pageCount)
        {
            foreach(Book book in books.Where(b => b.PageCount == pageCount))
            {
                Console.WriteLine("Найдена Книга:");
                Console.WriteLine("Название: {0}, Автор: {1}, Год Издания: {2}, Количество Страниц: {3}", book.Name, book.Author, book.Year, book.PageCount);
                break;
            }
        }


        private static void List()
        {
            foreach (Book book in books)
            {
                Console.WriteLine("Название: {0}, Автор: {1}, Год Издания: {2}, Количество Страниц: {3}", book.Name, book.Author, book.Year, book.PageCount);     
            }
            
            
        }

        private static void Add(
            string author,
            string name,
            string year,
            string pageCount)
        {
            books.Add(new Book(author, name, year, pageCount));
        }


        static public void SerializeToXML(List<Book> lib)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            TextWriter textWriter = new StreamWriter(@"lib.xml");
            serializer.Serialize(textWriter, lib);
            textWriter.Close();
        }

        static List<Book> DeserializeFromXML()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Book>));
            TextReader textReader = new StreamReader(@"lib.xml");
            List<Book> lib = (List<Book>)deserializer.Deserialize(textReader);
            textReader.Close();

            return lib;
        }
    }
}


public class Book
{
    public string Author { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public string PageCount { get; set; }

    public Book(
        string author,
        string name,
        string year,
        string pageCount)

    {
        this.Author = author;
        this.Name = name;
        this.Year = year;
        this.PageCount = pageCount;
    }

    public Book()
    {

    }
}