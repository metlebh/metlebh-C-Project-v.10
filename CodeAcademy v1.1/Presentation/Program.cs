using Core.Constant;
using Core.Helpers;
using Core1.Entities;
using Data.Repostories.Concret;
using Presentation.Sevices;
using System;
using System.Globalization;

namespace Presentation
{

    class Program
    {
        private readonly static GroupService _grooupSevice;
        static Program()
        {
            _grooupSevice = new GroupService();
        }

        static void Main(string[] args)

        {
            while (true)
            {
            WelcomeDes: ConsoleHelper.WriteWithColor("****Welcome CodeAcademy****", ConsoleColor.Cyan);

                ConsoleHelper.WriteWithColor("1-Groups", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("2-Students", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("0-Exit", ConsoleColor.DarkYellow);

                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputer number is not correct format", ConsoleColor.DarkRed);
                    goto WelcomeDes;
                }
                else
                {
                    switch (number)
                    {
                        case (int)MainMenuOptions.Groups:
                            while (true)
                            {

                                GroupDes: ConsoleHelper.WriteWithColor("1-Creat Group", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("2-Update Group", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("3-Delete Group", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("4-Get All Groups", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("5-Get Group By İd", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("6-Get Group By Name", ConsoleColor.DarkYellow);
                                ConsoleHelper.WriteWithColor("0-Exit", ConsoleColor.DarkYellow);

                                ConsoleHelper.WriteWithColor("**** Select Option ****", ConsoleColor.Cyan);

                                isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                if (!isSucceeded)
                                {
                                    ConsoleHelper.WriteWithColor("The entered number is not in the correct format ", ConsoleColor.DarkRed);
                                }
                                else
                                    switch (number)
                                    {
                                        case (int)Group_options.CreatGroup:
                                            _grooupSevice.Creat();
                                            break;
                                        case (int)Group_options.UpdateGroup:
                                            _grooupSevice.Update();
                                            break;
                                        case (int)Group_options.DeleteGroup:
                                            _grooupSevice.Delete();
                                            break;
                                        case (int)Group_options.GetAllGroups:
                                            _grooupSevice.GetAll();
                                            break;
                                        case (int)Group_options.GetGroupById:
                                            _grooupSevice.GetGroupById();
                                            break;
                                        case (int)Group_options.GetGroupByName:
                                            _grooupSevice.GetGroupByName();
                                            break;
                                        case (int)Group_options.Exit:
                                            _grooupSevice.Exit();
                                            break;

                                        default:
                                            ConsoleHelper.WriteWithColor("Inputed Number is not exsist", ConsoleColor.DarkRed);
                                            goto GroupDes;

                                    }


                            }



                        default:
                            ConsoleHelper.WriteWithColor("The entered number is not available\nEnter 0-6 digits", ConsoleColor.DarkRed);
                            goto WelcomeDes;
                            break;
                    }
                }


            }







        }
    }
}
