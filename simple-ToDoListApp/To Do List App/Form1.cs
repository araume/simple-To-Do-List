using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace To_Do_List_App
{
    public partial class ToDoList : Form
    {
        Connect cnc = new Connect();
        string selectedtitle, selecteddescription;
        public ToDoList()
        {
            InitializeComponent();
        }

        SqlDataAdapter adapter;
        DataTable dataTable = new DataTable();
        bool isEditing = false;

        private void ToDoList_Load(object sender, EventArgs e)
        {
            DataTable DSRec = cnc.DSRec;
            DSRec = cnc.GetDataTable("SELECT * FROM ToDolist;");
            ToDoListView.DataSource = DSRec;
            ToDoListView.Columns[0].Visible = false;
        }
        private void NewButton_Click(object sender, EventArgs e)
        {
            txtboxTitle.Text = "";
            txtboxDescription.Text = "";
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            isEditing = true;
            // Fill the text fields with data from table
            txtboxTitle.Text = selectedtitle;
            txtboxDescription.Text = selecteddescription;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                cnc.executeSQL("DELETE FROM ToDolist WHERE Title = '" + selectedtitle + "'");
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            ToDoList_Load(sender, e);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                cnc.executeSQL("UPDATE ToDolist SET Title = '" + txtboxTitle.Text + "', Description = '" + txtboxDescription.Text + "' WHERE Title = '" + selectedtitle + "'");
            }
            else
            {
                cnc.executeSQL("INSERT INTO ToDolist (Title, Description) VALUES ('" + txtboxTitle.Text + "', '" + txtboxDescription.Text + "')");
            }
            // Clear text fields after saving
            txtboxTitle.Text = "";
            txtboxDescription.Text = "";
            isEditing = false;
            ToDoList_Load(sender, e);
        }

        private void ToDoListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedtitle = ToDoListView.Rows[e.RowIndex].Cells[1].Value.ToString();
                selecteddescription = ToDoListView.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}