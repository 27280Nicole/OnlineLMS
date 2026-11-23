using OnlineLMS.Controllers;
using OnlineLMS.Models;

namespace OLMS.Views
{
    public partial class CourseForm : Form
    {
        private readonly CourseController controller = new CourseController();

        public CourseForm()
        {
            InitializeComponent();
            Load += CourseForm_Load;
            btnAdd.Click += btnAdd_Click;
        }

        private void CourseForm_Load(object? sender, EventArgs e)
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            try
            {
                dgvCourses.DataSource = null;
                dgvCourses.DataSource = controller.GetAllCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load courses: " + ex.Message);
            }
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a course title.");
                return;
            }

            try
            {
                var course = new Course
                {
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    InstructorId = 1
                };

                controller.AddCourse(course);

                txtTitle.Clear();
                txtDescription.Clear();
                LoadCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not add course: " + ex.Message);
            }
        }
    }
}