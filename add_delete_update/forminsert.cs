using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace add_delete_update
{
    public partial class forminsert : Form
    {
        string strCon;
        SqlConnection con;
        public forminsert()
        {
            InitializeComponent();
        }

        private void bttnAdd2_Click(object sender, EventArgs e)
        {

            //validation 
           
            //validate Name
            if (txtName.Text.Trim().Length == 0)
            {
                lblinfo.Text = "Please type teacher Name...";
                txtName.Focus();
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }
            //validate Address
            if (txtAddress.Text.Trim().Length == 0)
            {
                lblinfo.Text = "Please type teacher address...";
                txtAddress.Focus();
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }
            //validate Salary
            if (txtSalary.Text.Trim().Length == 0)
            {
                lblinfo.Text = "Please type salary Name...";
                txtSalary.Focus();
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }


            // variables
            string TeacherName, TeacherAddress;
            float Salary;

            //Store data in variable
            TeacherName = this.txtName.Text.Trim().Replace("'", "''");
            TeacherAddress = this.txtAddress.Text.Trim().Replace("'", "''");
            Salary = Convert.ToSingle(this.txtSalary.Text);


            //sql Qurey
            string sqlinsertQry;
            sqlinsertQry = "insert into teacherDB2 (teacherName,Address,Salary) values ('" + TeacherName + "', '" + TeacherAddress + "','" + Salary + "') ";


            //connection
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // command
            SqlCommand cmd = new SqlCommand(sqlinsertQry, con);
            int cnt = 0;

            try
            {
                cnt = cmd.ExecuteNonQuery();
                if (cnt > 0)
                {
                    this.lblinfo.Text = "Record of " + TeacherName + "added successfly";
                    clearControls();
                    ShowAllRecord2();
                }

                else
                {
                    this.lblinfo.Text = "Process Aborted";
                }
            }

            catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }

            this.lblinfo.Text = "Added successfly";
        }


        void clearControls()
        {
            lblinfo.Text = "";
            txtName.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            txtName.Focus();
        }

        void ShowAllRecord2()
        {
            // connection
            con = new SqlConnection(strCon);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // sql query  
            string sqlQry = "select * from TeacherDB2";
            SqlDataAdapter DA = new SqlDataAdapter(sqlQry, con);
            DataSet ds = new DataSet();

            //try and catch and finally
            try
            {
                DA.Fill(ds, "teacher");
                this.dataGridView1.DataSource = ds;
                this.dataGridView1.DataMember = "teacher";
            }

            // error code 
            catch (Exception ex)
            {
                this.lblinfo.Text = ex.Message;
            }

            finally
            {
                con.Close();
                DA.Dispose();
                ds.Dispose();
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            // put this code inside try and catch to avoid error when click in any place in datadridview
            try
            {
                lblID.Text = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtName.Text = this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtAddress.Text = this.dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtSalary.Text = this.dataGridView1.SelectedRows[0].Cells[3].Value.ToString();


            }
            catch (Exception ex) 
            {
                lblinfo.Text = "please doble click on start datagridview";
            }


          
        }

        private void bttnShow2_Click(object sender, EventArgs e)
        {
            ShowAllRecord2();
        }

        private void forminsert_Load(object sender, EventArgs e)
        {
            strCon = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            con = new SqlConnection(strCon);

            ShowAllRecord2();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtName.Text.Trim().Length > 0 && e.KeyChar == (int)Keys.Enter)
            {
                txtAddress.Focus();
            }
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAddress.Text.Trim().Length > 0 && e.KeyChar == (int)Keys.Enter)
            {
                txtSalary.Focus();
            }
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtSalary.Text.Trim().Length > 0 && e.KeyChar == (int)Keys.Enter)
            {
                bttnAdd2.Focus();
            }
        }

        private void bttnFind_Click(object sender, EventArgs e)
        {
            // connection
            con = new SqlConnection(strCon);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            // sql query
            string sql = "select * from TeacherDB2 where TeacherName = '" + txtFind.Text + "' ";


            // make adapter
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();


            // try and catch and finally
            try
            {
                da.Fill(ds, "tea");

                this.dataGridView1.DataSource = ds;
                this.dataGridView1.DataMember = "tea";
            }

            catch (Exception ex)
            {
                this.lblinfo.Text = ex.Message;
            }

            finally
            {
                con.Close();
                da.Dispose();
                ds.Dispose();
            }

            this.lblinfo.Text = this.dataGridView1.Rows.Count - 1 + " Record(s) Found...!";


        }

        private void bttnUpdate2_Click(object sender, EventArgs e)
        {
            //validation 

            //validate Name
            if (txtName.Text.Trim().Length == 0)
            {
                lblinfo.Text = "Please type teacher Name...";
                txtName.Focus();
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }
            //validate Address
            if (txtAddress.Text.Trim().Length == 0)
            {
                lblinfo.Text = "Please type teacher address...";
                txtAddress.Focus();
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }
            //validate Salary
            if (txtSalary.Text.Trim().Length == 0)
            {
                lblinfo.Text = "Please type salary Name...";
                txtSalary.Focus();
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }


            // variables
            string TeacherName, TeacherAddress;
            float Salary;

            //Store data in variable
            TeacherName = this.txtName.Text.Trim().Replace("'", "''");
            TeacherAddress = this.txtAddress.Text.Trim().Replace("'", "''");
            Salary = Convert.ToSingle(this.txtSalary.Text);


            //sql Qurey
            string sqlupdateQry;
            sqlupdateQry = "update teacherDB2 set teacherName = '" + TeacherName + "' , Address = '" + TeacherAddress + "', Salary = '" + Salary + "' where teacherID = '" + lblID.Text + "' ";


            //connection
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // command
            SqlCommand cmd = new SqlCommand(sqlupdateQry, con);
            int cnt = 0;

            //try and catch and finally
            try
            {
                cnt = cmd.ExecuteNonQuery();
                if (cnt > 0)
                {
                    this.lblinfo.Text = "Record of " + TeacherName + "Update successfly";
                    clearControls();
                    ShowAllRecord2();
                }

                else
                {
                    this.lblinfo.Text = "Process Aborted";
                }
            }

            catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }

            finally
            {
                con.Close();
                cmd.Dispose();
            }

            this.lblinfo.Text = "update successfly";
        }

        private void bttnDelete2_Click(object sender, EventArgs e)
        {
            //validation  for click on right place
            if(lblID.Text.Trim().Length ==0 )
            {
                this.lblinfo.Text = "please double click on the row you want to delete";
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }
            //sql Qurey
            string sqldeleteQry;
            sqldeleteQry = "delete from teacherDB2 where teacherID = '"+lblID.Text+"' ";


            //connection
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // command
            SqlCommand cmd = new SqlCommand(sqldeleteQry, con);
            int cnt = 0;

            // try and catch and finally 
            try
            {
                cnt = cmd.ExecuteNonQuery();
                if (cnt > 0)
                {
                    this.lblinfo.Text = "Record of " + lblID.Text + "deleted successfly";
                    clearControls();
                    ShowAllRecord2();
                }

                else
                {
                    this.lblinfo.Text = "Process Aborted";
                }
            }

            catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
