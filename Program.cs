using System;
using JournalOfEmployeeWorkbooks.Views;
using JournalOfEmployeeWorkbooks.Presenters;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Net.NetworkInformation;
using System.Linq;
using System.Runtime.Serialization;

namespace JournalOfEmployeeWorkbooks
{
    class Program 
    {
        /// <summary>
        /// Текущая позиция курсора
        /// </summary>
        private static (int, int) currentPosition { get; set; }

        /// <summary>
        /// Список сотрудников из базы данных
        /// </summary>
        private List<Employee> ListOfEmployee = new List<Employee>();

        /// <summary>
        /// Предложение о вводе имени
        /// </summary>
        private string firstNameSugestion = "\tInput first name of employee - ";

        /// <summary>
        /// Предложение о вводе фамилии
        /// </summary>
        private string secondNameSuggestion = "\tInput second name of employee - ";

        /// <summary>
        /// Предложение о вводе отчества
        /// </summary>
        private string thirdNameSuggestion = "\tInput third name of employee - ";

        /// <summary>
        /// Предложение о вводе даты рождения
        /// </summary>
        private string dateOfBirthSuggestion = "\tInput the employee's date of birth - ";

        /// <summary>
        /// Предложение о вводе места рождения
        /// </summary>
        private string placeOfBirthSuggestion = "\tInput the employee's place of birth - ";

        /// <summary>
        /// Предложение о вводе должности
        /// </summary>
        private string postOfEmployeeSuggestion = "\tInput the position held by the employee - ";

        /// <summary>
        /// Предложение о вводе ID
        /// </summary>
        private string idSuggestion = "\tInput ID of employee - ";

        /// <summary>
        /// Предложение о вводе просмотра сотудника
        /// </summary>
        private string viewSuggestion = "\tViewing one record";

        /// <summary>
        /// Предложение о вводе начальной даты выбора записей
        /// </summary>
        private string startDateSuggestion = "\tInput start date of registration employee's - ";

        /// <summary>
        /// Предложение о вводе конечной даты выбора записей
        /// </summary>
        private string endDateSuggestion = "\tInput end date of regidtration employee's - ";

        /// <summary>
        /// Путь к файлу
        /// </summary>
        private const string PATH = @"D:\source\Проекты SkillBox\JournalOfEmployeeWorkbooks\InformationAboutEmployees.txt";

        static void Main(string[] args)
        {         
            //Точка входа в приложение
            Program entryMainPoint = new Program();
            //Запуск приложения
            entryMainPoint.RunApplication();

            Console.WriteLine();

            Console.ReadKey();
        }
    
        /// <summary>
        /// Выводит информацию о сотрудниках в консоль
        /// </summary>
        /// <param name="listOfEmployees">Список сотрудников в базе данных</param>
        private static void PrintingEmployeesToConsole(List<Employee> listOfEmployees)
        {
            var separatingLine = "\t---------------------------------------------";

            foreach(var employee in listOfEmployees)
            {
                Console.WriteLine(separatingLine + "\n");
                Console.WriteLine(employee.ToString());
            }
        }

        /// <summary>
        /// Операции выбора в пользовательском меню
        /// </summary>
        private List<string> operations = new List<string>()
        {
            "\t1 - Add a new employee to the database\n",
            "\t2 - Delete an employee in the database\n",
            "\t3 - View all records\n",
            "\t4 - Viewing a record of a specific employee\n",
            "\t5 - Viewing records in the selected date range\n",
            "\t6 - Editing the employee's name\n",
            "\t7 - Editing the employee's last name\n",
            "\t8 - Editing an employee's patronymic\n",
            "\t9 - Editing the employee's date of birth\n",
            "\t0 - Editing the employee's place of birth\n",
            "\tEscape - Close programm\n"
        };

        //Будущее меню с более красивым выбором нужного действия
        /// <summary>
        /// Создание интерфейса для взаимодействия с пользователем
        /// </summary>
        //public void MakeUserMenu()
        //{
        //    ConsoleKey key;
        //    int position = 0;
        //    string separatingLine = ReturnSeparatingLine();

        //    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
        //    {
                
