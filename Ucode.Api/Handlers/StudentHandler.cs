using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class StudentHandler(AppDbContext context) : IStudentHandler
    {

        public Task<Response<List<Student>>> GetAllAsync(GetAllStudentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Student>> GetByIdAsync(GetStudentByRequest request)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<Student>> CreateAsync(CreateStudentRequest request)
        {
            try
            {
                var student = new Student
                {
                    UserId = request.UserId,
                    Name = request.Name,
                    Email = request.Email,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender
                };

                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();

                return new Response<Student>(student, 201,"Estudante criado com sucesso");
            }
            catch
            {
                return new Response<Student>(null, 500, "Não foi possível criar o estudante");
            }

        }
        public Task<Response<Student>> UpdateAsync(UpdateStudentRequest request)
        {
            throw new NotImplementedException();
        }
        public Task<Response<Student>> DeleteAsync(DeleteStudentRequest request)
        {
            throw new NotImplementedException();
        }    
               
    }
}
