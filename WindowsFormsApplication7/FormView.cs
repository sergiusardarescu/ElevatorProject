using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication7
{
    public partial class FormView : Form
    {
        enum Position
        {
            Up, Down, Stop
        }

        private int _x;
        private int _y;
        private int counter = 0;
        private Position _objPosition;
        animation animate1;
        animation animate2;
        Graphics g;
        Graphics scG;
        System.Windows.Forms.Timer open;
        System.Windows.Forms.Timer close;
        Bitmap btm;

        public FormView()
        {
            InitializeComponent();

            _x = 50;
            _y = 30;
            _objPosition = Position.Stop;
            button1.Enabled = false;
            button3.Enabled = false;
        }

        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(new Bitmap("LiftDoors.jpg"), _x, _y, 75, 150);
        }

        private void tmrMoving_Tick(object sender, EventArgs e)
        {
            if (counter < 228)
            {
                counter++;

                if (_objPosition == Position.Up && _y >= 30)
                {
                    _y -= 1;
                    pictureBox1.Image = Properties.Resources.up;
                }
                else if (_objPosition == Position.Down && _y <= 230)
                {
                    _y += 1;
                    pictureBox1.Image = Properties.Resources.down;
                }
                else if (_objPosition == Position.Stop)
                {
                    tmrMoving.Stop();
                }

                Invalidate();
            }
            else
            {
                if (_y == 29)
                {
                    tmrMoving.Enabled = false;
                    counter = 0;
                    button1.BackColor = Color.White;
                    button2.BackColor = Color.White;
                    button3.BackColor = Color.White;
                    button4.BackColor = Color.White;
                    pictureBox1.Image = Properties.Resources._1;
                    //open.Start();
                    //close.Stop();
                }
                else
                {
                    tmrMoving.Enabled = false;
                    counter = 0;
                    button1.BackColor = Color.White;
                    button2.BackColor = Color.White;
                    button3.BackColor = Color.White;
                    button4.BackColor = Color.White;
                    pictureBox1.Image = Properties.Resources.G;
                    //open.Stop();
                    //close.Start();
                    
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tmrMoving.Start();
            _objPosition = Position.Up;
            button1.BackColor = Color.Red;
            button2.BackColor = Color.White;
            insertdata("Lift Going Up");
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = true;
            //open.Start();
            //close.Start();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            tmrMoving.Start();
            button2.BackColor = Color.Red;
            button1.BackColor = Color.White;
            _objPosition = Position.Down;
            insertdata("Lift Going Down");
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            //open.Start();
            //close.Start();
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();

            btm = new Bitmap(150, 200);

            scG = Graphics.FromImage(btm);

            open = new System.Windows.Forms.Timer();
            open.Interval = 200;
            open.Tick += new EventHandler(open_Tick);

            close = new System.Windows.Forms.Timer();
            close.Interval = 200;
            close.Tick += new EventHandler(close_Tick);

            animate1 = new animation(new Bitmap[] { Properties.Resources.LiftDoors, Properties.Resources.LiftDoors_1, Properties.Resources.LiftDoors_2, Properties.Resources.LiftDoors_3, Properties.Resources.LiftDoors_4, Properties.Resources.LiftDoors_Open });
            animate2 = new animation(new Bitmap[] { Properties.Resources.LiftDoors_Open, Properties.Resources.LiftDoors_4, Properties.Resources.LiftDoors_3, Properties.Resources.LiftDoors_2, Properties.Resources.LiftDoors_1, Properties.Resources.LiftDoors });

        }

        private void open_Tick(object sender, EventArgs e)
        {
            if (counter < 228)
            {
                scG.DrawImage(animate1.GiveNextImage(), new Point(_x, _y));
                g.DrawImage(btm, Point.Empty);
            }
            else
            {
                scG.DrawImage(animate1.GiveNextImage(), new Point(_x, 200));
                g.DrawImage(btm, Point.Empty);
            }

            
        }

        private void close_Tick(object sender, EventArgs e)
        {
            if (counter < 228)
            {         
                scG.DrawImage(animate2.GiveNextImage(), new Point(_x, _y));
                g.DrawImage(btm, Point.Empty);
            }
            else
            {
                close.Enabled = false;
            }

            Invalidate();
        }

        private void insertdata(string action)
        {
            string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source = Elevator.accdb;";
            string dbcommand = "insert into [Actions] ([Date],[Time],[Action]) values (@date, @time, @action)";
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();
            listBox1.Items.Add(date + "\t\t" + time + "\t\t" + action);



            OleDbConnection conn_db = new OleDbConnection(dbconnection);
            OleDbCommand comm_insert = new OleDbCommand(dbcommand, conn_db);
            OleDbDataAdapter adapter_insert = new OleDbDataAdapter(comm_insert);
            comm_insert.Parameters.AddWithValue("@date", date);
            comm_insert.Parameters.AddWithValue("@time", time);
            comm_insert.Parameters.AddWithValue("@action", action);




            conn_db.Open();

            comm_insert.ExecuteNonQuery();

            conn_db.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tmrMoving.Start();
            button3.BackColor = Color.Green;
            button4.BackColor = Color.White;
            _objPosition = Position.Up;
            insertdata("Lift Going To First");
            button3.Enabled = false;
            button4.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
            //open.Start();
            //close.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tmrMoving.Start();
            button3.BackColor = Color.White;
            button4.BackColor = Color.Green;
            _objPosition = Position.Down;
            insertdata("Lift Going To Ground");
            button3.Enabled = true;
            button4.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
            
        }

        private bool prep;
        public DataSet dSet = new DataSet();

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Elevator.accdb;";
                string dbcommand = "Select * from Actions;";
                OleDbConnection conn = new OleDbConnection(dbconnection);
                OleDbCommand comm = new OleDbCommand(dbcommand, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(comm);

                conn.Open();
                while (prep == false)
                {
                    adapter.Fill(dSet);
                    prep = true;
                }
                //cnn.Close();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot open connection! ");
                string message = "Error in connection to datasource";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
            }

            listBox1.Items.Clear();
            foreach (DataRow row in dSet.Tables[0].Rows)
            {
                listBox1.Items.Add(row["Date"] + "\t\t" + row["Time"] + "\t\t" + row["Action"]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
