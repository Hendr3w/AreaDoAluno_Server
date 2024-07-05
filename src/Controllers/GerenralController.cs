using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    
    public class GeneralController : Controller
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private readonly DataContext _context;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public async Task<Address?> GetAdressId(int id)
        {
                var adress = await _context.Address.FirstOrDefaultAsync(a => a.Id == id);
                if (adress == null)
                    return null;


                return adress;
            
        }

        public async Task<Class?> GetClassId(int id)
        {
                var _class = await _context.Class.FirstOrDefaultAsync(a => a.Id == id);
                if (_class == null)
                    return null;


                return _class;
            
        }
        public async Task<Course?> GetCourseId(int id)
        {
                var course = await _context.Course.FirstOrDefaultAsync(a => a.Id == id);
                if (course == null)
                    return null;


                return course;
            
        }
        public async Task<Discipline?> GetDisciplineId(int id)
        {
                var discipline = await _context.Discipline.FirstOrDefaultAsync(a => a.Id == id);
                if (discipline == null)
                    return null;


                return discipline;
            
        }
        public async Task<Enrollment?> GetEnrollmentId(int id)
        {
                var enrollment = await _context.Enrollment.FirstOrDefaultAsync(a => a.Id == id);
                if (enrollment == null)
                    return null;


                return enrollment;
            
        }
        public async Task<Exam?> GetExamId(int id)
        {
                var exam = await _context.Exam.FirstOrDefaultAsync(a => a.Id == id);
                if (exam == null)
                    return null;


                return exam;
            
        }
        public async Task<Materials?> GetMaterialId(int id)
        {
                var materials = await _context.Materials.FirstOrDefaultAsync(a => a.Id == id);
                if (materials == null)
                    return null;


                return materials;
            
        }
        public async Task<Message?> GetMessageId(int id)
        {
                var adress = await _context.Message.FirstOrDefaultAsync(a => a.Id == id);
                if (adress == null)
                    return null;


                return adress;
            
        }
        public async Task<Professor?> GetProfessorId(int id)
        {
                var professor = await _context.Professor.FirstOrDefaultAsync(a => a.Id == id);
                if (professor == null)
                    return null;


                return professor;
            
        }
        public async Task<Student?> GetStudentId(int id)
        {
                var student = await _context.Student.FirstOrDefaultAsync(a => a.Id == id);
                if (student == null)
                    return null;


                return student;
            
        }
        
        public async Task<Tuition?> GetTuitionId(int id)
        {
                var tuition = await _context.Tuition.FirstOrDefaultAsync(a => a.Id == id);
                if (tuition == null)
                    return null;


                return tuition;
            
        }


    }
}