        //        switch (key)
        //        {
        //            case ConsoleKey.UpArrow:
        //                position--;

        //                if (position < 0)
        //                {
        //                    position = 0;
        //                }
        //                operations[position] = operations[position].Insert(0, "");
        //                operations[position] = operations[position].Insert(0, "=>");

        //                Console.WriteLine(separatingLine);
        //                for (int i = 0; i < operations.Count; i++)
        //                {

        //                    Console.Write(operations[i]);
        //                    Console.WriteLine(separatingLine);
        //                }
        //                operations[position] = operations[position].Insert(0, "");

        //                break;

        //            case ConsoleKey.DownArrow:
        //                position++;

        //                if (position > operations.Count - 1)
        //                {
        //                    position = operations.Count - 1;
        //                }
        //                operations[position] = operations[position].Insert(0, "");
        //                operations[position] = operations[position].Insert(0, "=>");

        //                Console.WriteLine(separatingLine);
        //                for (int i = 0; i < operations.Count; i++)
        //                {
        //                    Console.Write(operations[i]);
        //                    Console.WriteLine(separatingLine);
        //                }
        //                operations[position] = operations[position].Insert(0, "");

        //                break;
        //        }
        //    }

        //}

        /// <summary>
        /// Создает меню для информирования пользователя о возможных действиях
        /// </summary>
        private void CreateMenuForUser()
        {
            string separatingLine = ReturnSeparatingLine();

            Console.WriteLine(separatingLine);
            for (int i = 0; i < operations.Count; i++)
            {
                Console.Write(operations[i]);
                Console.WriteLine(separatingLine);
            }
        }


        
        /// <summary>
        /// Запуск и работа приложения
        /// </summary>
        public void RunApplication()
        {
            ConsoleKey key;

            do
            {
                CreateMenuForUser();

                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case (ConsoleKey.D1 or ConsoleKey.NumPad1):
                        bool dataIsCorrectly = false;
                        Repository repository;
                        MainView mainView;

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            var phraseCreatingOfEmployee = "\tCreating record in database";
                            Console.WriteLine(phraseCreatingOfEmployee);
                            Console.WriteLine(GetSeparatingLine(phraseCreatingOfEmployee) + "\n");
                            
                            mainView = new MainView();
                            repository = new Repository(ListOfEmployee);
                            Employee employee = new Employee();

                            //Ввод фамилии сотрудника
                            mainView.InputSecondName(employee, secondNameSuggestion);
                            Console.WriteLine();

                            //Ввод имени сотрудника
                            mainView.InputFirstName(employee, firstNameSugestion);
                            Console.WriteLine();

                            //Ввод отчества сотрудника
                            mainView.InputThirdName(employee, thirdNameSuggestion);
                            Console.WriteLine();

                            //Ввод даты рождения сотрудника
                            mainView.InputDateOfBirth(employee, dateOfBirthSuggestion);
                            Console.WriteLine();

                            //Ввод мета рождения сотрудника
                            mainView.InputPlaceOfBirth(employee, placeOfBirthSuggestion);
                            Console.WriteLine();

                            //Ввод должности сотрудника
                            mainView.InputPostOfEmployee(employee, postOfEmployeeSuggestion);
                            Console.WriteLine();

                            //Вывод ID сотрудника
                            Console.WriteLine($"\tID - {employee.ID}");
                            Console.WriteLine();

                            //Вывод даты добавления регистрации записи сотрудника
                            Console.WriteLine($"\tDate of adding record - {employee.DateTimeAddingRegistration.ToShortDateString()}");
                            Console.WriteLine();

                            //Сообщение пользователю о корректности ввода
                            PrintingSuggestionsAboutCorrectnessOfInput();

                            //Ввод символа, который пользователь вводит при корректности ввода
                            ConsoleKey keyToTruth;

                            do
                            {
                                keyToTruth = Console.ReadKey(true).Key;

                                if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    repository.CreateRecord(PATH, employee);
                                    dataIsCorrectly = true;
                                    break;

                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    if (ContinueInput() == true)
                                    {
                                        //Повтор ввода ID
                                        break;
                                    }
                                    else
                                    {
                                        //Выход в главное меню
                                        dataIsCorrectly = true;
                                        break;
                                    }
                                }
                                //Нажата не та клавиша, продолжаем считывание
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);

                        //Возврат в главное меню
                        ReturnToMainMenu();
                                                
                        break;

                    ///Удаление сотрудника из базы данных
                    case (ConsoleKey.D2 or ConsoleKey.NumPad2):

                        dataIsCorrectly = false;

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            var phraseDeletingOfEmployee = "\tDeleting an employee from the database";
                            Console.WriteLine(phraseDeletingOfEmployee);
                            Console.WriteLine(GetSeparatingLine(phraseDeletingOfEmployee) + "\n");

                            mainView = new MainView();
                            repository = new Repository(ListOfEmployee);

                            //Ввод ID сотрудника
                            mainView.InputID(idSuggestion);
                            Console.WriteLine();

                            //Сообщение пользователю о корректности ввода
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            Console.WriteLine();

                            //Ввод символа, который пользователь вводит при корректности ввода
                            ConsoleKey keyToTruth;

                            do
                            {
                                keyToTruth = Console.ReadKey(true).Key;

                                if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    //Вызов самой функции для редактирования имени
                                    int output = mainView.DeleteEmployee(repository,
                                        int.Parse(mainView.ID), phraseDeletingOfEmployee, PATH);

                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша, продолжаем считывание
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);
                        
                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;

                    ///Просмотр всех записей сотрудников из базы данных
                    case (ConsoleKey.D3 or ConsoleKey.NumPad3):
                        //Очистка консоли и информирование пользователя о совершаемом им действии
                        Console.Clear();
                        var phraseViewingAllRecords = "\t Viewing all records in database";
                        Console.WriteLine(phraseViewingAllRecords);
                        Console.WriteLine(GetSeparatingLine(phraseViewingAllRecords) + "\n");

                        repository = new Repository(ListOfEmployee);
                        List<Employee> listOfEmployees = repository.ViewAllRecords(PATH);
                        string separateLine;

                        if(listOfEmployees.Count == 0)
                        {
                            Console.WriteLine("\tUnfortunately, the list of employees is empty\n");
                            ReturnToMainMenu();
                            break;
                        }

                        var sortingText = "\tSort the list of employees by:\n";
                        Console.WriteLine(GetSeparatingLine(sortingText) + "\n");
                        Console.WriteLine(sortingText);
                        Console.WriteLine(GetSeparatingLine(sortingText) + "\n");
                        Console.WriteLine("\t1 - Surname\n");
                        Console.WriteLine("\t2 - Name\n");
                        Console.WriteLine("\t3 - Patronymic\n");
                        Console.WriteLine("\t4 - ID\n");
                        Console.WriteLine(GetSeparatingLine(sortingText) + "\n");

                        ConsoleKey selectionKey;
                        do
                        {
                            selectionKey = Console.ReadKey(true).Key;

                            if (selectionKey == ConsoleKey.D1 || selectionKey == ConsoleKey.NumPad1)
                            {
                                var listOrderedBySurname = listOfEmployees.OrderBy(x => x.SecondName);
                                PrintingEmployeesToConsole(listOrderedBySurname.ToList());
                                break;
                            }

                            else if(selectionKey == ConsoleKey.D2 || selectionKey == ConsoleKey.NumPad2)
                            {
                                var listOrderedBySurname = listOfEmployees.OrderBy(x => x.FirstName);
                                PrintingEmployeesToConsole(listOrderedBySurname.ToList());
                                break;
                            }

                            else if (selectionKey == ConsoleKey.D3 || selectionKey == ConsoleKey.NumPad3)
                            {
                                var listOrderedBySurname = listOfEmployees.OrderBy(x => x.ThirdName);
                                PrintingEmployeesToConsole(listOrderedBySurname.ToList());
                                break;
                            }
                            else if (selectionKey == ConsoleKey.D4 || selectionKey == ConsoleKey.NumPad4)
                            {
                                var listOrderedBySurname = listOfEmployees.OrderBy(x => x.ID);
                                PrintingEmployeesToConsole(listOrderedBySurname.ToList());
                                break;
                            }
                            else
                            {
                                continue;
                            }

                        } while (selectionKey != ConsoleKey.D1 || selectionKey != ConsoleKey.NumPad1 ||
                                 selectionKey != ConsoleKey.D2 || selectionKey != ConsoleKey.NumPad2 ||
                                 selectionKey != ConsoleKey.D3 || selectionKey != ConsoleKey.NumPad3 ||
                                 selectionKey != ConsoleKey.D4 || selectionKey != ConsoleKey.NumPad4 );

                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;

                    case (ConsoleKey.D4 or ConsoleKey.NumPad4):
                        dataIsCorrectly = false;

                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            var phraseOfViewingEmployee = "\tViewing one employee\n";
                            Console.Write(phraseOfViewingEmployee);
                            Console.WriteLine(GetSeparatingLine(phraseOfViewingEmployee) + "\n");

                            //Ввод ID сотрудника
                            mainView.InputID(idSuggestion);
                            Console.WriteLine();

                            //Опрос пользователя о корректности ввода
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            Console.WriteLine();

                            separateLine = "\t---------------------------------------------";

                            ConsoleKey keyToTruth;
                            //Проверка корректности ввода и вызов главной функции
                            do
                            {
                                //Считываем клавишу для выбора корректности ввода данных
                                keyToTruth = Console.ReadKey(true).Key;
                                //Если нажимаем 1 - то идет вызов главной функции
                                //Если нажимаем 2 - то идет возврат к началу ввода ID
                                if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    int output = mainView.ViewingOneRecordByID(repository,
                                                 int.Parse(mainView.ID), PATH, out Employee employee);

                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        Console.WriteLine(separateLine + "\n");
                                        Console.Write(employee.ToString());
                                        Console.WriteLine(separateLine);
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);

                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;

                    ///Загрузка записей в выбранном диапазоне
                    case (ConsoleKey.D5 or ConsoleKey.NumPad5):

                        dataIsCorrectly = false;
                        var phraseLoadingRecordsInDateRange = "\tLoading records" +
                            " in the selected range";

                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        List<Employee> loadingRecords = new List<Employee>();

                        do
                        {
                            //Очистка всего содержимого и информирование пользователя о текущем действии
                            Console.Clear();
                            Console.WriteLine(phraseLoadingRecordsInDateRange);
                            Console.WriteLine(GetSeparatingLine(phraseLoadingRecordsInDateRange) + "\n");
                            //Ввод начальной даты, с которой производится поиск записей
                            mainView.InputStartDate(startDateSuggestion);
                            Console.WriteLine();
                            //Ввод конечной даты, с которой производится поиск записей
                            mainView.InputEndDate(endDateSuggestion);
                            Console.WriteLine();
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            Console.WriteLine();
                            ConsoleKey keyToTruth;
                            do
                            {
                                keyToTruth = Console.ReadKey(true).Key;

                                if(keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    int output = mainView.LoadingRecordsInSelectedDateRange(
                                        repository, PATH, DateTime.Parse(mainView.StartDate),
                                        DateTime.Parse(mainView.EndDate), out loadingRecords);

                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        PrintingEmployeesToConsole(loadingRecords);
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша, продолжаем считывание
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);


                        } while (dataIsCorrectly != true);
 
                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;

                    ///Редактирование имени сотрудника
                    case (ConsoleKey.D6 or ConsoleKey.NumPad6):

                        dataIsCorrectly = false;
                        //Создание репозитория сотрудников и главного вида взаимодействия
                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        var phraseOfRedactFirstName = "\tChanging the employee's name";

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            Console.WriteLine(phraseOfRedactFirstName);
                            Console.WriteLine(GetSeparatingLine(phraseOfRedactFirstName) + "\n");
                            //Ввод ID сотрудника корректного типа
                            mainView.InputID(idSuggestion);
                            Console.WriteLine();
                            //Печать предложений о правильности ввода ID сотрудника
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            Console.WriteLine();

                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth;

                            do
                            {
                                keyToTruth = Console.ReadKey(true).Key;

                                if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    //Вызов самой функции для редактирования имени
                                    int output = mainView.RedactFirstNameOfEmployee(repository,
                                        int.Parse(mainView.ID), PATH, firstNameSugestion);

                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша, продолжаем считывание
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);
                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;
                    
                    ///Редактирование фамилии сотрудника
                    case (ConsoleKey.D7 or ConsoleKey.NumPad7):

                        dataIsCorrectly = false;
                        //Создание репозитория сотрудников и главного вида взаимодействия
                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        var phraseOfRedactSecondName = "\tChanging the employee's last name";

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            Console.WriteLine(phraseOfRedactSecondName);
                            Console.WriteLine(GetSeparatingLine(phraseOfRedactSecondName) + "\n");
                            //Ввод ID сотрудника корректного типа
                            mainView.InputID(idSuggestion);
                            Console.WriteLine();
                            //Печать предложений о правильности ввода ID сотрудника
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            Console.WriteLine();

                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth;

                            do
                            {
                                keyToTruth = Console.ReadKey(true).Key;

                                if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    //Вызов самой функции для редактирования фамилии
                                    int output = mainView.RedactSecondNameOfEmployee(repository,
                                        int.Parse(mainView.ID), PATH, secondNameSuggestion);

                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша, продолжаем считывание
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);

                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;

                    ///Редактирование отчества сотрудника
                    case (ConsoleKey.D8 or ConsoleKey.NumPad8):

                        dataIsCorrectly = false;
                        //Создание репозитория сотрудников и главного вида взаимодействия
                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        var phraseOfRedactThirdName = "\tChanging an employee's patronymic";

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            Console.WriteLine(phraseOfRedactThirdName);
                            Console.WriteLine(GetSeparatingLine(phraseOfRedactThirdName) + "\n");
                            //Ввод ID сотрудника корректного типа
                            mainView.InputID(idSuggestion);
                            Console.WriteLine();
                            //Печать предложений о правильности ввода ID сотрудника
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            Console.WriteLine();

                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth;

                            do
                            {
                                keyToTruth = Console.ReadKey(true).Key;

                                if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    //Вызов самой функции для редактирования отчества
                                    int output = mainView.RedactThirdNameOfEmployee(repository,
                                        int.Parse(mainView.ID), PATH, thirdNameSuggestion);

                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша, продолжаем считывание
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);

                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;

                    ///Редактирование даты рождения сотрудника
                    case (ConsoleKey.D9 or ConsoleKey.NumPad9):

                        dataIsCorrectly = false;
                        //Создание репозитория сотрудников и главного вида взаимодействия
                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        var phraseOfRedactDateOfBirth = "\tChanging the employee's date of birth";

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            Console.WriteLine(phraseOfRedactDateOfBirth);
                            Console.WriteLine(GetSeparatingLine(phraseOfRedactDateOfBirth) + "\n");
                            //Ввод ID сотрудника корректного типа
                            mainView.InputID(idSuggestion);
                            Console.WriteLine();
                            //Печать предложений о правильности ввода ID сотрудника
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            Console.WriteLine();

                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth;

                            do
                            {
                                keyToTruth = Console.ReadKey(true).Key;

                                if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    //Вызов самой функции для редактирования даты рождения сотрудника
                                    int output = mainView.RedactDateOfBirthOfEmployee(repository,
                                        int.Parse(mainView.ID), PATH, dateOfBirthSuggestion);

                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 ||
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша, продолжаем считывание
                                else
                                {
                                    continue;
                                }

                            } while (keyToTruth != ConsoleKey.D1 ||
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);

                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;

                    ///Редактирование места рождения сотрудника
                    case (ConsoleKey.D0 or ConsoleKey.NumPad0):

                        dataIsCorrectly = false;
                        //Создание репозитория сотрудников и главного вида взаимодействия
                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        var phraseOfRedactPlaceOfBirth = "\tChanging the employee's place of birth";

                        do
                        {
                            //Очистка консоли и информирование пользователя о совершаемом им действии
                            Console.Clear();
                            Console.WriteLine(phraseOfRedactPlaceOfBirth);
                            Console.WriteLine(GetSeparatingLine(phraseOfRedactPlaceOfBirth) + "\n");
                            //Ввод ID сотрудника корректного типа
                            mainView.InputID(idSuggestion);
                            Console.WriteLine();
                            //Печать предложений о правильности ввода ID сотрудника
                            PrintingSuggestionsAboutCorrectnessOfInput();
                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth;
                            Console.WriteLine();
                            
                            //Проверка корректности ввода и вызов главной функции
                            do
                            {
                                //Считываем клавишу для выбора корректности ввода данных
                                keyToTruth = Console.ReadKey(true).Key;
                                //Если нажимаем 1 - то идет вызов главной функции
                                //Если нажимаем 2 - то идет возврат к началу ввода ID
                                if(keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                                {
                                    int output = mainView.RedactPlaceOfBirthOfEmployee(repository,
                                    int.Parse(mainView.ID), PATH, placeOfBirthSuggestion);
                                    
                                    //Если функция завершилась корректно, то выходим из 2х циклов
                                    if (output == 1)
                                    {
                                        dataIsCorrectly = true;
                                        break;
                                    }

                                    //Иначе вызываем функцию выхода в главное меню
                                    else
                                    {
                                        if (ContinueInput() == true)
                                        {
                                            //Повтор ввода ID
                                            break;
                                        }
                                        else
                                        {
                                            //Выход в главное меню
                                            dataIsCorrectly = true;
                                            break;

                                        }
                                    }
                                }
                                //Повторный ввод ID сотрудника
                                else if (keyToTruth == ConsoleKey.D2 || 
                                         keyToTruth == ConsoleKey.NumPad2)
                                {
                                    break;
                                }
                                //Нажата не та клавиша
                                else
                                {
                                    continue;
                                }
                                
                            } while (keyToTruth != ConsoleKey.D1 || 
                                     keyToTruth != ConsoleKey.NumPad1 ||
                                     keyToTruth != ConsoleKey.D2 ||
                                     keyToTruth != ConsoleKey.NumPad2);

                        } while (dataIsCorrectly != true);

                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;
                }
            } while (key != ConsoleKey.Escape);

        }

