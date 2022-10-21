using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JournalOfEmployeeWorkbooks.Views;

namespace JournalOfEmployeeWorkbooks.Views
{
    public class Repository : IRepository
    {
        /// <summary>
        /// Список сотрудников компании
        /// </summary>
        private List<Employee> dataOfEmployees;

        public List<Employee> DataOfEmployees
        {
            get { return dataOfEmployees; }

            set { dataOfEmployees = value; }
        }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        public Repository(List<Employee> dataOfEmployees)
        {
            DataOfEmployees = dataOfEmployees;
        }


        /// <summary>
        /// Считывает строковые данные о сотрудниках из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Возвращает список List данных о сотрудниках</returns>
        private List<string> GetStringRecordsFromFile(string path)
        {
            List<string> stringDataOfEmployee = new List<string>();

            string line = "";
            //Чтение данных из файла
            using (StreamReader streamReader = new StreamReader(path))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    //Добавление в список
                    stringDataOfEmployee.Add(line);
                }
            }

            return stringDataOfEmployee;
        }

        /// <summary>
        /// Просмотр всех записей в базе данных
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Список записей всех сотрудников</returns>
        public List<Employee> ViewAllRecords(string path)
        {
            DataOfEmployees = new List<Employee>();

            List<string> stringData = GetStringRecordsFromFile(path);
            
            for (int i = 0; i < stringData.Count; i++)
            {
                //Разделенные строковые данные
                string[] separatedData = stringData[i].Split('#');

                Employee employee = new Employee();

                employee.ID = int.Parse(separatedData[0]);
                employee.DateTimeAddingRegistration = DateTime.Parse(separatedData[1]);
                employee.SecondName = separatedData[2];
                employee.FirstName = separatedData[3];
                employee.ThirdName = separatedData[4];
                employee.DateOfBirth = separatedData[5];
                employee.Age = separatedData[6];
                employee.PlaceOfBirth = separatedData[7];
                employee.PostOfEmployee = separatedData[8];
                //Добавлеие в основной список
                DataOfEmployees.Add(employee);
            }
            return DataOfEmployees;
        }
        /// <summary>
        /// Просматривает запись сотрудника из базы данных по его ID
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Объект сотрудника</returns>
        /// <exception cref="ApplicationException">Если сотрудник не найден</exception>
        public Employee ViewingOneRecord(int ID, string path)
        {
            try
            {
                DataOfEmployees = ViewAllRecords(path);
                var employee = GetEmployeeByID(ID);
                return employee;
            }
            catch
            {
                ErrorMessage = "Employee was not found!";
                throw new ApplicationException(ErrorMessage);
            }
        }

        /// <summary>
        /// Создает объект сотрудника, добавляет его в список сотрудников и записывает в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="secondName">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="thirdName">Отчество</param>
        /// <param name="age">Возраст</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        /// <param name="placeOfBirth">Место рождения</param>
        /// <param name="postOfEmployee">Должность, занимаемая сотрудником</param>
        public void CreateRecord(string path, Employee employee)
        {
            DataOfEmployees = ViewAllRecords(path);

            DataOfEmployees.Add(employee);

            WriteInformationToFile(path);
        }

        /// <summary>
        /// Удалить запись сотрудника из файла по его ID
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        /// <param name="path">Путь к файлу</param>
        public void DeleteRecord(int ID, string path)
        {
            try
            {
                //Добавление всех сотрудников из файла в список
                DataOfEmployees = ViewAllRecords(path);
                //Поиск сотрудника по его ID
                var deleteEmployee = GetEmployeeByID(ID);
                //Удаление сотрудника из списка
                DataOfEmployees.Remove(deleteEmployee);
                //Запись данных в файл после удаления
                WriteInformationToFile(path);
            }
            catch
            {
                //В случае ошибки проинформировать об этом
                ErrorMessage = "Can't find an employee with this ID!";
                throw new ApplicationException(ErrorMessage);
            }

        }

        /// <summary>
        /// Записать информацию о сотрудниках в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        private void WriteInformationToFile(string path)
        {
            //Запись в файл данных о сотрудниках
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                for (int i = 0; i < DataOfEmployees.Count; i++)
                {
                    streamWriter.WriteLine($"{DataOfEmployees[i].ID}#" +
                                           $"{DataOfEmployees[i].DateTimeAddingRegistration.ToShortDateString()}#" +
                                           $"{DataOfEmployees[i].SecondName}#" +
                                           $"{DataOfEmployees[i].FirstName}#" +
                                           $"{DataOfEmployees[i].ThirdName}#" +
                                           $"{DataOfEmployees[i].DateOfBirth}#" +
                                           $"{DataOfEmployees[i].Age}#" +
                                           $"{DataOfEmployees[i].PlaceOfBirth}#" +
                                           $"{DataOfEmployees[i].PostOfEmployee}");
                }
                streamWriter.Close();
            }
        }

        #region Редактирование фамилии сотрудника
        public void RedactSecondNameInRecord(int ID, string path, string secondName)
        {
            try
            {
                DataOfEmployees = ViewAllRecords(path);

                var employee = GetEmployeeByID(ID);

                employee.SecondName = secondName;

                WriteInformationToFile(path);
            }
            catch
            {
                ErrorMessage = "It is not possible to change the employee's last name!";
                throw new ApplicationException(ErrorMessage);
            }

        }
        #endregion

        #region Редактирование имени сотрудника
        public void RedactFirstNameInRecord(int ID, string path, string firstName)
        {
            try
            {
                DataOfEmployees = ViewAllRecords(path);

                var employee = GetEmployeeByID(ID);

                employee.FirstName = firstName;

                WriteInformationToFile(path);
            }
            catch
            {
                ErrorMessage = "Can't change the employee's name!";
                throw new ApplicationException(ErrorMessage);
            }

        }
        #endregion

        #region Редактирование отчества сотрудника
        public void RedactThirdNameInRecord(int ID, string path, string thirdName)
        {
            try
            {
                DataOfEmployees = ViewAllRecords(path);

                var employee = GetEmployeeByID(ID);

                employee.ThirdName = thirdName;

                WriteInformationToFile(path);
            }
            catch
            {
                ErrorMessage = "It is impossible to change the patronymic of an employee";
                throw new ApplicationException(ErrorMessage);
            }

        }
        #endregion

        #region Редактирование даты рождения сотрудника
        public void RedactDateOfBirthInRecord(int ID, string path, string dateOfBirth)
        {
            try
            {
                DataOfEmployees = ViewAllRecords(path);

                var employee = GetEmployeeByID(ID);

                employee.DateOfBirth = dateOfBirth;

                WriteInformationToFile(path);
            }
            catch
            {
                ErrorMessage = "It is not possible to change the employee's date of birth";
                throw new ApplicationException(ErrorMessage);
            }

        }
        #endregion

        #region Редактирование места рождения сотрудника
        public void RedactPlaceOfBirthInRecord(int ID, string path, string placeOfBirth)
        {
            try
            {
                DataOfEmployees = ViewAllRecords(path);

                var employee = GetEmployeeByID(ID);

                employee.PlaceOfBirth = placeOfBirth;

                WriteInformationToFile(path);
            }
            catch
            {
                ErrorMessage = "It is not possible to change the employee's place of birth";
                throw new ApplicationException(ErrorMessage);
            }

        }
        #endregion

        /// <summary>
        /// Получение сотрудника по его ID
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        /// <returns>CСотрудника из базы</returns>
        public Employee GetEmployeeByID(int ID)
        {
            Employee employee = DataOfEmployees.Single(x => x.ID == ID);

            return employee;
        }

        /// <summary>
        /// Загрузка записей в выбранном диапазоне дат
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="startingDate">Начальная дата</param>
        /// <param name="endDate">Конечная дата</param>
        public List<Employee> LoadingRecordsInSelectedDateRange(string path,
            DateTime startingDate, DateTime endDate)
        {
            try
            {
                //Добавление всех сотрудников из файла в список
                DataOfEmployees = ViewAllRecords(path);

                //Выбор тех записей, которые удовлетворяют тому, что дата регистрации больше
                //начальной даты и меньше конечной даты, вводимой пользователем
                var dataOfEmployeeInDateRange = DataOfEmployees.
                    Where(x => x.DateTimeAddingRegistration >= startingDate &&
                               x.DateTimeAddingRegistration <= endDate).ToList();
                //Если список записей не удовлетворяет заданным датам, то возврат пустого списка
                if(dataOfEmployeeInDateRange == null)
                {
                    return null;
                }
                return dataOfEmployeeInDateRange;
            }
            catch
            {
                ErrorMessage = "There are no records with this date!";
                throw new ApplicationException(ErrorMessage);
            }
        }
    }
}
