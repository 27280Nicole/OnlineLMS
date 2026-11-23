using Npgsql;
using OnlineLMS.Data;
using OnlineLMS.Models;

namespace OnlineLMS.Controllers
{
    public class CourseController
    {
        // Job 1: read all courses from the database
        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT id, title, description, instructor_id FROM courses ORDER BY id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            InstructorId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
                        });
                    }
                }
            }

            return courses;
        }

        // Job 2: add a new course to the database
        public void AddCourse(Course course)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO courses (title, description, instructor_id) " +
                             "VALUES (@title, @description, @instructorId)";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@title", course.Title);
                    cmd.Parameters.AddWithValue("@description", course.Description);
                    cmd.Parameters.AddWithValue("@instructorId", course.InstructorId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}