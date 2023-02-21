using Core.Helpers;
using Core1.Entities;
using Data.Repostories.Concret;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Sevices
{
    public class GroupService
    {
        private readonly Grouprepositery _groupRepository;
        public GroupService()
        {
            _groupRepository = new Grouprepositery();
        }
        public void GetAll()
        {
            ConsoleHelper.WriteWithColor("**** ALL GROUPS ****", ConsoleColor.DarkCyan);
            var groups = _groupRepository.GetAll();
            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax Size: {group.MaxSize}\nStart Date: {group.StartDate}\nEnd Date: {group.EndDate}", ConsoleColor.DarkMagenta);
            }
        }
        public void GetGroupById()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            NewGroupDes: ConsoleHelper.WriteWithColor("There is no existing group\n*Do you not want to create a group y/n", ConsoleColor.DarkRed);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)
                {
                    ConsoleHelper.WriteWithColor("Your selection is not in the correct format\n*Pleace write y/n", ConsoleColor.DarkRed);
                    goto NewGroupDes;
                }
                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("Your selection is not correct ", ConsoleColor.DarkRed);
                    goto NewGroupDes;
                }

                if (decision == 'y')
                {
                    Creat();
                }
                else if (decision == 'n')
                {
                    return;
                }

            }
            else
            {
                GetAll();
            IdCheck: ConsoleHelper.WriteWithColor("****Enter ID****", ConsoleColor.DarkCyan);

                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Id is not coorect format", ConsoleColor.DarkRed);
                    goto IdCheck;
                }

                var group = _groupRepository.Get(id);
                ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax Size: {group.MaxSize}\nStart Date: {group.StartDate}\nEnd Date: {group.EndDate}", ConsoleColor.DarkMagenta);
            }


        }
        public void GetGroupByName()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            NewGroupDes: ConsoleHelper.WriteWithColor("There is no existing group\n*Do you not want to create a group y/n", ConsoleColor.DarkRed);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)
                {
                    ConsoleHelper.WriteWithColor("Your selection is not in the correct format\n*Pleace write y/n", ConsoleColor.DarkRed);
                    goto NewGroupDes;
                }
                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("Your selection is not correct ", ConsoleColor.DarkRed);
                    goto NewGroupDes;
                }

                if (decision == 'y')
                {
                    Creat();
                }
                else if (decision == 'n')
                {
                    return;
                }

            }
            else
            {
                GetAll();
            NameCheck: ConsoleHelper.WriteWithColor("****Enter Name****", ConsoleColor.DarkCyan);
                string name = Console.ReadLine();
                var group = _groupRepository.GetByName(name);
                ConsoleHelper.WriteWithColor($"Id: {group.Id}\nName: {group.Name}\nMax Size: {group.MaxSize}\nStart Date: {group.StartDate}\nEnd Date: {group.EndDate}", ConsoleColor.DarkMagenta);
            }

        }
        public void Creat()
        {
            ConsoleHelper.WriteWithColor("****Enter Group Name****", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();

            int maxSize;
        MaxSizeDes: ConsoleHelper.WriteWithColor("****Enter Group Maxsize****", ConsoleColor.DarkCyan);
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max number is not in correct format", ConsoleColor.DarkRed);
                goto MaxSizeDes;
            }

            if (maxSize > 18)
            {
                ConsoleHelper.WriteWithColor("max number cannot be greater than 18", ConsoleColor.DarkRed);
                goto MaxSizeDes;
            }

        DateTimeDes: ConsoleHelper.WriteWithColor("****Enter Start Date****", ConsoleColor.DarkCyan);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!(isSucceeded))
            {
                ConsoleHelper.WriteWithColor("*Start Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                goto DateTimeDes;
            }
            DateTime boundryDate = new DateTime(2015, 1, 1);
            if (startDate < boundryDate)
            {
                ConsoleHelper.WriteWithColor("*Start date is not choosen right\n*Startig 01.01.2015", ConsoleColor.DarkRed);
                goto DateTimeDes;
            }

        EndDateTimeDes: ConsoleHelper.WriteWithColor("****Enter End Date****", ConsoleColor.DarkCyan);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!(isSucceeded))
            {
                ConsoleHelper.WriteWithColor("*End Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                goto EndDateTimeDes;
            }
            if (endDate < startDate)
            {
                ConsoleHelper.WriteWithColor("The end date cannot be greater than the start date", ConsoleColor.DarkRed);
                goto EndDateTimeDes;
            }

            var group = new Group
            {
                Name = name,
                MaxSize = maxSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            _groupRepository.Add(group);
            ConsoleHelper.WriteWithColor($"Group succesfuly created\nName: {group.Name}\nMaxSize: {group.MaxSize}\nStart Date: {group.StartDate.ToShortDateString()}\nEnd Date: {group.EndDate.ToShortDateString()}", ConsoleColor.DarkMagenta);


        }
        public void Update()
        {
            var groups = _groupRepository.GetAll();
            if (groups.Count == 0)
            {
            NewGroupDes: ConsoleHelper.WriteWithColor("There is no existing group\n*Do you not want to create a group y/n", ConsoleColor.DarkRed);
                char decision;
                bool isSucceededResult = char.TryParse(Console.ReadLine(), out decision);
                if (!isSucceededResult)
                {
                    ConsoleHelper.WriteWithColor("Your selection is not in the correct format\n*Pleace write y/n", ConsoleColor.DarkRed);
                    goto NewGroupDes;
                }
                if (!(decision == 'y' || decision == 'n'))
                {
                    ConsoleHelper.WriteWithColor("Your selection is not correct ", ConsoleColor.DarkRed);
                    goto NewGroupDes;
                }

                if (decision == 'y')
                {
                    Creat();
                }
                else if (decision == 'n')
                {
                    return;
                }

            }
            else
            {
                GetAll();
            UpdateCheck: ConsoleHelper.WriteWithColor("Enter group\nId--1\nName--2", ConsoleColor.DarkCyan);
                int number;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number is not correct format", ConsoleColor.DarkRed);
                    goto UpdateCheck;
                }
                if (!(number == 1 || number == 2))
                {
                    ConsoleHelper.WriteWithColor("Pleace choose 1 or 2", ConsoleColor.DarkRed);
                    goto UpdateCheck;
                }
                if (number == 1)
                {
                    ConsoleHelper.WriteWithColor("Enter Id", ConsoleColor.DarkCyan);
                    int id;
                IdCheck: isSucceeded = int.TryParse(Console.ReadLine(), out id);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Id is not correct format", ConsoleColor.DarkRed);
                        goto IdCheck;
                    }
                    var group = _groupRepository.Get(id);
                    if (group is null)
                    {
                        ConsoleHelper.WriteWithColor("There is no any group this id", ConsoleColor.DarkRed);
                        goto IdCheck;
                    }
                    ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.DarkCyan);
                    string name = Console.ReadLine();

                MaxSizeDes: ConsoleHelper.WriteWithColor("Enter new Maxsize", ConsoleColor.DarkCyan);
                    int maxSize;
                    isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Max size is not corrext format", ConsoleColor.DarkRed);
                        goto MaxSizeDes;
                    }
                    if (maxSize > 18)
                    {
                        ConsoleHelper.WriteWithColor("Groups cannot be higher than 18", ConsoleColor.DarkRed);
                        goto MaxSizeDes;
                    }

                DateTimeDes: ConsoleHelper.WriteWithColor("****Enter New Start Date****", ConsoleColor.DarkCyan);
                    DateTime startDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    if (!(isSucceeded))
                    {
                        ConsoleHelper.WriteWithColor("*Start Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                        goto DateTimeDes;
                    }
                    DateTime boundryDate = new DateTime(2015, 1, 1);
                    if (startDate < boundryDate)
                    {
                        ConsoleHelper.WriteWithColor("*Start date is not choosen right\n*Startig 01.01.2015", ConsoleColor.DarkRed);
                        goto DateTimeDes;
                    }
                EndDateTimeDes: ConsoleHelper.WriteWithColor("****Enter New End Date****", ConsoleColor.DarkCyan);
                    DateTime endDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                    if (!(isSucceeded))
                    {
                        ConsoleHelper.WriteWithColor("*End Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                        goto EndDateTimeDes;
                    }
                    if (endDate < startDate)
                    {
                        ConsoleHelper.WriteWithColor("The end date cannot be greater than the start date", ConsoleColor.DarkRed);
                        goto EndDateTimeDes;
                    }
                    group.Name = name;
                    group.MaxSize = maxSize;
                    group.StartDate = startDate;
                    group.EndDate = endDate;
                    _groupRepository.Update(group);


                }
                else
                {
                    NameCheck: ConsoleHelper.WriteWithColor("Enter Group Name", ConsoleColor.DarkCyan);
                    string name = Console.ReadLine();
                    var group = _groupRepository.GetByName(name);
                    if (group is null)
                    {
                        ConsoleHelper.WriteWithColor("There is no any group this name", ConsoleColor.DarkRed);
                        goto NameCheck;
                    }
                    ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.DarkCyan);
                    name = Console.ReadLine();

                MaxSizeDes: ConsoleHelper.WriteWithColor("Enter new Maxsize", ConsoleColor.DarkCyan);
                    int maxSize;
                    isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Max size is not corrext format", ConsoleColor.DarkRed);
                        goto MaxSizeDes;
                    }
                    if (maxSize > 18)
                    {
                        ConsoleHelper.WriteWithColor("Groups cannot be higher than 18", ConsoleColor.DarkRed);
                        goto MaxSizeDes;
                    }

                DateTimeDes: ConsoleHelper.WriteWithColor("****Enter New Start Date****", ConsoleColor.DarkCyan);
                    DateTime startDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                    if (!(isSucceeded))
                    {
                        ConsoleHelper.WriteWithColor("*Start Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                        goto DateTimeDes;
                    }
                    DateTime boundryDate = new DateTime(2015, 1, 1);
                    if (startDate < boundryDate)
                    {
                        ConsoleHelper.WriteWithColor("*Start date is not choosen right\n*Startig 01.01.2015", ConsoleColor.DarkRed);
                        goto DateTimeDes;
                    }
                EndDateTimeDes: ConsoleHelper.WriteWithColor("****Enter New End Date****", ConsoleColor.DarkCyan);
                    DateTime endDate;
                    isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                    if (!(isSucceeded))
                    {
                        ConsoleHelper.WriteWithColor("*End Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                        goto EndDateTimeDes;
                    }
                    if (endDate < startDate)
                    {
                        ConsoleHelper.WriteWithColor("The end date cannot be greater than the start date", ConsoleColor.DarkRed);
                        goto EndDateTimeDes;
                    }
                    group.Name = name;
                    group.MaxSize = maxSize;
                    group.StartDate = startDate;
                    group.EndDate = endDate;
                    _groupRepository.Update(group);
                }
            }

        }
        private void IntenalUpdate(Group group)
        {
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group this id", ConsoleColor.DarkRed);
                
            }
            ConsoleHelper.WriteWithColor("Enter new name", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();

        MaxSizeDes: ConsoleHelper.WriteWithColor("Enter new Maxsize", ConsoleColor.DarkCyan);
            int maxSize;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out maxSize);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Max size is not corrext format", ConsoleColor.DarkRed);
                goto MaxSizeDes;
            }
            if (maxSize > 18)
            {
                ConsoleHelper.WriteWithColor("Groups cannot be higher than 18", ConsoleColor.DarkRed);
                goto MaxSizeDes;
            }

        DateTimeDes: ConsoleHelper.WriteWithColor("****Enter New Start Date****", ConsoleColor.DarkCyan);
            DateTime startDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            if (!(isSucceeded))
            {
                ConsoleHelper.WriteWithColor("*Start Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                goto DateTimeDes;
            }
            DateTime boundryDate = new DateTime(2015, 1, 1);
            if (startDate < boundryDate)
            {
                ConsoleHelper.WriteWithColor("*Start date is not choosen right\n*Startig 01.01.2015", ConsoleColor.DarkRed);
                goto DateTimeDes;
            }
        EndDateTimeDes: ConsoleHelper.WriteWithColor("****Enter New End Date****", ConsoleColor.DarkCyan);
            DateTime endDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            if (!(isSucceeded))
            {
                ConsoleHelper.WriteWithColor("*End Date is not correct format\n*Example:dd.mm.yyyy", ConsoleColor.DarkRed);
                goto EndDateTimeDes;
            }
            if (endDate < startDate)
            {
                ConsoleHelper.WriteWithColor("The end date cannot be greater than the start date", ConsoleColor.DarkRed);
                goto EndDateTimeDes;
            }
            group.Name = name;
            group.MaxSize = maxSize;
            group.StartDate = startDate;
            group.EndDate = endDate;
            _groupRepository.Update(group);
        }
        public void Delete()
        {
            GetAll();
        IdDes: ConsoleHelper.WriteWithColor("**** ENTER ID ****", ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("ID is not correct variant", ConsoleColor.DarkCyan);
                goto IdDes;
            }
            var dbGroup = _groupRepository.Get(id);
            if (dbGroup is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group this iD", ConsoleColor.DarkRed);
            }
            else
            {
                _groupRepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("Group succesfuly deleted", ConsoleColor.DarkGreen);
            }
        }
        public void Exit()


        {
        AreYouSureDes: ConsoleHelper.WriteWithColor("Are you sure?  y/n", ConsoleColor.DarkGreen);
            char decision;
            bool isSucceeded = char.TryParse(Console.ReadLine(), out decision);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Your selection is not in the correct format\n*Pleace write y/n", ConsoleColor.DarkRed);
                goto AreYouSureDes;
            }
            if (!(decision == 'y' || decision == 'n'))
            {
                ConsoleHelper.WriteWithColor("Your selection is not correct ", ConsoleColor.DarkRed);
                goto AreYouSureDes;
            }

            if (decision == 'n')
            {

            }


        }
    }
}



