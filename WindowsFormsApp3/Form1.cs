using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private Account[] ac = new Account[3];
        private Account activeAccount = null;
        private int userInput;
        private int pin;
       
        public Form1(Account[] ac)
        {
            InitializeComponent();
            button1.Click += new EventHandler(MyButtonClick);
            button2.Click += new EventHandler(MyButtonClick);
            button3.Click += new EventHandler(MyButtonClick);
            button4.Click += new EventHandler(MyButtonClick);
            button5.Click += new EventHandler(MyButtonClick);
            button6.Click += new EventHandler(MyButtonClick);
            button7.Click += new EventHandler(MyButtonClick);
            button8.Click += new EventHandler(MyButtonClick);
            button9.Click += new EventHandler(MyButtonClick);
            button0.Click += new EventHandler(MyButtonClick);
            btnEnter.Click += new EventHandler(inputSelection);
            this.ac = ac;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
          

            promptAccountNum();

        }

        void promptAccountNum()
        {
            while (lstThing.Items.Count > 0)
            {
                lstThing.Items.RemoveAt(0);
            }
            lstThing.Items.Add("Please Enter Your Account Number");
        }

        void sideInput(Object sender,EventArgs e)
        {
            Button button = sender as Button;
            String switchNum = Convert.ToString(button.Name[(button.Name).Length - 1 ]);
            Console.WriteLine(switchNum);
            switch (switchNum)
            {
                case "1":

                    withdawCash();
                    Console.WriteLine("Case 1");
                    break;
                case "2":
                    displayBalance();
                    Console.WriteLine("Case 2");
                    break;
                case "3":
                    activeAccount = null;
                    promptAccountNum();                   
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }
        void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            txtInput.Text += button.Name[(button.Name).Length - 1];
        }

        void inputSelection(object sender, EventArgs e)
        {
            Button button = sender as Button;
            try
            {
                userInput = Convert.ToInt32(txtInput.Text);
            }
            catch
            {
                displayOptions();
            }
            txtInput.Text = "";
            if (activeAccount == null)
            {
                checkSequence();
            }
            else
            {

                if (activeAccount.checkPin(userInput))
                {
                    lstThing.Items.Add("Pin Correct");
                    pin = userInput;
                    displayOptions();
                }
                else
                {
                    lstThing.Items.Add("Pin Incorrect");
                }
            }
        }
        void withdawCash()
        {
           

            while (lstThing.Items.Count > 0)
            {
                lstThing.Items.RemoveAt(0);
            }
            
            lstThing.Items.Add("1>10 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("2>20 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("3>40 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("4>100 ");
            lstThing.Items.Add("");
            lstThing.Items.Add("5>500 ");



            lstThing.Items.Add("3> exit");
            resetButtons();
            btnOption1.Click += new EventHandler(selectMoney);
            btnOption2.Click += new EventHandler(selectMoney);
            btnOption3.Click += new EventHandler(selectMoney);
            btnOption4.Click += new EventHandler(selectMoney);
            btnOption5.Click += new EventHandler(selectMoney);
        }

        

        void selectMoney(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String switchNum = Convert.ToString(button.Name[(button.Name).Length - 1]);
            Console.WriteLine(switchNum);
            switch (switchNum)
            {
                case "1":
                    
                    decBalance(10);
                    moneyGif();
                    break;
                case "2":
                    decBalance(20);
                    break;
                case "3":
                    decBalance(40);
                    break;
                case "4":
                    decBalance(100);
                    break;
                case "5":
                    decBalance(500);
                    break;
                default:
                    Console.WriteLine("Default case(2nd)");
                    break;
            }

        }

        async void moneyGif()
        {
            money.Visible = true;
            await Task.Delay(1000);
       //     money.Visible = false;
        }
        void decBalance(int value)
        {
            while (lstThing.Items.Count > 0)
            {
                lstThing.Items.RemoveAt(0);
            }

            if (activeAccount.decrementBalance(value)){
                lstThing.Items.Add("new balance " + activeAccount.getBalance());
                lstThing.Items.Add("Press enter button to continue:");
            }
            else
            {
                lstThing.Items.Add("There is not enough funds in this account");
                lstThing.Items.Add("Press enter button to continue:");
               
            }
        }
        void displayOptions()
        {
            Console.WriteLine("DISPLAY");
                while (lstThing.Items.Count > 0)
                {
                 lstThing.Items.RemoveAt(0);
                 }
                lstThing.Items.Add("1> take out cash");
                lstThing.Items.Add("");
                lstThing.Items.Add("2> balance");
                lstThing.Items.Add("");
                lstThing.Items.Add("3> exit");

            resetButtons();

            btnOption1.Click += new EventHandler(sideInput);
            btnOption2.Click += new EventHandler(sideInput);
            btnOption3.Click += new EventHandler(sideInput);


        }
        void resetButtons()
        {
            RemoveClickEvent(btnOption1);
            RemoveClickEvent(btnOption2);
            RemoveClickEvent(btnOption3);
            RemoveClickEvent(btnOption4);
            RemoveClickEvent(btnOption5);

        }
        
        private void displayBalance()
        {
            if (this.activeAccount != null)
            {
                while (lstThing.Items.Count > 0)
                {
                    lstThing.Items.RemoveAt(0);
                }
                lstThing.Items.Add(" your current balance is : " + activeAccount.getBalance());
                lstThing.Items.Add("Press Enter Button to Continue");
            }
        }
        


        private void lstThing_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }

        void checkSequence()
        {

            activeAccount = findAccount();

            if (activeAccount != null)
            {
                Console.WriteLine("AccountFound");
                promptForPin();
            }
            else
            {   //if the account number entered is not found let the user know!
                Console.WriteLine("no matching account found.");
                
            }
            
        }
        private Account findAccount()
        {

            for (int i = 0; i < this.ac.Length; i++)
            {
                if (ac[i].getAccountNum() == userInput)
                {
                    return ac[i];
                }
            }
           return null;

        }
            private bool getAccountNumber()
        {
            //lstThing.Items.Clear;
            lstThing.Items.Add("Enter your account number..");

            return true;
        }
        

        private void promptForPin()
        {
            lstThing.Items.RemoveAt(0);
            lstThing.Items.Add("Enter Pin");      
        }
        /**
         * Code From
         * https://stackoverflow.com/questions/91778/how-to-remove-all-event-handlers-from-an-event
         * **/

        private void RemoveClickEvent(Button b)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick",
                BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(b);
            PropertyInfo pi = b.GetType().GetProperty("Events",
                BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)pi.GetValue(b, null);
            list.RemoveHandler(obj, list[obj]);
        }



        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button0_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }
        private void btnEnter_Click(object sender, EventArgs e)
        {

        }

        private void btnOption1_Click(object sender, EventArgs e)
        {

        }

        private void btnOption2_Click(object sender, EventArgs e)
        {

        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }
    }


    /*
     *   The Account class encapusulates all features of a simple bank account
     */
    public class Account
    {
        
        
        //the attributes for the account
        private int balance;
        private int pin;
        private int accountNum;

        // a constructor that takes initial values for each of the attributes (balance, pin, accountNumber)
        public Account(int balance, int pin, int accountNum)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
        }

        //getter and setter functions for balance
        public int getBalance()
        {
            return balance;
        }
        public void setBalance(int newBalance)
        {
            this.balance = newBalance;
        }

        /*
         *   This funciton allows us to decrement the balance of an account
         *   it perfomes a simple check to ensure the balance is greater tha
         *   the amount being debeted
         *   
         *   reurns:
         *   true if the transactions if possible
         *   false if there are insufficent funds in the account
         */
        public Boolean decrementBalance(int amount)
        {
            //Bank.sema.Release();
           // Bank.sema.WaitOne();
            if (this.balance > amount)
            {
                int temp = balance;
                Thread.Sleep(4000);
                balance = temp - amount;
          //      Bank.sema.Release();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * This funciton check the account pin against the argument passed to it
         *
         * returns:
         * true if they match
         * false if they do not
         */
        public Boolean checkPin(int pinEntered)
        {
            if (pinEntered == pin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int getAccountNum()
        {
            return accountNum;
        }

    }
    /* 
     *      This is out main ATM class that preforms the actions outlined in the assigment hand out
     *      
     *      the constutor contains the main funcitonality.
     */

    public class Bank
    {
        public static Semaphore sema = new Semaphore(0, 1);
        private Account[] ac = new Account[3];
        private Form1 atm;


        public Bank()
        {
            ac[0] = new Account(300, 1111, 111111);
            ac[1] = new Account(750, 2222, 222222);
            ac[2] = new Account(3000, 3333, 333333);

            


            // Application.Run(new Form1());
            Thread ATM2 = new Thread(new ThreadStart(ThreadProc));
            ATM2.Start();
            Thread ATM1 = new Thread(new ThreadStart(ThreadProc));
            ATM1.Start();
        }

        private static void Main(string[] args)
        {
            Bank test = new Bank();

        }

        private void ThreadProc()
        {
            var frm = new Form1(ac);
            frm.ShowDialog();
        }
    }
}








