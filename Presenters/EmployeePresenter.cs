using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using JournalOfEmployeeWorkbooks.Views;

namespace JournalOfEmployeeWorkbooks.Presenters
{
    public class EmployeePresenter
    {
        private Employee employee;

        public Employee Emp
        {
            get { return employee; }
            set { employee = value; }
        }
        
        private const string PATH = @"D:\source\Проекты SkillBox\JournalOfEmployeeWorkbooks\InformationAboutEmployees.txt";

        private Repository repositoryOfEmployees = new Repository(dataOfEmployees);
        /// <summary>
        /// Список всех сотрудников
        /// </summary>
        private static List<Employee> dataOfEmployees;
        /// <summary>
        /// Объект для представления модели
        /// </summary>
        private IEmployee employeeView;

        public EmployeePresenter(IEmployee view)
        {
            employeeView = view;
        }

        
        public void InputFirstName()
        {
             
        }
        /// <summary>
        /// Делает запись в журнале
        /// </summary>
        public void CreateRecording()
        {
            
        }

        public void EnteringFirstName()
        {
            string errorMessage;

            while (!TryInputFirstName(employee, out errorMessage))
            {
                employee.FirstName = employeeView.TextFirstName;

                
            }
        }
        //TODO: Перенети этот кусок кода в основной класс, отрисовывающий все в консоли
        private bool CheckFirstName(string firstName)
        {
            if(firstName.Length > 30)
            {
                return false;
            }

            else if(firstName.All(c => Char.IsLetter(c) || c == ' ') == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Вывод сообщения об ошибке при преобразовании типов
        /// </summary>
        /// <param name="errorMessage"></param>
        public void ShowError(string errorMessage)
        {
            Console.Write(errorMessage);
        }


        private bool TryInputFirstName(Employee emp, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                emp.FirstName = employeeView.TextFirstName;
                return true;
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private bool TryInputSecondName(Employee emp, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                emp.SecondName = employeeView.TextSecondName;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        //Попытка присваивания модели того, что ввел пользователь через Представление
        private bool TryInputThirdName(Employee emp, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                emp.ThirdName = employeeView.TextThirdName;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }







    }
}
