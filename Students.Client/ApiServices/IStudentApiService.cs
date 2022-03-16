using Students.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Students.Client.ApiServices
{
    public interface IStudentApiService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(string id);
        Task<Student> CreateStudent(Student movie);
        Task<Student> UpdateStudent(Student movie);
        Task DeleteStudent(int id);
        Task<UserInfoViewModel> GetUserInfo();
    }
}
