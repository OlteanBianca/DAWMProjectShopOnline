using OnlineShop.AppDbContext;
using OnlineShop.Entities;
using System.Diagnostics;

namespace OnlineShop.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(ShopDbContext dbContext) : base(dbContext) { }

        #region Public Methods
        //public async Task<bool> AddGrade(AddGradeDto gradeDto)
        //{
        //    var Course = await _dbContext.CourseTypes.FirstOrDefaultAsync(e => e.Name == gradeDto.CourseName);

        //    if (Course == null)
        //    {
        //        return false;
        //    }
        //    Grade grade = new()
        //    {
        //        Value = gradeDto.Value,
        //        CourseTypeId = Course.Id,
        //        StudentId = gradeDto.StudentId
        //    };
        //    _dbContext.Add(grade);
        //    await _dbContext.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<Dictionary<string, List<double>>> GetGroupedGradesForStudent(Student student)
        //{
        //    return await _dbContext.Grades
        //        .GroupBy(e => e.CourseType.Name)
        //        .Select(e => new
        //        {
        //            CourseType = e.Key,
        //            Grades = e.Select(e => e.Value).ToList()
        //        }).ToDictionaryAsync(e => e.CourseType, e => e.Grades);
        //}

        //public async Task<AverageGradesDto> GetAverageGradesForStudent(Student student)
        //{
        //    AverageGradesDto averageGrades = new()
        //    {
        //        FullName = student.FirstName + " " + student.LastName,
        //        AverageGradePerCourse = await _dbContext.Grades
        //        .GroupBy(e => e.CourseType.Name)
        //        .Select(e => new
        //        {
        //            CourseType = e.Key,
        //            Mark = e.Select(e => e.Value).AsQueryable().Average()
        //        }).ToDictionaryAsync(e => e.CourseType, e => e.Mark)
        //    };
        //    averageGrades.AverageTotalGrade = averageGrades.AverageGradePerCourse.Values.Average();
        //    return averageGrades;
        //}

        #endregion
    }
}
