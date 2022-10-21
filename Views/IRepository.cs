using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalOfEmployeeWorkbooks.Views
{
    public interface IRepository
    {
        /// <summary>
        /// Загрузка всех записей
        /// </summary>
        /// <returns></returns>
        List<Employee> ViewAllRecords(string path);

        /// <summary>
        /// Просмотр записи по ID
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        /// <returns></returns>
        Employee ViewingOneRecord(int ID, string path);

        /// <summary>
        /// Создание записи сотрудника в базе
        /// </summary>
        /// <returns></returns>
        void CreateRecord(string path, Employee employee);

        /// <summary>
        /// Удаление записи по ID сотрудника
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        void DeleteRecord(int ID, string path);

        /// <summary>
        /// По ID сотрудника редактирует фамилию сотрудника
        /// </summary>
        /// <param name="ID">ID, по которому проводится поиск сотрудника в базе</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="secondName">Фамилия сотрудника</param>
        void RedactSecondNameInRecord(int ID, string path, string secondName);

        /// <summary>
        /// По ID сотрудника редактирует имя сотрудника
        /// </summary>
        /// <param name="ID">ID, по которому проводится поиск сотрудника в базе</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="firstName">Имя сотрудника</param>
        void RedactFirstNameInRecord(int ID, string path, string firstName);

        /// <summary>
        /// По ID сотрудника редактирует отчество сотрудника
        /// </summary>
        /// <param name="ID">ID, по которому проводится поиск сотрудника в базе</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="thirdName">Отчество сотрудника</param>
        void RedactThirdNameInRecord(int ID, string path, string thirdName);

        /// <summary>
        /// По ID сотрудника редактирует дату рождения сотрудника
        /// </summary>
        /// <param name="ID">ID, по которому проводится поиск сотрудника в базе</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="dateOfBirth">Дата рождения сотрудника</param>
        void RedactDateOfBirthInRecord(int ID, string path, string dateOfBirth);
       
        /// По ID сотрудника редактирует метсо рождения сотрудника
        /// </summary>
        /// <param name="ID">ID, по которому проводится поиск сотрудника в базе</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="placeOfBirth">Метсо рождения сотрудника</param>
        void RedactPlaceOfBirthInRecord(int ID, string path, string placeOfBirth);

        /// <summary>
        /// Получает объект сотрудника из базы по его ID
        /// </summary>
        /// <param name="ID">ID сотрудника</param>
        /// <returns></returns>
        Employee GetEmployeeByID(int ID);

        /// <summary>
        /// Загрузка записей в выбранном диапазоне дат
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="startingDate">Начальная дата</param>
        /// <param name="endDate">Конечная дата</param>
        List<Employee> LoadingRecordsInSelectedDateRange(string path, DateTime startingDate,
            DateTime endDate);

        

    }
}