        private static void OffersToGoToMainMenu()
        {


        }

        /// <summary>
        /// Предлагает пользователю продолжить ввод заново либо выйти в главное меню
        /// </summary>
        /// <returns>true - если ввод продолжится, false - выход в главное меню</returns>
        private static bool ContinueInput()
        {
            //Текст, выводимый пользователю
            var textOfContinueInput = "Do you want to continue typing or exit to the main menu?";

            //Вывод в консоль информации о возможности дальнейших событий
            Console.WriteLine($"{GetSeparatingLine(textOfContinueInput)}");
            Console.WriteLine($"\t{textOfContinueInput}");
            Console.WriteLine($"{GetSeparatingLine(textOfContinueInput)}\n");
            Console.WriteLine("\tEnter 1 - if you want to continue typing\n");
            Console.WriteLine("\tEnter 2 - if you want to go to the main menu\n");
            Console.WriteLine($"{GetSeparatingLine(textOfContinueInput)}\n");

            bool output = false;

            ConsoleKey keyToContinue;

            do
            {
                //Считываение клавиши
                keyToContinue = Console.ReadKey(true).Key;

                //Если нажата 1 - ввод данных продолжится
                if (keyToContinue == ConsoleKey.D1 || keyToContinue == ConsoleKey.NumPad1)
                {
                    output = true;
                    break;
                }

                //Если нажата 2 - ввод данных прекращается
                else if (keyToContinue == ConsoleKey.D2 || keyToContinue == ConsoleKey.NumPad2)
                {
                    output = false;
                    break;
                }

                //Если пользоваетль нажимает что-то другое, то продолжается считываение клавиш
                else
                {
                    continue;
                }
            } while ((keyToContinue != ConsoleKey.D1 || keyToContinue != ConsoleKey.NumPad2) ||
                     (keyToContinue != ConsoleKey.D2 || keyToContinue != ConsoleKey.NumPad2));

            //Возврат результата пользователя
            return output;
        }

