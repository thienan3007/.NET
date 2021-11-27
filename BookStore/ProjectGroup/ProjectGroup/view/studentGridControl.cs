using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectGroup.entities;
using DevExpress.XtraGrid.Columns;
using ProjectGroup.data;

namespace ProjectGroup.view
{
    public partial class studentGridControl : DevExpress.XtraEditors.XtraForm
    {
        private StudentData studentData = new StudentData();
        private StudentStatusData studentStatusData = new StudentStatusData();
        public studentGridControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<Student> students = studentData.GetStudent();
            bsStudents.DataSource = students;

            List<StudentStatus> studentStatuses = studentStatusData.GetStudentStatuses();
            bsStudentStatus.DataSource = studentStatuses;

            gridControl.DataSource = bsStudents;
            gridView1.PopulateColumns();
            gridView1.Columns.Remove(gridView1.Columns["Status"]);
            GridColumn deleteCol = new GridColumn() { FieldName="Delete",Visible=true, Caption="Delete"};
            gridView1.Columns.Add(deleteCol);

            GridColumn editCol = new GridColumn() { FieldName = "Edit",Visible=true, Caption = "Edit" };
            gridView1.Columns.Add(editCol);

            gridView1.Columns["StatusID"].ColumnEdit = repositoryItemLookUpEdit1;
            gridView1.Columns["Delete"].ColumnEdit = btnDelete;
            gridView1.Columns["Edit"].ColumnEdit = btnEdit;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show($"Do you really want to delete student {(gridView1.GetFocusedRow() as Student).StudentName} ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(studentData.DeleteStudent((gridView1.GetFocusedRow() as Student).StudentID))
                {
                    XtraMessageBox.Show("Delete successful");
                    LoadData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Student s = gridView1.GetFocusedRow() as Student;
            studentDetails studentDetails = new studentDetails(false, s);
            DialogResult r = studentDetails.ShowDialog();
            if(r == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}