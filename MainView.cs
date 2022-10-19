using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JournalOfEmployeeWorkbooks.Views;
using JournalOfEmployeeWorkbooks.Presenters;
using System.Reflection;
using System.IO;

namespace JournalOfEmployeeWorkbooks
{
    public class MainView
    {
        /// <summary>
        /// Позиция курсора в текущий момент времени
        /// </summary>
        private static (int,int) currentPosition { get; set; }
        /// <summary>
        /// Сообщение об ошибке при вводе неправильного ID сотрудника
        /// </summary>
        public string ErrorMessage { get; set; }

        private int id;

        /// <summary>
        /// ID сотрудника по которому будет производиться поиск в базе данных
        /// </summary>
        public string ID
        {
            get { return id.ToString(); }

            set
            {
                if(int.TryParse(value, out id) == false)
                {
                    ErrorMessage = "The employee ID has an incorrect format!";
                    throw new ApplicationException(ErrorMessage);
                }
            }
        }

        private DateTime startDate;

        public string StartDate
        {
            get { return startDate.ToShortDateString(); }

            set
            {
                if(DateTime.TryParse(value, out startDate) == false)
                {
                    ErrorMessage = "The start date has an incorrect format!";
                    throw new ApplicationException(ErrorMessage);
                }
            }
        }

        private DateTime endDate;

        public string EndDate
        {
            get { return endDate.ToShortDateString(); }

            set
            {
                if (DateTime.TryParse(value, out endDate) == false)
                {
                    ErrorMessage = "The end date has an incorrect format!";
                    throw new ApplicationException(ErrorMessage);
                }
            }
        }

