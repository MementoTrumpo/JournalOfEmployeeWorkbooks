using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JournalOfEmployeeWorkbooks
{
    public class Employee
    {
        /// <summary>
        /// Информация об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Уникальный идентификатор ID для каждого сотрудника
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// Дата и время добавления сотрудника
        /// </summary>
        public DateTime DateTimeAddingRegistration { get; set; }

        private string secondName;
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                if (value.Length > 30)
                {
                    ErrorMessage = "The number of letters of second name can't exceed 30 characters!";
                    throw new ApplicationException(ErrorMessage);
                }

                else if (value.Contains('#'))
                {
                    ErrorMessage = "Second name cannot contain a sign {#}!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorMessage = "Second name cannot be empty!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if(CheckStringForNumbers(value) == false)
                {
                    ErrorMessage = "Second name contains numbers!";
                    throw new ApplicationException(ErrorMessage);
                }
                else
                {
                    secondName = value;
                }
            }
        }
        private string firstName;
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value.Length > 30)
                {
                    ErrorMessage = "The number of letters of first name can't exceed 30 characters!";
                    throw new ApplicationException(ErrorMessage);
                }

                else if (value.Contains('#'))
                {
                    ErrorMessage = "First name cannot contain a sign {#}!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorMessage = "First name cannot be empty!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (CheckStringForNumbers(value) == false)
                {
                    ErrorMessage = "First name contains numbers!";
                    throw new ApplicationException(ErrorMessage);
                }
                else
                {
                    firstName = value;
                }
            }
        }
        private string thirdName;
        /// <summary>
        /// Отчество сотрдуника
        /// </summary>
        public string ThirdName
        {
            get
            {
                return thirdName;
            }
            set
            {
                if (value.Length > 30)
                {
                    ErrorMessage = "The number of letters of third name can't exceed 30 characters!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (value.Contains('#'))
                {
                    ErrorMessage = "Third name cannot contain a sign {#}!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorMessage = "Third name cannot be empty!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (CheckStringForNumbers(value) == false)
                {
                    ErrorMessage = "Third name contains numbers!";
                    throw new ApplicationException(ErrorMessage);
                }
                else
                {
                    thirdName = value;
                }
            }
        }
        private string age;
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public string Age
        {
            get
            {
                //Вычисление возраста сотрудника
                var _age = DateTime.Now.Year - dateOfBirth.Year;
                return _age.ToString();
            }
            set
            {
                age = value;
            }
        }
        private DateTime dateOfBirth;
        /// <summary>
        /// Дата раждения
        /// </summary>
        public string DateOfBirth
        {
            
            get { return dateOfBirth.ToShortDateString(); }

            set
            {
                if(DateTime.TryParse(value, out dateOfBirth) == false)
                {
                    ErrorMessage = "The date format is entered incorrectly!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if((DateTime.Now.Year - dateOfBirth.Year) < 18)
                {
                    ErrorMessage = "The age of the employee cannot be less than 18 years";
                    throw new ApplicationException(ErrorMessage);
                }
                else if ((DateTime.Now.Year - dateOfBirth.Year) > 100)
                {
                    ErrorMessage = "The age of the employee cannot be over than 100 years";
                    throw new ApplicationException(ErrorMessage);
                }
            }
        }

        private string placeOfBirth;
        /// <summary>
        /// Город, в котором родился сотрудник
        /// </summary>
        public string PlaceOfBirth
        {
            get { return placeOfBirth; }

            set
            {
                if (value.Contains('#'))
                {
                    ErrorMessage = "The place of birth cannot contain a sign {#}!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (value.Length > 50)
                {
                    ErrorMessage = "The place of birth cannot exceed 50 characters!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorMessage = "The place of birth cannot be empty!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (CheckStringForNumbers(value) == false)
                {
                    ErrorMessage = "The place of birth name contains numbers!";
                    throw new ApplicationException(ErrorMessage);
                }
                else
                {
                    placeOfBirth = value;
                }
            }
        }

        private string postOfEmployee;

        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string PostOfEmployee
        {
            get { return postOfEmployee; }

            set
            {
                if (value.Contains('#'))
                {
                    ErrorMessage = "The post of employee cannot contain a sign {#}!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (value.Length > 50)
                {
                    ErrorMessage = "The post of employee cannot exceed 50 characters!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorMessage = "The post of employee cannot be empty!";
                    throw new ApplicationException(ErrorMessage);
                }
                else if (CheckStringForNumbers(value) == false)
                {
                    ErrorMessage = "The post of employee contains numbers!";
                    throw new ApplicationException(ErrorMessage);
                }
                else
                {
                    postOfEmployee = value;
                }
            }
        }


        public Employee(string secondName, string firstName, string thirdName,
                        string age, string dateOfBirth, string placeOfBirth, string postOfEmployee)
        {
            ID = GenerateID();
            DateTimeAddingRegistration = GenerateDateTimeAddingRegistration();
            SecondName = secondName;
            FirstName = firstName;
            ThirdName = thirdName;
            DateOfBirth = dateOfBirth;
            Age = age;
            PlaceOfBirth = placeOfBirth;
            PostOfEmployee = postOfEmployee;
        }

        public Employee() 
        {
            ID = GenerateID();
            DateTimeAddingRegistration = GenerateDateTimeAddingRegistration();
        }
        
        /// <summary>
        /// Генерирует ID для сотрудника
        /// </summary>
        /// <returns>Случайное число int от 0 до int.MaxValue</returns>
        private int GenerateID()
        {
            Random randomNamber = new Random();

            ID = randomNamber.Next(1, int.MaxValue);

            return ID;
        }

        /// <summary>
        /// Генерирует дату добавления записи
        /// </summary>
        /// <returns></returns>
        private DateTime GenerateDateTimeAddingRegistration()
        {
            DateTimeAddingRegistration = DateTime.Now;

            return DateTimeAddingRegistration;
        }

        public override string ToString()
        {
            return $"\tID - {ID}\n" +
                   $"\tDate time adding registration note - {DateTimeAddingRegistration.ToShortDateString()}\n" +
                   $"\tSecondName - {SecondName}\n" +
                   $"\tFirstName - {FirstName}\n" +
                   $"\tThirdName - {ThirdName}\n" +
                   $"\tAge - {Age}\n" +
                   $"\tDate of birth - {DateOfBirth}\n" +
                   $"\tPlace of birth - {PlaceOfBirth}\n" +
                   $"\tPost of employee - {PostOfEmployee}\n";
        }

        /// <summary>
        /// Проверка строки на наличие цифр и других знаков (кроме пробела и тире)
        /// </summary>
        /// <param name="str">Исходная строка для проверка</param>
        /// <returns>true - если в строке нет цифр, false - если строка содержит цифры</returns>
        private bool CheckStringForNumbers(string str)
        {
           
            string[] _str = str.Split(new char[] { ' ', '-', '/', '|', ',',
                ';', ':', '<', '>'},StringSplitOptions.RemoveEmptyEntries);
            
            if (_str.Length == 0)
            {
                return false;
            }
            
            foreach (var s in _str)
            {
                foreach(var ch in s)
                {
                    if(char.IsLetter(ch) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        




    }
}
