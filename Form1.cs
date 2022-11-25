
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;
using WinFormsApp2.LoginSysten;
using MySqlX.XDevAPI.Common;
using System.Collections;

namespace WinFormsApp2

{
    namespace LoginSysten
    {
        public class Config
        {
            string ConectionString = "";  // save connection string
            public MySqlConnection connection = null;
            public string server = "127.0.0.1";// MySQL host / ip of the computer
            public string user = "root";// MySQL user
            public string password = "";// MySQL password 
            DataSet ds;
            DataTable dt;
            public string Table = "livre"; // initialize db table
            public string ConnectionType = "";
            string RecordSource = "";

            DataGridView tempdata;

            public Config()
            {

            }

            // function to connect to the database
            public void Connect(string database_name)
            {
                try
                {

                    ConectionString = "SERVER=" + server + ";" + "DATABASE=" + database_name + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";

                    connection = new MySqlConnection(ConectionString);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }

            // Function to execute select statements
            public void ExecuteSql(string Sql_command)
            {

                nowquiee(Sql_command);

            }

            // creates connection to MySQL before execution
            public void nowquiee(string sql_comm)
            {
                try
                {
                    MySqlConnection cs = new MySqlConnection(ConectionString);
                    cs.Open();
                    MySqlCommand myc = new MySqlCommand(sql_comm, cs);
                    myc.ExecuteNonQuery();
                    cs.Close();


                }
                catch (Exception err)
                {

                    MessageBox.Show(err.Message);
                }
            }

            // function to execute delete , insert and update
            public void Execute(string Sql_command)
            {
                RecordSource = Sql_command;
                ConnectionType = Table;
                dt = new DataTable(ConnectionType);
                try
                {
                    string command = RecordSource.ToUpper();

                    //======================if sql contains select==========================================
                    MySqlDataAdapter da2 = new MySqlDataAdapter(RecordSource, connection);

                    DataSet tempds = new DataSet();
                    da2.Fill(tempds, ConnectionType);
                    da2.Fill(tempds);

                    //======================================================================================


                }
                catch (Exception err) { MessageBox.Show(err.Message); }
            }

            // function to bring selected results based on column name and row index
            public string Results(int ROW, string COLUMN_NAME)
            {
                try
                {
                    return dt.Rows[ROW][COLUMN_NAME].ToString();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    return "";

                }
            }

            // function to bring selected results based on column index and row index
            

            // Execute select statement
            public void ExecuteSelect(string Sql_command)
            {
                RecordSource = Sql_command;
                ConnectionType = Table;

                dt = new DataTable(ConnectionType);
                try
                {
                    string command = RecordSource.ToUpper();
                    MySqlDataAdapter da = new MySqlDataAdapter(RecordSource, connection);
                    ds = new DataSet();
                    da.Fill(ds, ConnectionType);
                    da.Fill(dt);
                    tempdata = new DataGridView();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }


            }

            // count Number of rows after selecy
            public int Count()
            {
                return dt.Rows.Count;
            }
        }
    }
    public partial class Form1 : Form
    {
        Config db = new Config();
        public Form1()
        {
            InitializeComponent();
            db.Connect("userdata");
        }
     
        private void button2_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            db.Execute("INSERT INTO livre (nom_livre,prix) " +
                "VALUES nom_livre='" + textBox1.Text + "' , prix ='" + textBox2.Text + "'");
           
                bool flag = listBox1.Items.Contains(textBox1);
            if (this.listBox1.Items.Contains(textBox1.Text))
            {
                MessageBox.Show("book already existing");
            }
            else
            {
                MessageBox.Show("livre ajouter");
                listBox1.Items.Add(textBox1.Text);
                listBox2.Items.Add(textBox2.Text);
            }
            
          

        }

        private void button3_Click(object sender, EventArgs e)
        {


            listBox1.Items.Remove(listBox1.SelectedItem);
           // int index = listBox1.SelectedIndex;
           // listBox2.Items.RemoveAt(index);
            
            

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox1 = new ListBox();
            listBox1.Items.Add("rythem of war");
            listBox1.Items.Add("oethbringer");
            listBox1.Items.Add("words of raidiance");
            listBox1.Items.Add("the way of kings");

            
            this.Controls.Add(listBox1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            db.Execute("UPDATE livre(nom_livre,prix)" +
               "WHERE  PRIX "+ "prix ='" + textBox2.Text + "'");
            listBox2.Items[listBox2.SelectedIndex] = textBox2.Text;
            MessageBox.Show("le prix à été changer");

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox2 = new ListBox();
            listBox2.Items.Add("cent");
            listBox2.Items.Add("cent_dix");
            listBox2.Items.Add("cinque-cent");
            listBox2.Items.Add("mille");


        }
    }
}