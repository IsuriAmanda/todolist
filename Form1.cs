using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todolist2
{
    public partial class ToDoList: Form
    {
        public ToDoList()
        {
            InitializeComponent();
        }

        DataTable todoList = new DataTable();
        bool isEditing = false;

        private void ToDoList_Load(object sender, EventArgs e)
        {
            // Get the screen size
    int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // Set form size to half of the screen width and height
            this.Size = new Size(screenWidth / 2, screenHeight / 2);



            todoList.Columns.Add("Title");
            todoList.Columns.Add("Description");

            toDoListView.DataSource = todoList;
            toDoListView.AllowUserToAddRows = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (toDoListView.CurrentRow != null)
            {
                isEditing = true;
                int rowIndex = toDoListView.CurrentCell.RowIndex;

                titleTextBox.Text = todoList.Rows[rowIndex]["Title"].ToString();
                descriptionTextBox.Text = todoList.Rows[rowIndex]["Description"].ToString();
            }
            else
            {
                MessageBox.Show("Please select a task to edit.", "No Task Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            ClearFields();
            isEditing = false;

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (toDoListView.CurrentRow != null)
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "No Task Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Task title cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isEditing)
            {
                int rowIndex = toDoListView.CurrentCell.RowIndex;
                todoList.Rows[rowIndex]["Title"] = titleTextBox.Text;
                todoList.Rows[rowIndex]["Description"] = descriptionTextBox.Text;
            }
            else
            {
                todoList.Rows.Add(titleTextBox.Text, descriptionTextBox.Text);
            }

            ClearFields();
            isEditing = false;
        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            if (todoList.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to clear all tasks?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    todoList.Clear();
                }
            }
            else
            {
                MessageBox.Show("No tasks to clear.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ClearFields()
        {
            titleTextBox.Text = "";
            descriptionTextBox.Text = "";
        }
    }
}