        /// <summary>
        /// Предложения ввода корректности ввода
        /// </summary>
        private List<string> SuggestionsForCorrectInput = new List<string>()
        {
            "Check the correctness of the entered data:",
            "Enter 1 - if all the data is entered correctly",
            "Enter 2 - if the data is entered incorrectly"
        };

        /// <summary>
        /// Вывод предложений о корректности ввода
        /// </summary>
        public void PrintingSuggestionsAboutCorrectnessOfInput()
        {
            int maxCharacter = 0;
            int maximum = SuggestionsForCorrectInput.Max(x => x.Length);

            for (int i = 0; i < SuggestionsForCorrectInput.Count; i++)
            {
                if (SuggestionsForCorrectInput[i].Length > maxCharacter)
                    maxCharacter = SuggestionsForCorrectInput[i].Length;
            }

            Console.WriteLine($"{GetSeparatingLine(SuggestionsForCorrectInput[1])}");
            Console.WriteLine($"\t{SuggestionsForCorrectInput[0]}");
            Console.WriteLine($"{GetSeparatingLine(SuggestionsForCorrectInput[1])}\n");
            Console.WriteLine($"\t{SuggestionsForCorrectInput[1]}\n");
            Console.WriteLine($"\t{SuggestionsForCorrectInput[2]}\n");
            Console.WriteLine($"{GetSeparatingLine(SuggestionsForCorrectInput[1])}\n");

        }

