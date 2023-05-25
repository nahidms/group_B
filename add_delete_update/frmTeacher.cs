using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace add_delete_update
{
    public partial class frmTeacher : Form
    {

        // <connectionStrings>
        //<add name = "dbcon" connectionString="server=.\sqlexpress;database=databaseName;trusted= true" providerName="system.data.sqlClient"/>
        //</connectionStrings>


        string strCon;
        SqlConnection con;
        public frmTeacher()
        {
            InitializeComponent();
        }

        private void frmTeacher_Load(object sender, EventArgs e)
        {

            strCon = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            con = new SqlConnection(strCon);

            ShowAllRecord();
        }

        void ShowAllRecord()
        {
            // connection
            con = new SqlConnection(strCon);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            // sql query  
            string sqlQry = "select * from Teachers";
            SqlDataAdapter DA = new SqlDataAdapter(sqlQry, con);
            DataSet ds = new DataSet();



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

        private void bttnFind_Click(object sender, EventArgs e)
        {
            // connection
            con = new SqlConnection(strCon);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            // sql query
            string sql = "select * from Teachers where TeacherName = '" + txtFind.Text + "' ";


            // make adapter
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();


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

        private void bttnAdd_Click(object sender, EventArgs e)
        {

            //validation 
            //validate ID
            if (txtID.Text.Trim().Length == 0)
            {
                lblinfo.Text = "Please type teacher ID...";
                txtID.Focus();
                return;
            }
            else
            {
                this.lblinfo.Text = "";
            }

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



            // connection
            con = new SqlConnection(strCon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //sql query
            string sqlinsertQry = "insert into teachers (teacherID,teacherName,Address,Salary) values ('" + this.txtID.Text + "' ,'" + this.txtName.Text + "' , '" + this.txtAddress.Text + "' , '" + this.txtSalary.Text + "' );";

            //adapter
            SqlDataAdapter da = new SqlDataAdapter(sqlinsertQry, con);

            //data set
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds, "Add");
                this.dataGridView1.DataSource = ds;
                this.dataGridView1.DataMember = "ADD";
            }

            catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }

            this.lblinfo.Text = this.dataGridView1.Rows.Count - 1 + " Record(S) added";
            ShowAllRecord();
        }

        private void bttnShow_Click(object sender, EventArgs e)
        {
            ShowAllRecord();
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

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAddress.Text.Trim().Length > 0 && e.KeyChar == (int)Keys.Enter)
            {
                txtName.Focus();
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void bttnDelete_Click(object sender, EventArgs e)
        {
            // connection
            con = new SqlConnection(strCon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //sql query
            string sqlQry = "delete from teachers where teacherID= '" + txtID.Text + "' ";

            //adapter
            SqlDataAdapter da = new SqlDataAdapter(sqlQry, con);

            //data set
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds, "Delet");
                this.dataGridView1.DataSource = ds;
                this.dataGridView1.DataMember = "Delet";
            }

            catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }

            this.lblinfo.Text = this.dataGridView1.Rows.Count - 1 + " Record(S) deleted";
            ShowAllRecord();

        }

        private void bttnUpdate_Click(object sender, EventArgs e)
        {
            // connection
            con = new SqlConnection(strCon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            //sql query
            string sqlQry = "update teachers set teacherName= '" + txtName.Text + "' where teacherID= '" + this.txtID.Text + "'  ";

            //adapter
            SqlDataAdapter da = new SqlDataAdapter(sqlQry, con);

            //data set
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds, "update");
                this.dataGridView1.DataSource = ds;
                this.dataGridView1.DataMember = "update";
            }

            catch (Exception ex)
            {
                lblinfo.Text = ex.Message;
            }

            this.lblinfo.Text = this.dataGridView1.Rows.Count - 1 + " Record(S) updated";
            ShowAllRecord();
        }

        private void bttnAdd2_Click(object sender, EventArgs e)
        {

        }
   

        void clearControls()
        {
            lblinfo.Text = "";
            txtName.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            txtName.Focus();
        }


        
        private void bttnShow2_Click(object sender, EventArgs e)
        {
            //ShowAllRecord2();
        }
    }
}

