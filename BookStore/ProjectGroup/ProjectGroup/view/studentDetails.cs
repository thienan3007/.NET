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
using ProjectGroup.data;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Controls;

namespace ProjectGroup.view
{
    public partial class studentDetails : DevExpress.XtraEditors.XtraForm
    {
        private bool addOrEdit;

        public Student StudentAddOrEdit { get; set; }
        public StudentData studentData = new StudentData();
        public StudentStatusData statusData = new StudentStatusData();
        public studentDetails()
        {
            InitializeComponent();
        }

        public studentDetails(bool flag, Student s) : this()
        {
            addOrEdit = flag;
            StudentAddOrEdit = s;
            InitData();
        }

        private void InitData()
        {
            txtName.Text = StudentAddOrEdit.StudentName;
            txtPhone.Text = StudentAddOrEdit.Phone;
            txtEmail.Text = StudentAddOrEdit.Email;
            txtAddress.Text = StudentAddOrEdit.Address;
            InitComboxboxStatus();
        }

        private void InitComboxboxStatus ()
        {
            List<StudentStatus> studentStatuses = statusData.GetStudentStatuses();
            cbStatus.DataSource = studentStatuses;
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Id";
            if(addOrEdit == false)
            {
                cbStatus.FindStringExact(StudentAddOrEdit.Status.Name);
            }
        }

        private bool ValidStudentName(string studentName, out string errorMsg)
        {
            if(studentName.Length == 0)
            {
                errorMsg = "Student's name must not be empty!";
                return false;
            }
            if (!Regex.Match(studentName.Trim(), "^[0-9]*$").Success)
            {
                errorMsg = "";
                return true;
            }
            errorMsg = "Student's name must not be empty and be a string!";
            return false;
        }

        private bool ValidStudentEmail(string studentEmail, out string errorMsg)
        {
            if (studentEmail.Length == 0)
            {
                errorMsg = "Student's email must not be empty!";
                return false;
            }
            if (studentEmail.Contains("@"))
            {
                errorMsg = "";
                return true;
            }
            errorMsg = "Student's email must not be empty and be a valid email!";
            return false;
        }

        private bool ValidStudentPhone(string studentPhone, out string errorMsg)
        {
            if (studentPhone.Length == 0)
            {
                errorMsg = "Student's name must not be empty!";
                return false;
            }
            if (Regex.Match(studentPhone.Trim(), "^[0-9]*$").Success)
            {
                errorMsg = "";
                return true;
            }
            errorMsg = "Student's phone must not be empty and be a string of number!";
            return false;
        }

        private bool ValidStudentAddress(string studentAddress, out string errorMsg)
        {
            if (studentAddress.Length == 0)
            {
                errorMsg = "Student's name must not be empty!";
                return false;
            }
            errorMsg = "";
            return true;
        }

        private void btnCancelClick_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtName, "");
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if(!ValidStudentName(txtName.Text, out errorMsg))
            {
                e.Cancel = true;
                txtName.Select(0, txtName.Text.Length);

                this.dxErrorProvider1.SetError(txtName, errorMsg);
            }
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtEmail, "");
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if(!ValidStudentEmail(txtEmail.Text.Trim(), out errorMsg))
            {
                e.Cancel = true;
                txtEmail.Select(0, txtEmail.Text.Length);

                this.dxErrorProvider1.SetError(txtEmail, errorMsg);
            }
        }

        private void txtPhone_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtPhone, "");
        }

        private void txtPhone_Validating_1(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidStudentPhone(txtPhone.Text.Trim(), out errorMsg))
            {
                e.Cancel = true;
                txtPhone.Select(0, txtPhone.Text.Length);

                this.dxErrorProvider1.SetError(txtPhone, errorMsg);
            }
        }

        private void txtAddress_Validated(object sender, EventArgs e)
        {
            dxErrorProvider1.SetError(txtAddress, "");
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidStudentAddress(txtAddress.Text.Trim(), out errorMsg))
            {
                e.Cancel = true;
                txtAddress.Select(0, txtAddress.Text.Length);

                this.dxErrorProvider1.SetError(txtAddress, errorMsg);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            int studentID = -1;
            StudentAddOrEdit.StudentName = txtName.Text;
            StudentAddOrEdit.Phone = txtPhone.Text;
            StudentAddOrEdit.Email = txtEmail.Text;
            StudentAddOrEdit.Address = txtAddress.Text;
            StudentAddOrEdit.Status = cbStatus.SelectedItem as StudentStatus;
            if (addOrEdit == true)
            {
                studentID = studentData.AddStudent(StudentAddOrEdit);
            }
            else
            {
                result = studentData.UpdateStudent(StudentAddOrEdit);
            }
            if (result == true || studentID > 0)
            {
                MessageBox.Show("Save successfully");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Fail");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.Disable;
            this.Close();
        }
    }
}