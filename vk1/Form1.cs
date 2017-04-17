using System;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        VkApi vkApi = new VkApi();
        long? thisUserID = 0;
        long UserId = 0;
        string thisUserName = "";


        public Form1()
        {
            InitializeComponent();
                
            textBox3.Visible = false;
            History.Visible = false;
            button3.Visible = false;
            listBox.Visible = false;
            button6.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {


        }

        //public void GetOnlineFriends()
        //{
        //    var online = vkApi.Friends.GetOnlineEx(new FriendsGetOnlineParams{

        //        UserId = long.Parse(UserId.ToString())


        //    });

        //    foreach (var friend in online.Online)
        //    {
        //        //listBox.Items.Add(friend.FirstName + " " + friend.LastName);
        //    }
        //}

        public void GetFriends()
        {
            var users = vkApi.Friends.Get(long.Parse(thisUserID.ToString()), ProfileFields.FirstName | ProfileFields.LastName);

            foreach (var friend in users)
            {
                listBox.Items.Add(friend.FirstName + " " + friend.LastName + " | " + friend.Id);
            }
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int appID = 5982087;
            string email = textBox1.Text;
            string password = textBox2.Text;
            Settings settings = Settings.All;
            try
            {
                listBox.Items.Clear();
                vkApi.Authorize(new ApiAuthParams
                {
                    ApplicationId = (ulong)appID,
                    Login = email,
                    Password = password,
                    Settings = settings
                });

                thisUserID = vkApi.UserId;
                thisUserName = vkApi.Users.Get(long.Parse(thisUserID.ToString())).FirstName;
                GetFriends();
                //GetOnlineFriends();
                this.timer.Enabled = true;
                textBox1.Visible = false;
                textBox2.Visible = false;
                button1.Visible = false;
                textBox3.Visible = true;
                History.Visible = true;
                button3.Visible = true;
                listBox.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                button6.Visible = true;

            }
            catch
            {
                MessageBox.Show("Неверный логин или пароль. Либо проверьте интернет-соединение.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void quit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }



        private void buttonSending_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text != "")
                {
                    // var send =
                    vkApi.Messages.Send(UserId, false, textBox3.Text);

                    textBox3.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("no msg");
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                UserId = long.Parse(listBox.Text.Substring(listBox.Text.LastIndexOf(' ')));

            }
            catch
            {
                MessageBox.Show("");
            }
            GetHistory();
        }

        void Hiding()
        {
            button3.Visible = false;
            textBox3.Visible = false;
            History.Visible = false;
            listBox.Visible = false;
            button6.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hiding();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            textBox3.Visible = true;
            History.Visible = true;
            listBox.Visible = true;
            button6.Visible = true;
        }


        void GetHistory()
        {
            History.Items.Clear();
            var getHistory = vkApi.Messages.GetHistory(new MessagesGetHistoryParams
            {
                Count = 200,
                UserId = long.Parse(UserId.ToString()),
            });


            foreach (var message in getHistory.Messages)
            {
                if (message.FromId == thisUserID)
                {
                    History.Items.Add(thisUserName + ": " + message.Body);

                }
                if (message.FromId == UserId)
                {
                    History.Items.Add(listBox.Text.Substring(0, listBox.Text.IndexOf(' ')) + ": " + message.Body);

                }
            }


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.Width >= 590) this.timer.Enabled = false;
            else this.Width += 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                GetHistory();
            }
            catch
            {
                MessageBox.Show("There are no object for update");
            }
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    this.timer.Enabled = true;
        //}
    }
}