        #region Ввод имени сотрудника
        public void InputFirstName(Employee employee, string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputFirstName(employee, suggestion, out errorMessage))
            {
                if (errorMessage != employee.ErrorMessage)
                {
                    employee.ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    ClearIncorrectInput(currentPosition);
                }
            }
        }
        private static bool TryInputFirstName(Employee employee, string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();
            Console.Write(suggestion);

            try
            {
                employee.FirstName = Console.ReadLine();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion

        #region Ввод фамилиии сотрудника
        public void InputSecondName(Employee employee, string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputSecondName(employee, suggestion, out errorMessage))
            {
                if (errorMessage != employee.ErrorMessage)
                {
                    employee.ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    ClearIncorrectInput(currentPosition);
                }

            }
        }

        private static bool TryInputSecondName(Employee employee, string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();
            Console.Write(suggestion);

            try
            {
                employee.SecondName = Console.ReadLine();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion


        #region Ввод отчества сотрудника
        public void InputThirdName(Employee employee, string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputThirdName(employee, suggestion, out errorMessage))
            {
                if (errorMessage != employee.ErrorMessage)
                {
                    employee.ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    ClearIncorrectInput(currentPosition);
                }
            }

        }

        private static bool TryInputThirdName(Employee employee, string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();
            Console.Write(suggestion);

            try
            {
                employee.ThirdName = Console.ReadLine();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion

        #region Ввод даты рождения сотрудника
        public void InputDateOfBirth(Employee employee, string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputDateOfBirth(employee, suggestion, out errorMessage))
            {
                if (errorMessage != employee.ErrorMessage)
                {
                    employee.ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
                    ClearIncorrectInput(currentPosition);
                }
            }

        }

        private static bool TryInputDateOfBirth(Employee employee, string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;
            //Текущая позиция курсора
            currentPosition = Console.GetCursorPosition();
            //Вывод в консоль предложения пользователю ввести дату рождения сотрудника
            Console.Write(suggestion);

            try
            {
                employee.DateOfBirth = Console.ReadLine();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }


        }
        #endregion

        #region Ввод места рождения сотрудника
        public void InputPlaceOfBirth(Employee employee, string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputPlaceOfBirth(employee, suggestion, out errorMessage))
            {
                if (errorMessage != employee.ErrorMessage)
                {
                    employee.ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
                    ClearIncorrectInput(currentPosition);
                }
            }

        }

        private static bool TryInputPlaceOfBirth(Employee employee, string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;
            //Текущая позиция курсора
            currentPosition = Console.GetCursorPosition();
            //Вывод в консоль предложения пользователю ввести дату рождения сотрудника
            Console.Write(suggestion);

            try
            {
                employee.PlaceOfBirth = Console.ReadLine();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }


        }
        #endregion


        #region Ввод поста, занимаегомого сотрудником
        public void InputPostOfEmployee(Employee employee, string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputPostOfEmployee(employee, suggestion, out errorMessage))
            {
                if (errorMessage != employee.ErrorMessage)
                {
                    employee.ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
                    ClearIncorrectInput(currentPosition);
                }
            }
        }

        private static bool TryInputPostOfEmployee(Employee employee, string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;
            //Текущая позиция курсора
            currentPosition = Console.GetCursorPosition();
            //Вывод в консоль предложения пользователю ввести дату рождения сотрудника
            Console.Write(suggestion);

            try
            {
                employee.PostOfEmployee = Console.ReadLine();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion


        #region Ввод начальной даты с которой будет производиться поиск записей
        public void InputStartDate(string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputStartDate(suggestion, out errorMessage))
            {
                if (errorMessage != ErrorMessage)
                {
                    ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
                    ClearIncorrectInput(currentPosition);
                }
            }
        }

        private bool TryInputStartDate(string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;
            //Текущая позиция курсора
            currentPosition = Console.GetCursorPosition();
            //Вывод в консоль предложения пользователю ввести дату рождения сотрудника
            Console.Write(suggestion);

            try
            {
                StartDate = Console.ReadLine();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion

        #region Ввод конечной даты с которой будет производиться поиск записей
        public void InputEndDate(string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputEndDate(suggestion, out errorMessage))
            {
                if (errorMessage != ErrorMessage)
                {
                    ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
                    ClearIncorrectInput(currentPosition);
                }
            }
        }

        private bool TryInputEndDate(string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;
            //Текущая позиция курсора
            currentPosition = Console.GetCursorPosition();
            //Вывод в консоль предложения пользователю ввести дату рождения сотрудника
            Console.Write(suggestion);

            try
            {
                EndDate = Console.ReadLine();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion


        #region Ввод ID сотрудника 
        public void InputID(string suggestion)
        {
            string errorMessage = string.Empty;

            while (!TryInputID(suggestion, out errorMessage))
            {
                if (errorMessage != ErrorMessage)
                {
                    ErrorMessage = string.Empty;
                    break;
                }
                else
                {
                    //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
                    ClearIncorrectInput(currentPosition);
                }
            }
        }

        ///Попытка ввода ID сотрудника
        private bool TryInputID(string suggestion, out string errorMessage)
        {
            errorMessage = string.Empty;
            //Текущая позиция курсора
            currentPosition = Console.GetCursorPosition();
            //Вывод в консоль предложения пользователю ввести дату рождения сотрудника
            Console.Write(suggestion);

            try
            {
                ID = Console.ReadLine();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion

        #region Удаление сотрудника из базы данных

        /// <summary>
        /// Удаление сотрудника из базы данных с учетомм неправильного ввода ID
        /// </summary>
        /// <param name="repository">Репозиторий с базой данных</param>
        /// <param name="ID">ID сотрудника </param>
        /// <param name="path">Путь к файлу</param>
        public int DeleteEmployee(Repository repository, int ID, string suggestion, string path)
        {
            string errorMessage = string.Empty;

            int correctOutput = 0;

            if(TryDeleteEmployee(repository, ID, path, suggestion, out errorMessage) == true)
            {
                correctOutput = 1;
            }
            else
            {
                correctOutput = 0;
            }
            return correctOutput;
        }


        private bool TryDeleteEmployee(Repository repository,int ID, string path, string suggestion,
            out string errorMessage)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();

            Program program = new Program();

            try
            {
                repository.DeleteRecord(ID, path);

                return true;
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion


        #region Просмотр информации об одном сотруднике по его ID
        public int ViewingOneRecordByID(Repository repository, int ID,
            string path, out Employee employee)
        {
            string errorMessage = string.Empty;

            int correctOutput;

            if (TryViewingOneRecordByID(repository, ID, path, out errorMessage, out employee) == true)
            {
                correctOutput = 1;
            }
            else
            {
                correctOutput = 0;
            }
            return correctOutput;
        }


        private bool TryViewingOneRecordByID(Repository repository, int ID, string path,
            out string errorMessage, out Employee employee)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();

            try
            {
                employee = repository.ViewingOneRecord(ID, path);

                return true;
            }
            catch (Exception ex)
            {
                employee = null;
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion


        #region Редактирование имени сотрудника и запись в базу данных обновленное имя
        public int RedactFirstNameOfEmployee(Repository repository, int ID, string path,
                                             string firstNameSuggestion)
        {
            string errorMessage = string.Empty;

            int correctOutput = 0;

            if (TryRedactFirstNameOfEmployee(repository, ID, firstNameSuggestion, path,
                out errorMessage, out Employee employee) == true)
            {
                correctOutput = 1;
                return correctOutput;
            }
            else
            {
                return correctOutput;
            }
        }


        private bool TryRedactFirstNameOfEmployee(Repository repository, int ID,
            string firstNameSuggestion, string path, out string errorMessage, out Employee employee)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();

            try
            {
                employee = new Employee();

                InputFirstName(employee, firstNameSuggestion);

                repository.RedactFirstNameInRecord(ID, path, employee.FirstName);

                return true;
            }
            catch (Exception ex)
            {
                employee = null;
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion


        #region Изменение фамилии сотрудника и запись в базу данных обновленную фамилию сотрудника
        public int RedactSecondNameOfEmployee(Repository repository, int ID, string path,
                                     string secondNameSuggestion)
        {
            string errorMessage = string.Empty;

            int correctOutput = 0;

            if (TryRedactSecondNameOfEmployee(repository, ID, secondNameSuggestion, path,
                out errorMessage, out Employee employee) == true)
            {
                correctOutput = 1;
                return correctOutput;
            }
            else
            {
                return correctOutput;
            }
        }


        private bool TryRedactSecondNameOfEmployee(Repository repository, int ID,
            string secondNameSuggestion, string path, out string errorMessage, out Employee employee)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();

            try
            {
                employee = new Employee();

                InputSecondName(employee, secondNameSuggestion);

                repository.RedactSecondNameInRecord(ID, path, employee.SecondName);

                return true;
            }
            catch (Exception ex)
            {
                employee = null;
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion

        #region Изменение отчества сотрудника и запись в базу данных обновленное отчество сотрудника
        public int RedactThirdNameOfEmployee(Repository repository, int ID, string path,
                             string thirdNameSuggestion)
        {
            string errorMessage = string.Empty;

            int correctOutput = 0;

            if (TryRedactThirdNameOfEmployee(repository, ID, thirdNameSuggestion, path,
                out errorMessage, out Employee employee) == true)
            {
                correctOutput = 1;
                return correctOutput;
            }
            else
            {
                return correctOutput;
            }
        }


        private bool TryRedactThirdNameOfEmployee(Repository repository, int ID,
            string thirdNameSuggestion, string path, out string errorMessage, out Employee employee)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();

            try
            {
                employee = new Employee();

                InputThirdName(employee, thirdNameSuggestion);

                repository.RedactThirdNameInRecord(ID, path, employee.ThirdName);

                return true;
            }
            catch (Exception ex)
            {
                employee = null;
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion

        #region Изменение даты рождения сотрудника и запись в базу данных обновленную дату рождения сотрудника
        public int RedactDateOfBirthOfEmployee(Repository repository, int ID, string path,
                     string dateOfBirthSuggestion)
        {
            string errorMessage = string.Empty;

            int correctOutput = 0;

            if (TryRedactDateOfBirthOfEmployee(repository, ID, dateOfBirthSuggestion, path,
                out errorMessage, out Employee employee) == true)
            {
                correctOutput = 1;
                return correctOutput;
            }
            else
            {
                return correctOutput;
            }
        }


        private bool TryRedactDateOfBirthOfEmployee(Repository repository, int ID,
            string dateOfBirthSuggestion, string path, out string errorMessage, out Employee employee)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();

            try
            {
                employee = new Employee();

                InputDateOfBirth(employee, dateOfBirthSuggestion);

                repository.RedactDateOfBirthInRecord(ID, path, employee.DateOfBirth);

                return true;
            }
            catch (Exception ex)
            {
                employee = null;
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion

        #region Изменение места рождения сотрудника и запись в базу данных обновленное место рождения сотрудника
        public int RedactPlaceOfBirthOfEmployee(Repository repository, int ID, string path,
                     string placeOfBirthSuggestion)
        {
            string errorMessage = string.Empty;

            int correctOutput = 0;

            if (TryRedactPlaceOfBirthOfEmployee(repository, ID, placeOfBirthSuggestion, path,
                out errorMessage, out Employee employee) == true)
            {
                correctOutput = 1;
                return correctOutput;
            }
            else
            {
                return correctOutput;
            }
        }


        private bool TryRedactPlaceOfBirthOfEmployee(Repository repository, int ID,
            string placeOfBirthSuggestion, string path, out string errorMessage, out Employee employee)
        {
            errorMessage = string.Empty;

            currentPosition = Console.GetCursorPosition();

            try
            {
                employee = new Employee();

                InputPlaceOfBirth(employee, placeOfBirthSuggestion);

                repository.RedactPlaceOfBirthInRecord(ID, path, employee.PlaceOfBirth);

                return true;
            }
            catch (Exception ex)
            {
                employee = null;
                errorMessage = ex.Message;
                PrintErrorMessage(errorMessage);
                Console.ReadLine();
                return false;
            }
        }
        #endregion


        /// <summary>
        /// Выводит сообщение об ошибке пользователю
        /// </summary>
        /// <param name="errorMessage"></param>
        private static void PrintErrorMessage(string errorMessage)
        {
            Console.Write($"\n\t{errorMessage}");
            Console.Write("\n\tPlease, repeat the input. Click enter to continue programm ....");
        }

        /// <summary>
        /// Очищает неправильно введенное пользователем поле в консоли
        /// </summary>
        /// <param name="currentPosition">Текущая позиция курсора</param>
        private static void ClearIncorrectInput((int, int) currentPosition)
        {
            //Очищает неправильно введенный пользователем текст, а также сообщение об ошибке
            Console.SetCursorPosition(currentPosition.Item1, currentPosition.Item2);
            Console.Write(new String(' ', Console.BufferWidth * 15));
            Console.SetCursorPosition(currentPosition.Item1, currentPosition.Item2);
        }
    }
}
