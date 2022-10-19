using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalOfEmployeeWorkbooks.Views
{
    /// <summary>
    /// Реализует передачу свойств из ввода пользователя в модель
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Текст ID сотрудника
        /// </summary>
        string TextID { get; set; }
        /// <summary>
        /// Текст фамилии сотрудника
        /// </summary>
        string TextSecondName { get; set; }
        /// <summary>
        /// Текст имени сотрудника
        /// </summary>
        string TextFirstName { get; set; }
        /// <summary>
        /// Текст отчества сотрудника
        /// </summary>
        string TextThirdName { get; set; }
        /// <summary>
        /// Текст возраста сотрудника
        /// </summary>
        string TextAge { get; set; }
        /// <summary>
        /// Текст даты рождения сотрудника
        /// </summary>
        string TextDateOfBirth { get; set; }
        /// <summary>
        /// Текст места рождения сотрудника
        /// </summary>
        string TextPlaceOfBirth { get; set; }
        /// <summary>
        /// Текст должности сотрудника
        /// </summary>
        string TextPostOfEmployee { get; set; }


    }
}
