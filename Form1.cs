
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;


using MySqlX.XDevAPI.Common;
using System.Collections;
using LoginSysten;
using MySqlX.XDevAPI.Relational;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel.DataAnnotations;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        Config db = new Config();
        public Form1()
        {
            InitializeComponent();
            db.Connect("user");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = true;
            button2.Visible = true;
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            { 
                db.Execute("INSERT INTO user (nom) " +
                    "VALUES nom='" + textBox1.Text + "'");


            bool flag = comboBox1.Items.Contains(textBox1);
            if (this.comboBox1.Items.Contains(textBox1.Text))
            {
                MessageBox.Show("user already existing");

            }
            else
            {
                MessageBox.Show("user added");
                comboBox1.Items.Add(textBox1.Text);

            }
            }
            else if(checkBox3.Checked == true)
            {
                comboBox1.Items.Remove(comboBox1.SelectedItem);

                db.Execute("DELETE FROM user(nom)" +
                    "WHERE  nom " + "nom ='" + comboBox1.SelectedItem + "'");
            }
            else if(checkBox2.Checked==true)
            {
                db.Execute("UPDATE user(nom)" +
                   "WHERE  nom " + "nom ='" + textBox1.Text + "'");
                comboBox1.Items[comboBox1.SelectedIndex] = textBox1.Text;
                MessageBox.Show("le nom  à été changer");
            }



        }

        
       

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Clear();
            db.Execute("DELETE * FROM user(nom)" + "'");
        }

       
        bool textboxChanged = false;
        /* private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
         {
             if (!textboxChanged)
             {
                 checkbox2.visible=false;
                 checkbox3.visible=false;

             }
             else if(textboxChanged)
             {

                    checkbox2.visible=true;
                 checkbox3.visible=true;


         }
        */



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
           db.ExecuteSelect("SELECT * FROM `user` " );
            for (int i = 0; i < db.Count(); i++)

            {

                comboBox1.Items.Add(db.Results(i,0) + " " + db.Results(i,1) + " " + db.Results(i,2));

            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                button1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;

            }
            else
            {
                button1.Visible = false;
               


            }



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                checkBox2.Visible = false;
                checkBox3.Visible = false;  

            }
            else if(checkBox1.Checked==false)
            {
                checkBox2.Visible = true;
                checkBox3.Visible=true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Visible = false;
                checkBox3.Visible = false;
               
                

            }
            else if (checkBox2.Checked == false)
            {
                checkBox1.Visible = true;
                button1.Visible = true;
                checkBox3.Visible = true;
                button2.Visible = true;
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Visible = false;
                checkBox2.Visible = false;
               
               

            }
            else if (checkBox2.Checked == false)
            {
                checkBox1.Visible = true;
                button1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                button2.Visible = true;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load_2(object sender, EventArgs e)
        {

        }
    }
    }

namespace LoginSysten
{
    public class Config
    {
        string ConectionString = "";
        public MySqlConnection connection = null;
        public string server = "127.0.0.1";
        public string user = "root";
        public string password = "";
        DataSet ds;
        DataTable dt;
        public string Table = "user";
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

       
        public void ExecuteSql(string Sql_command)
        {

            nowquiee(Sql_command);

        }

        // creates con to MySQL  
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


        public void Execute(string Sql_command)
        {
            RecordSource = Sql_command;
            ConnectionType = Table;
            dt = new DataTable(ConnectionType);
            try
            {
                string command = RecordSource.ToUpper();


                MySqlDataAdapter da2 = new MySqlDataAdapter(RecordSource, connection);

                DataSet tempds = new DataSet();
                da2.Fill(tempds, ConnectionType);
                da2.Fill(tempds);



            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

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
        public string Results(int ROW, int COLUMN_NAME)
        {
            try
            {
                return dt.Rows[ROW][COLUMN_NAME].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return dt.Rows[ROW][COLUMN_NAME].ToString();

            }
        }
        public int Count()
        {
            return dt.Rows.Count;
        }
        
    }
   /* public DataSet validateUserName()
    {
        MySqlConnection cs = new MySqlConnection(ConectionString);
        cs.Open();
        MySqlCommand dbCommand = new MySqlCommand();
        var stm = "SELECT COUNT(*) FROM user  WHERE nom= '" + TextBox1.text + '";
            dbCommand.command(stm);
        // this textusername is from the window form
        dt = new DataTable(ConnectionType);
        MySqlDataAdapter da = new MySqlDataAdapter(RecordSource, connection);
        DataSet ds = new DataSet();
        da = new MySqlDataAdapter();
        da.SelectCommand = dbCommand;
        da.Fill(ds);
        return ds;
        cs.Close();

    }
   */
}