        /// <summary>
        /// Возващает строку разделителей (нижнего подчеркивания) с учетом длины строки
        /// </summary>
        /// <param name="line">Строка, которую необходимо отделить от остального текста</param>
        /// <returns>Возвращает строку разделителей</returns>
        private static string GetSeparatingLine(string line)
        {
            string separatingString = "\t";
            if(line.Length == 0)
            {
                return "";
            }    
            for(int i = 0; i < line.Length; i++)
            {
                separatingString += "-";
            }
            return separatingString;
        }

        /// <summary>
        /// Очищает неправильно введенные данные и устанавливает курсор в исходную позицию
        /// </summary>
        /// <param name="currentPosition">Текущая позиция курсора</param>
        private static void ClearIncorrectInput((int, int) currentPosition)
        {
            //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
            Console.SetCursorPosition(currentPosition.Item1, currentPosition.Item2);
            //Печать строки, состоящей из 15 пустых строк после начального положения курсора
            Console.Write(new String(' ', Console.BufferWidth * 15));
            //Вновь установка нового положения курсора
            Console.SetCursorPosition(currentPosition.Item1, currentPosition.Item2);
        }

        /// <summary>
        /// Возврат в гланое меню
        /// </summary>
        private static void ReturnToMainMenu()
        {
            ConsoleKey exitKey;

            do
            {
                //Установка текущего положения курсора
                currentPosition = Console.GetCursorPosition();

                Console.WriteLine("\tPress enter to exit the main menu...");
                //Ввод клавиши enter для выхода из приложения
                exitKey = Console.ReadKey(true).Key;
                //Очистка некорректного ввода
                ClearIncorrectInput(currentPosition);

            } while (exitKey != ConsoleKey.Enter);

            Console.Clear();
        }

        /// <summary>
        /// Добавляет в меню разделяющюю строку
        /// </summary>
        /// <returns></returns>
        private string ReturnSeparatingLine()
        {
            string line = "\t";

            for (int i = 0; i <= FindingMaximumNumberOfCharacters(operations); i++)
            {
                line += "-";
            }

            return line;
        }

        /// <summary>
        /// Находит максимальное количество символов в предложении
        /// </summary>
        /// <param name="operations">Список команд, использующихся в меню</param>
        /// <returns>Количество символов</returns>
        private int FindingMaximumNumberOfCharacters(List<string> operations)
        {
            int maxCharacters = 0;

            maxCharacters = operations[0].Length;
            for (int i = 1; i < operations.Count; i++)
            {
                if (maxCharacters < operations[i].Length)
                {
                    maxCharacters = operations[i].Length;
                }
            }

            return maxCharacters;
        }







    }
}
