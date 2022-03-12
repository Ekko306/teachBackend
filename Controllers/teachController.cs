using teachBackend.Models;
using teachBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace teachBackend.Controllers
{
    [Route("/api/teacher")]
    [ApiController]
    public class TeachersController: ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeachersController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public ActionResult<List<Teacher>> Get() =>
            _teacherService.Get();

        [HttpGet("{teacherId:length(24)}", Name ="GetTeacher")]
        public ActionResult<Teacher> GetTeacher(string teacherId)
        {
            var teacher = _teacherService.Get(teacherId);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        [HttpPost]
        public ActionResult<Teacher> Create(Teacher teacher)
        {
            _teacherService.Create(teacher);

            return CreatedAtRoute(nameof(GetTeacher), new { teacherId = teacher.Id.ToString() }, teacher);
        }


        [HttpPut("{teacherId:length(24)}")]
        public IActionResult Update(string teacherId, Teacher teacherIn)
        {
            var teacher = _teacherService.Get(teacherId);

            if (teacher == null)
            {
                return NotFound();
            }

            _teacherService.Update(teacherId, teacherIn);


            return CreatedAtRoute(nameof(GetTeacher), new { teacherId = teacher.Id.ToString() }, teacherIn);
        }

        [HttpDelete("{teacherId:length(24)}")]
        public IActionResult Delete(string teacherId)
        {
            var teacher = _teacherService.Get(teacherId);

            if (teacher == null)
            {
                return NotFound();
            }

            _teacherService.Remove(teacher.Id);

            return NoContent();
        }
    }

    [Route("/api/student")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<List<Student>> Get() =>
            _studentService.Get();

        [HttpGet("{studentId:length(24)}", Name = "GetStudent")]
        public ActionResult<Student> GetStudent(string studentId)
        {
            var student = _studentService.Get(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            _studentService.Create(student);

            return CreatedAtRoute(nameof(GetStudent), new { studentId = student.Id.ToString() }, student);
        }


        [HttpPut("{studentId:length(24)}")]
        public IActionResult Update(string studentId, Student studentIn)
        {
            var student = _studentService.Get(studentId);

            if (student == null)
            {
                return NotFound();
            }

            _studentService.Update(studentId, studentIn);

            return CreatedAtRoute(nameof(GetStudent), new { studentId = student.Id.ToString() }, studentIn);
        }

        [HttpDelete("{studentId:length(24)}")]
        public IActionResult Delete(string studentId)
        {
            var student = _studentService.Get(studentId);

            if (student == null)
            {
                return NotFound();
            }

            _studentService.Remove(student.Id);

            return NoContent();
        }
    }


    [Route("/api/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;

        public ClassController(StudentService studentService, TeacherService teacherService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
        }

        [HttpGet("student/{classId}")]  // 没有length就是不指定宽度
        public ActionResult<List<Student>> GetStudentByClass(string classId)
        { 
            var student = _studentService.GetByClass(classId);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpGet("teacher/{classId}")]  // 没有length就是不指定宽度
        public ActionResult<List<Teacher>> GetTeacherByClass(string classId)
        {
            var teacher = _teacherService.GetByClass(classId);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }


        [HttpGet("login/{email}/{password}")]
        public ActionResult<LoginQuery> CheckLogin(string email, string password)
        {
            var teacherResult = _teacherService.CheckTeacher(email, password);
            var studentResult = _studentService.CheckStudent(email, password);
            if (teacherResult.Id != "-1")
            {
                return teacherResult;
            }
            else if(studentResult.Id != "-1")
            {
                return studentResult;
            }
            else
            {
                return new LoginQuery("-1", "error"); // 没查询到 账号或密码错误
            }
        }
    }


    [Route("/api/record")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly RecordService _recordService;

        public RecordController(RecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet("{recordId:length(24)}", Name = "GetRecord")]
        public ActionResult<Record> GetRecord(string recordId)
        {
            var record = _recordService.Get(recordId);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }


        [HttpDelete("{recordId:length(24)}")]
        public IActionResult Delete(string recordId)
        {
            var record = _recordService.Get(recordId);

            if (record == null)
            {
                return NotFound();
            }

            _recordService.Remove(record.Id);

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Record> Create(Record record)
        {
            _recordService.Create(record);

            return CreatedAtRoute(nameof(GetRecord), new { recordId = record.Id.ToString() }, record); ; // 这里要返回新建内容 告诉recordId 前端可能可以用
        }


        [HttpPut("{recordId:length(24)}")]
        public IActionResult Update(string recordId, Record recordIn)
        {
            var student = _recordService.Get(recordId);

            if (student == null)
            {
                return NotFound();
            }

            _recordService.Update(recordId, recordIn);

            return NoContent();
        }

        [HttpGet("teacher/{teacherId:length(24)}")]  // 没有length就是不指定宽度
        public ActionResult<List<Record>> GetRecordByTeacher(string teacherId)
        {
            var records = _recordService.GetByTeacher(teacherId);

            if (records == null)
            {
                return NotFound();
            }

            return records;
        }

        [HttpGet("student/{classNum}")]
        public ActionResult<List<Record>> GetRecordByStudentClass(string classNum)
        {
            var records = _recordService.GetByStudentClass(classNum);

            if (records == null)
            {
                return NotFound();
            }

            return records;
        }
    }
}
