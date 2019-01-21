/*Ethan Seiber
 * Date: 1/21/19
 * File: Assignment 1
 * Description: This program is a store program. it uses multiple classes and inheritance to define multiple types of items you would find in a store. 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Item> ItemList = new List<Item>();//the list of items that will be operated on throughout the program
            bool controller = true;//used to control when the program will exit
            int count = 0;//used to get the index of an item

            while (controller)
            {
                Console.WriteLine("\n\nMenu");
                Console.WriteLine("1) Do you wish to add an item?");
                Console.WriteLine("2) Restock an item?");
                Console.WriteLine("3) Sell an item?");
                Console.WriteLine("4) Or do you want info on the items?");

                string answer = Console.ReadLine();//users choice from the menu

                if (answer == "1")//user chose menu item 1
                {
                    Console.WriteLine("What is the name of the item?");
                    string ItemName = Console.ReadLine();//the user created name for a new item to be added

                    Console.WriteLine("What type of item is it? (Movie, Book, Toy)");
                    string ItemType = Console.ReadLine();//the tyoe of item specified by the user. Used to determine what class the item belongs to

                    if (ItemType == "Movie" || ItemType == "movie")//determines if the item belongs to the Movie class and adds it to the ItemList as a movie object
                    {
                        Item MovieObj = new Movie(ItemName, ItemType);
                        ItemList.Add(MovieObj);
                    }

                    else if (ItemType == "Book" || ItemType == "book")//determines if the item belongs to the Book class and adds it to the ItemList as a book object
                    {
                        Item BookObj = new Book(ItemName, ItemType);
                        ItemList.Add(BookObj);

                    }

                    else if (ItemType == "Toy" || ItemType == "toy")//determines if the item belongs to the Toy class and adds it to the ItemList as a toy object
                    {
                        Item ToyObj = new Toy(ItemName, ItemType);
                        ItemList.Add(ToyObj);

                    }

                    else//in case the user names a type that isn't a class
                        Console.WriteLine("Sorry there was a problem with your item type");
                }

                else if (answer == "2")//Menu choice 2 is chosen so this should restock the item chosen
                {
                    foreach (Item x in ItemList)//provides a list of items to choose from to the user for restocking
                    {
                        Console.Write("\n\n" + Convert.ToString(count + 1) + ") ");//item number
                        x.Info();

                        count++;
                    }
                    Console.WriteLine("\nChoose an item from this list to Restock");
                    string choice = Console.ReadLine();//users choice of item is held

                    Console.WriteLine("How much of that item do you wish to restock?");
                    string amount = Console.ReadLine();//the amount the user wishes to restock

                    try//makes sure the user doesn't try to access an item that doesn't yet exist
                    {
                        ItemList[Convert.ToInt32(choice) - 1].Restock(Convert.ToInt32(amount));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Sorry the item you chose from the list did not exist meaning you chose an item that was not on the list or you gave bad input");
                    }

                    count = 0;//resets count so it can be used again in choice 3 if needed
                }

                else if (answer == "3")//user chose menu item 3 which means this should sell a chosen item
                {
                    foreach (Item x in ItemList)//loops through the list of items printing them for the user to choose an item from the list to sell
                    {
                        string counter = Convert.ToString(count + 1);
                        Console.Write("\n\n" + counter + ") ");
                        x.Info();

                        count++;
                    }

                    Console.WriteLine("\nChoose an item from this list to sell");
                    string choice = Console.ReadLine();//user makes their choice of item
                    int UserChoice = Convert.ToInt32(choice) - 1;

                    Console.WriteLine("How many of this item do you wish to sell?");
                    string amount = Console.ReadLine();//the stock of the item the user wishes to sell

                    try//makes sure the user chooses a valid item and not one that was never added to the list
                    {
                        ItemList[UserChoice].Sell(Convert.ToInt32(amount));
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Sorry the item you chose from the list did not exist meaning you chose an item that was not on the list or you gave bad input");
                    }

                    count = 0;//count is reset in case it is to be reused in menu choice 2
                }

                else if (answer == "4")//menu choice 4 is chosen which means this should print all the information about the items including item name, type, and stock
                {
                    foreach (Item x in ItemList)
                    {
                        Console.Write("\n\n");
                        x.Info();//writes the information of the item
                    }
                }

                else
                    Console.WriteLine("I'm sorry but you didn't choose a menu item on the list");

                Console.WriteLine("\n\nDo you wish to continue? Y/N");
                string UserAnswer = Console.ReadLine();//gets whether the user wishes to continue with the program or if the user is finished and wants to quit

                if (UserAnswer == "N" || UserAnswer == "n")
                    controller = false;//while loop terminates and so does the program
            }

        }
    }
}

abstract class Item
{

    string name;//private item name: like ShamWow!
    string type;//private item type: holds whether item is a Movie etc.

    public Item(string Name, string ObjType)//parameterized constructor
    {
        name = Name;
        type = ObjType;
    }
    protected virtual string GetName() { return name; }//returns the name of an item
    protected string getType() { return type; }//returns the item type
    public abstract void Restock(int RestockAmount);//the abstract declaration of the restock function that takes an int amount as a parameter and adds it to the stock
    public abstract void Sell(int ItemsSold);//the abstract declaration of the sell function that takes an int amount as a parameter and sells that many items
    public abstract void Info();//the abstract declaration of the info function that lists info about each item such as the item name, type, and the current stock
}

class Book : Item//the Book class inherits from the Item class
{
    int stock;//the amount of that item in stock

    public Book(string Name, string ObjType) : base(Name, ObjType)//parameterized constructor that calls the base class constructor for variable initialization
    {
        stock = 0;
    }

    public override void Restock(int RestockVal)//restocks the stock of the item
    {
        if (RestockVal >= 0)
            stock += RestockVal;
        else
            Console.WriteLine("You're trying to add a negative amount of stock?");
    }
    public override void Sell(int ItemsSold)//sells a particular amount of the stock and makes sure the user doesn't try to sell more than is in stock
    {
        if (stock >= ItemsSold && ItemsSold >= 0)
            stock -= ItemsSold;

        else
        {
            stock = 0;
            Console.WriteLine("We unfortunately either did't have enough of that item to sell or you tried to sell a negative amount");
        }
    }

    public override void Info()//prints the info on each item such as the item name, type, stock
    {

        Console.Write("\tItem name: " + GetName() + "\n\tItem Type: " + getType() + "\n\tStock: " + Convert.ToString(stock));
    }
}

class Movie : Item//Movie class inherits from the Item class
{
    int stock;//the amount of that item in stock

    public Movie(string Name, string ObjType) : base(Name, ObjType)//parameterized constructor that calls the base class constructor for variable initialization   
    {
        stock = 0;
    }

    public override void Restock(int RestockVal)//restocks the stock of the item
    {
        if (RestockVal >= 0)
            stock += RestockVal;
        else
            Console.WriteLine("You're trying to add a negative amount of stock?");
    }
    public override void Sell(int ItemsSold)//sells a particular amount of the stock and makes sure the user doesn't try to sell more than is in stock
    {
        if (stock >= ItemsSold && ItemsSold >= 0)
            stock -= ItemsSold;

        else
        {
            stock = 0;
            Console.WriteLine("We unfortunately either did't have enough of that item to sell or you tried to sell a negative amount");
        }
    }
    public override void Info()//prints the info on each item such as the item name, type, stock
    {
        Console.Write("\tItem name: " + GetName() + "\n\tItem Type: " + getType() + "\n\tStock: " + Convert.ToString(stock));
    }
}

class Toy : Item//the Toy class inherits from the Item class
{
    int stock;//the amount of that item in stock

    public Toy(string Name, string ObjType) : base(Name, ObjType)//parameterized constructor that calls the base class constructor for variable initialization
    {
        stock = 0;
    }

    public override void Restock(int RestockVal)//restocks the stock of the item
    {
        if (RestockVal >= 0)
            stock += RestockVal;
        else
            Console.WriteLine("You're trying to add a negative amount of stock?");
    }

    public override void Sell(int ItemsSold)//sells a particular amount of the stock and makes sure the user doesn't try to sell more than is in stock
    {
        if (stock >= ItemsSold && ItemsSold >= 0)
            stock -= ItemsSold;

        else
        {
            stock = 0;
            Console.WriteLine("We unfortunately either did't have enough of that item to sell or you tried to sell a negative amount");
        }
    }
    public override void Info()//prints the info on each item such as the item name, type, stock
    {
        Console.Write("\tItem name: " + GetName() + "\n\tItem Type: " + getType() + "\n\tStock: " + Convert.ToString(stock));
    }
}