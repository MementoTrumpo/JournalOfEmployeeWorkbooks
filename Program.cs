using System;
using JournalOfEmployeeWorkbooks.Views;
using JournalOfEmployeeWorkbooks.Presenters;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Reflection.Metadata;
using System.Threading;

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
            Program entryMainPoint = new Program();

            entryMainPoint.RunApplication();

            
            Console.WriteLine();

            Console.ReadKey();
        }



        private void CreateFunctionalMenu()
        {
            string indicator = "=>";
            int countSpaces = 5;
            string chooseOperation = operations[0];
            //Console.WriteLine($"{indicator}   {operations[0]}");

            do
            {
                for (int i = 0; i < operations.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.WriteLine($"{indicator}   {operations[i]}");
                    }

                    string operation = operations[i];
                    for (int j = 0; j < countSpaces; j++)
                    {
                        operation = operation.Insert(0, " ");
                    }
                    Console.WriteLine(operation);
                }

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                if (consoleKeyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            while (true);

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

        /// <summary>
        /// Создание интерфейса для взаимодействия с пользователем
        /// </summary>
        public void MakeUserMenu()
        {
            ConsoleKey key;
            int position = 0;
            string separatingLine = ReturnSeparatingLine();

            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
            {
                
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        position--;

                        if (position < 0)
                        {
                            position = 0;
                        }
                        operations[position] = operations[position].Insert(0, "");
                        operations[position] = operations[position].Insert(0, "=>");

                        Console.WriteLine(separatingLine);
                        for (int i = 0; i < operations.Count; i++)
                        {

                            Console.Write(operations[i]);
                            Console.WriteLine(separatingLine);
                        }
                        operations[position] = operations[position].Insert(0, "");

                        break;

                    case ConsoleKey.DownArrow:
                        position++;

                        if (position > operations.Count - 1)
                        {
                            position = operations.Count - 1;
                        }
                        operations[position] = operations[position].Insert(0, "");
                        operations[position] = operations[position].Insert(0, "=>");

                        Console.WriteLine(separatingLine);
                        for (int i = 0; i < operations.Count; i++)
                        {
                            Console.Write(operations[i]);
                            Console.WriteLine(separatingLine);
                        }
                        operations[position] = operations[position].Insert(0, "");

                        break;
                }
            }

        }

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
                            Console.Clear();

                            var phraseCreatingOfEmployee = "\tCreating record in database";
                            Console.WriteLine(phraseCreatingOfEmployee);
                            Console.WriteLine(GetSeparatingLine(phraseCreatingOfEmployee) + "\n");
                            
                            mainView = new MainView();
                            repository = new Repository(ListOfEmployee);
                            Employee employee = new Employee();

                            mainView.InputSecondName(employee, secondNameSuggestion);
                            Console.WriteLine();
                            mainView.InputFirstName(employee, firstNameSugestion);
                            Console.WriteLine();
                            mainView.InputThirdName(employee, thirdNameSuggestion);
                            Console.WriteLine();
                            mainView.InputDateOfBirth(employee, dateOfBirthSuggestion);
                            Console.WriteLine();
                            mainView.InputPlaceOfBirth(employee, placeOfBirthSuggestion);
                            Console.WriteLine();
                            mainView.InputPostOfEmployee(employee, postOfEmployeeSuggestion);
                            Console.WriteLine();
                            Console.WriteLine($"\tID - {employee.ID}");
                            Console.WriteLine();
                            Console.WriteLine($"\tDate of adding record - {employee.DateTimeAddingRegistration.ToShortDateString()}");
                            Console.WriteLine();

                            PrintingSuggestionsAboutCorrectnessOfInput();

                            ConsoleKey keyToTruth;

                            keyToTruth = Console.ReadKey(true).Key;
                            
                            if ((keyToTruth == ConsoleKey.D1) || (keyToTruth == ConsoleKey.NumPad1))
                            {
                                repository.CreateRecord(path: PATH, employee);
                                dataIsCorrectly = true;
                            }
                            else if ((keyToTruth == ConsoleKey.D2) || (keyToTruth == ConsoleKey.NumPad2))
                            {
                                continue;
                            }
                        } while (dataIsCorrectly != true);

                        ReturnToMainMenu();
                                                
                        break;

                    case (ConsoleKey.D2 or ConsoleKey.NumPad2):

                        dataIsCorrectly = false;
                        do
                        {
                            Console.Clear();
                            var phraseDeletingOfEmployee = "\tDeleting an employee from the database";
                            Console.WriteLine(phraseDeletingOfEmployee);
                            Console.WriteLine(GetSeparatingLine(phraseDeletingOfEmployee) + "\n");

                            mainView = new MainView();
                            repository = new Repository(ListOfEmployee);

                            mainView.InputID(idSuggestion);
                            Console.WriteLine();

                            PrintingSuggestionsAboutCorrectnessOfInput();
                            

                            ConsoleKey keyToTruth = Console.ReadKey(true).Key;

                            if(keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                            {
                                var output = mainView.DeleteEmployee(repository,int.Parse(mainView.ID),
                                    phraseDeletingOfEmployee, PATH);
                                if(output == 1)
                                {
                                    dataIsCorrectly = true;
                                }
                                else
                                {
                                    continue;
                                }
                                
                            }

                            else if(keyToTruth == ConsoleKey.D2 || keyToTruth == ConsoleKey.NumPad2)
                            {
                                continue;
                            }
                        } while (dataIsCorrectly != true);
                        
                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;


                    case (ConsoleKey.D3 or ConsoleKey.NumPad3):

                        Console.Clear();

                        var phraseViewingAllRecords = "\t Viewing all records in database";
                        Console.WriteLine(phraseViewingAllRecords);
                        Console.WriteLine(GetSeparatingLine(phraseViewingAllRecords) + "\n");

                        repository = new Repository(ListOfEmployee);
                        List<Employee> listOfEmployees = repository.ViewAllRecords(PATH);

                        string separateLine = "\t---------------------------------------------";
                        foreach (var employee in listOfEmployees)
                        {
                            Console.WriteLine(separateLine + "\n");
                            Console.WriteLine(employee.ToString());
                        }

                        ReturnToMainMenu();
                        break;

                    case (ConsoleKey.D4 or ConsoleKey.NumPad4):
                        dataIsCorrectly = false;

                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        do
                        {
                            Console.Clear();
                            var phraseOfViewingEmployee = "\tViewing one employee\n";
                            Console.Write(phraseOfViewingEmployee);
                            Console.WriteLine(GetSeparatingLine(phraseOfViewingEmployee) + "\n");

                            mainView.InputID(idSuggestion);
                            Console.WriteLine();
                            PrintingSuggestionsAboutCorrectnessOfInput();

                            separateLine = "\t---------------------------------------------";


                            ConsoleKey keyToTruth = Console.ReadKey(true).Key;
                            if((keyToTruth == ConsoleKey.D1) || (keyToTruth == ConsoleKey.NumPad1))
                            {
                                var output = mainView.ViewingOneRecordByID(repository,
                                    int.Parse(mainView.ID), PATH, out Employee employee);
                                if(output == 1)
                                {
                                    Console.WriteLine(separateLine);
                                    Console.Write(employee.ToString());
                                    Console.WriteLine(separateLine + "\n");
                                    dataIsCorrectly = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                            //Возврат в главное меню
                            ReturnToMainMenu();

                        } while (dataIsCorrectly != true);
                        break;
                    ///Загрузка записей в выбранном диапазоне
                    case (ConsoleKey.D5 or ConsoleKey.NumPad5):

                        dataIsCorrectly = false;
                        var phraseLoadingRecordsInDateRange = "\tLoading records" +
                            " in the selected range";
                        repository = new Repository(ListOfEmployee);
                        mainView = new MainView();

                        List<Employee> loadingRecords;

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
                            //Загрузка всех записей, удовлетворяющих начальной и конечной дате
                            loadingRecords = repository.LoadingRecordsInSelectedDateRange(PATH,
                                DateTime.Parse(mainView.StartDate), DateTime.Parse(mainView.EndDate));
                            Console.WriteLine();
                            //Если список оказывается пуст, то выводится сообщение об ошибке
                            if(loadingRecords.Count == 0)
                            {
                                Console.WriteLine("\tUnfortunately," +
                                    " there is not a single record in this range");
                                ConsoleKey keyToContinue;

                                do
                                {
                                    currentPosition = Console.GetCursorPosition();
                                    Console.WriteLine("\tPlease, repeat the input");
                                    keyToContinue = Console.ReadKey(false).Key;
                                    ClearIncorrectInput(currentPosition);

                                } while (keyToContinue != ConsoleKey.Enter);
                                
                                continue;
                            }
                            else
                            {
                                dataIsCorrectly = true;
                            }
                        } while (dataIsCorrectly != true);
                        //Вывод в консоль всех удовлетворяющих записей
                        foreach (var record in loadingRecords)
                        {
                            Console.WriteLine("\t---------------------------------------------\n");
                            Console.WriteLine(record.ToString());
                        }
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
                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth = Console.ReadKey(true).Key;
                            Console.WriteLine();

                            if(keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                            {
                                //Попытка изменить имя сотрудника (1 - если все успешно, 0 - если неудача)
                                int output = mainView.RedactFirstNameOfEmployee(repository,
                                    int.Parse(mainView.ID), PATH, firstNameSugestion);
                                if(output == 1)
                                {
                                    dataIsCorrectly = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }

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
                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth = Console.ReadKey(true).Key;
                            Console.WriteLine();

                            if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                            {
                                //Попытка изменить имя сотрудника (1 - если все успешно, 0 - если неудача)
                                int output = mainView.RedactSecondNameOfEmployee(repository,
                                    int.Parse(mainView.ID), PATH, dateOfBirthSuggestion);
                                if (output == 1)
                                {
                                    dataIsCorrectly = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }

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
                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth = Console.ReadKey(true).Key;
                            Console.WriteLine();

                            if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                            {
                                //Попытка изменить имя сотрудника (1 - если все успешно, 0 - если неудача)
                                int output = mainView.RedactThirdNameOfEmployee(repository,
                                    int.Parse(mainView.ID), PATH, dateOfBirthSuggestion);
                                if (output == 1)
                                {
                                    dataIsCorrectly = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }

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
                            //Ввод символа, который пользователь вводит при корректности ввода ID
                            ConsoleKey keyToTruth = Console.ReadKey(true).Key;
                            Console.WriteLine();

                            if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                            {
                                //Попытка изменить имя сотрудника (1 - если все успешно, 0 - если неудача)
                                int output = mainView.RedactDateOfBirthOfEmployee(repository,
                                    int.Parse(mainView.ID), PATH, dateOfBirthSuggestion);
                                if (output == 1)
                                {
                                    dataIsCorrectly = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }

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
                            ConsoleKey keyToTruth = Console.ReadKey(true).Key;
                            Console.WriteLine();

                            if (keyToTruth == ConsoleKey.D1 || keyToTruth == ConsoleKey.NumPad1)
                            {
                                //Попытка изменить имя сотрудника (1 - если все успешно, 0 - если неудача)
                                int output = mainView.RedactPlaceOfBirthOfEmployee(repository,
                                    int.Parse(mainView.ID), PATH, dateOfBirthSuggestion);
                                if (output == 1)
                                {
                                    dataIsCorrectly = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }

                        } while (dataIsCorrectly != true);
                        //Возврат в главное меню
                        ReturnToMainMenu();
                        break;
                    case (ConsoleKey.Escape):
                        Console.Clear();
                        Console.WriteLine("The user selected escape from the programm");
                        break;

                }
            } while (key != ConsoleKey.Escape);

        }

        /// <summary>
        /// Предложения ввода корректности ввода
        /// </summary>
        private List<string> SuggestionsForCorrectInput = new List<string>()
        {
            "\t---------------------------------------------\n",
            "\tCheck the correctness of the entered data\n",
            "\tEnter 1 - if all the data is entered correctly\n",
            "\tEnter 2 - if the data is entered incorrectly\n",
            "\t---------------------------------------------\n"
        };

        /// <summary>
        /// Вывод предложений о корректности ввода
        /// </summary>
        public void PrintingSuggestionsAboutCorrectnessOfInput()
        {
            foreach(var suggestion in SuggestionsForCorrectInput)
            {
                Console.Write(suggestion);
            }
        }
        
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
            Console.Write(new String(' ', Console.BufferWidth * 15));
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
                currentPosition = Console.GetCursorPosition();

                Console.WriteLine("\tPress enter to exit the main menu...");

                exitKey = Console.ReadKey(true).Key;

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
        internal int FindingMaximumNumberOfCharacters(List<string> operations)
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
